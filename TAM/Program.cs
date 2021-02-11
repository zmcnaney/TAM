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
    public class TAM
    {
        public static void Main(string[] args) {


            SetupSimpleConfiguration();

            // Create service provider
            // Print connection string to demonstrate configuration object is populated
            Console.WriteLine(Config.GetConnectionString("DataConnection"));


            //console.WriteLine("You must specify at a subcommand.");
            CommandLineApplication.Execute<TAM>(args);
                }

        private static void SetupSimpleConfiguration()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("app.json")
                .Build();
        }
        public static IConfigurationRoot Config { get; private set; }

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp();
            return 1;
        }


    }

}
    
