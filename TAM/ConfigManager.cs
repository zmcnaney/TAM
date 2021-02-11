using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Snowflake.Data.Client;
using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TAM
{
    [Command("config", Description = "Configure the application for use"),
       Subcommand(typeof(BookingList))]
    class ConfigManager : TAMapp    
    {

        override public int RunCommand()
        {
            
            console.WriteLine();
            console.Error.WriteLine("Do the thing");
            Console.WriteLine(configuration.GetConnectionString("DataConnection"));

            return 1;
        }

        [Command("listbookings", Description = "Get the most recent bookings",
            UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.StopParsingAndCollect)]
        private class BookingList : TAMapp
        {
            [Option(Description = "Show all containers (default shows just running)")]
            public bool All { get; }

            private IReadOnlyList<string> RemainingArguments { get; }

            override public int RunCommand()
            {


                using (var conn = new SnowflakeDbConnection())
                {
                    conn.ConnectionString = configuration.GetConnectionString("SnowFlake");
                    
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    var CommandText = "SELECT * from DB_APPS.PUBLIC.BOOKINGS as b ORDER BY b.CREATED_AT DESC LIMIT 10";
                    var reservations = conn.Query<Reservation>(CommandText);
                    
                    foreach (var reservation in reservations)
                    {
                        console.WriteLine("= " + reservation.Id);

                    }
                }

                return 0;

            }

        }
    }
}
