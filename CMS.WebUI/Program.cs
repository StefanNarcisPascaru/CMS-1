using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using CMS.Domain;

namespace CMS.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
           // ProcessDbCommands("seeddb", host);

            host.Run();
        }
        private static void ProcessDbCommands(string arg, IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {

                if (arg.Equals("seeddb"))
                {
                    var db = GetDb(scope);
                    db.Seed();
                }
            }
        }

        private static CmsContext GetDb(IServiceScope services)
        {
            var db = services.ServiceProvider.GetRequiredService<CmsContext>();
            return db;
        }
    }
}
