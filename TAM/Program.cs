using Microsoft.Extensions.CommandLineUtils;
using System;

namespace TAM
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = "TAM Tools";
            app.Description = "A series of tools which can be used to help the TAM team run smoother and automate parts of their life";

            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                app.ShowHint();
                return 0;
            });

            app.Command("refresh", (command) =>
            {
                command.Description = "Refresh local system";


                command.HelpOption("-?|-h|--help");

                command.OnExecute(() =>
                {
                    Console.WriteLine("simple-command has finished.");
                    return 0;
                });
            });

            try
            {
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
                app.ShowHelp();
            }

        }
    }
}
