using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAM
{

    [Command(Name = "TAM", Description = "A list of commands to help make a TAM's life easier"),
        Subcommand(typeof(ConfigManager))]

    [HelpOption("-?|-h|--help")]
    public class TAM : TAMapp
    {

        override public int RunCommand()
        {
            console.WriteLine();
            console.Error.WriteLine("You must specify an action. See --help for more details.");
            console.WriteLine(configuration.GetConnectionString("SnowFlake"));
            return 0;
        }

    }

    public class TAMapp
    {
        public static IConfigurationRoot configuration;
        public static void Main(string[] args)
        {


            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            // Print connection string to demonstrate configuration object is populated

            var app = new CommandLineApplication<TAM>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(serviceProvider);
            app.Execute(args);
        }
        private CommandLineApplication app;
        protected IConsole console;
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Add logging
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("app.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add app
            serviceCollection.AddTransient<TAM>();
        }
        public int OnExecute(CommandLineApplication _app, IConsole _console)
        {
            app = _app;
            console = _console;
            return RunCommand();
        }

        virtual public int RunCommand()
        {
            console.WriteLine();
            console.Error.WriteLine("You must specify an action. See --help for more details.");
            return 0;
        }
       
    }
}
    
