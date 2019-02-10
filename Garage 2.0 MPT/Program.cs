using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Garage_2._0_MPT.Models;

namespace Garage_2._0_MPT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();
            using (var scope = webHost.Services.CreateScope())
            {

                var services = scope.ServiceProvider;
                SeedData.Initialize(services);
            }

            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
      //      .ConfigureAppConfiguration((hostingContext, config) =>
      //      {
      //          config.SetBasePath(Directory.GetCurrentDirectory());
            //    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
      //          config.AddJsonFile("Sectret.json", optional: false, reloadOnChange: false);
            //    config.AddEnvironmentVariables();
      //      })
                .UseStartup<Startup>();
    }
}
