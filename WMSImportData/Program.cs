using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Data;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using WmsReport.Infrastructure.DbCommon;
using WMSImportData.BLL;
using Microsoft.Extensions.Options;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WmsReport.Infrastructure.Config;
using WMSImportData.Config;
using System.Collections.Generic;
using WMSImportData.Model;
using System.Linq;
using AccountCenter.Api.Infrastructure.WmsEncryption;
using System.Threading.Tasks;
using NLog;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace WMSImportData
{
    class Program
    {
        //private static IServiceProvider _serviceProvider;
        //public static IConfiguration config;
        //public static ImportData importData;
        //static  Program()
        //{
        //    Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "local");

        //    RegisterServices();
            
        //    var service = _serviceProvider.GetService<IWmsDbConnection>();

        //    importData = new ImportData(service,config);

            
        //}

        public static void RegisterConfigurationInjectionsAutoFac(IServiceProvider serviceProvider, ContainerBuilder builder)
        {
            builder.Register(context => serviceProvider.GetService<IOptions<ApiConfig>>());
            builder.Register(context => serviceProvider.GetService<IOptions<DbConnections>>());

        }
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var  config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.local.json", true, true)
          .Build();
                

                var servicesProvider = BuildDi(config);
                using (servicesProvider as IDisposable)
                {
                    var runner = servicesProvider.GetRequiredService<Runner>();
                    await runner.DoAction("ImportData");
                }

                Console.Write("Press any key to continue...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
            

        }

        private static IServiceProvider BuildDi(IConfiguration config)
        {
            return new ServiceCollection()
               .AddSingleton(config)
               .Configure<DbConnections>(config.GetSection(nameof(DbConnections)))
               .AddTransient<Runner>() // Runner is the custom class
               .AddScoped<IImportData, ImportData>()
               .AddScoped<IWmsDbConnection, WmsDbConnection>()
               .AddLogging(loggingBuilder =>
               {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(config);
               })
               .BuildServiceProvider();
        }

        private static void RegisterServices()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(env))
            {
                // TODO this could fall back to an environment, rather than exceptioning?
                throw new Exception("ASPNETCORE_ENVIRONMENT env variable not set.");
            }
            Console.WriteLine($"Bootstrapping application using environment {env}");

            var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
             .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: false)
             .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var collection = new ServiceCollection();
            collection.AddOptions();
            collection.Configure<ApiConfig>(configuration.GetSection(nameof(ApiConfig)));
            collection.Configure<DbConnections>(configuration.GetSection(nameof(DbConnections)));
            collection.Configure<IConfiguration>(configuration);
            IServiceProvider _serviceProvider = collection.BuildServiceProvider();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            RegisterConfigurationInjectionsAutoFac(_serviceProvider, containerBuilder);

            containerBuilder.RegisterType<WmsDbConnection>().As<IWmsDbConnection>();

            //
            // Add other services ...
            //
            containerBuilder.Populate(collection);

            var appContainer = containerBuilder.Build();

            //apiConfig = appContainer.Resolve<IOptions<ApiConfig>>();

            _serviceProvider = new AutofacServiceProvider(appContainer);
        }


    }

    
}
