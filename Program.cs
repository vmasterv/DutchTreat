using DutchTreat.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            if (args.Length == 1 && args[0].ToLower() == "/seed")
            {
                RunSeeding(host);
            }
            else
            {
                host.Run();
            }

        }

        private static void RunSeeding(IHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.SeedAsync().Wait();
            
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(AddConfiguration)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        public static void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            bldr.Sources.Clear();

            bldr.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                //.AddUserSecrets<Program>()
                .AddEnvironmentVariables();
        }

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //    .ConfigureAppConfiguration(SetupConfiguration)
        //     .UseStartup<Startup>()
        //     .Build();


        //private static void SetupConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder bldr)
        //{
        //    bldr.Sources.Clear();

        //    bldr.SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("config.json")
        //        .AddEnvironmentVariables();
        //}
    }
}
