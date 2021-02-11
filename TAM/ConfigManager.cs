using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace TAM
{
    [Command("config", Description = "Configure the application for use"),
       Subcommand(typeof(Refresh))]
    class ConfigManager
    {

        private int OnExecute(IConsole console)
        {
            console.Error.WriteLine("You must specify an action. See --help for more details.");
            return 1;
        }

        [Command("refresh", Description = "Update local DB",
            UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
        private class Refresh
        {
            [Option(Description = "Show all containers (default shows just running)")]
            public bool All { get; }

            private IReadOnlyList<string> RemainingArguments { get; }

            private void OnExecute(IConsole console)
            {
                console.WriteLine("testing");
            }
        }
    }
}
