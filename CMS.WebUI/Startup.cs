using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.Domain;
using Repository;
using CMS.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CMS.WebUI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CmsContext>();
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("Cookies").RequireAuthenticatedUser().Build();
                options.AddPolicy("FacultyMember", policy => policy.RequireClaim("Rank", "Admin","Professor", "Student"));
                options.AddPolicy("Professor", policy => policy.RequireClaim("Rank", "Admin","Professor"));
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Rank", "Admin"));
            });

            services.AddMvc();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CmsContext>().As<DbContext>().InstancePerLifetimeScope();//.InstancePerRequest();
            containerBuilder.RegisterType<UserLogic>().As<IUserLogic>();
            containerBuilder.RegisterType<RankLogic>().As<IRankLogic>();
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            containerBuilder.Populate(services);
            ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.ApplicationServices.GetRequiredService<CmsContext>().Seed();

            var sslPort = 0;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile(@"Properties/launchSettings.json", optional: false, reloadOnChange: true);
                var launchConfig = builder.Build();
                sslPort = launchConfig.GetValue<int>("iisSettings:iisExpress:sslPort");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.IsHttps)
                {
                    await next();
                }
                else
                {
                    var sslPortStr = $":{sslPort}";
                    var httpsUrl = $"https://{context.Request.Host.Host}{sslPortStr}{context.Request.Path}";
                    context.Response.Redirect(httpsUrl);
                }
            });

            app.UseStaticFiles();
            // app.UseIdentity();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "Cookies",
                LoginPath = new PathString("/Account/Login/"),
                AccessDeniedPath = new PathString("/Account/Forbidden/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
