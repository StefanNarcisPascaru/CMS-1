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

            //services.AddIdentity<User, Rank>()
            //.AddEntityFrameworkStores<CmsContext>()
            //.AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("Cookies").RequireAuthenticatedUser().Build();
                options.AddPolicy("FacultyMember", policy => policy.RequireClaim("Rank", "Professor", "Student"));
            });

            services.AddMvc();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CmsContext>().As<DbContext>().InstancePerLifetimeScope();//.InstancePerRequest();
            containerBuilder.RegisterType<UserLogic>().As<IUserLogic>();
            containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            containerBuilder.Populate(services);
            ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.ApplicationServices.GetRequiredService<CmsContext>().Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

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
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
