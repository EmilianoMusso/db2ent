using CommandLine;
using db2ent.Data;
using db2ent.Misc;
using System;

namespace db2ent
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parses command line arguments using CommandLineParser
            // Uses db2Ent.Misc.CommandLineOptions to store parsed values
            // <see>https://www.nuget.org/packages/CommandLineParser/2.8.0/</see>
            Parser.Default.ParseArguments<CommandLineOptions>(args)
                          .MapResult(
                            (opts) => RunProcedure(opts), (errs) => { return -1; } );
        }

        /// <summary>
        /// Executes connection, table query, result output
        /// </summary>
        /// <param name="opts">CommandLineOptions object with stored command line arguments specified by user</param>
        /// <returns></returns>
        private static int RunProcedure(CommandLineOptions opts) 
        { 
            // *** Currently works for SQL Server only ***
            // TO DO: Implement different data access class inheriting from EntConnector to use the code
            // with other providers
            
            // ConnectionString inherited from arguments
            var entSqlServer = new EntSqlServer(opts.ConnectionString);
            
            var entGenericDb = new EntGenericDb(entSqlServer);
            // ***

            if (entGenericDb.OpenConnection())
            {
                // TableName and NumRecords inherited from arguments
                var tableList = opts.TableName.Split(',');
                foreach (var tablename in tableList)
                {
                    var results = entGenericDb.ToString(tablename.Trim(), opts.NumRecords, opts.Where);

                    // Currently outputs result to console
                    Console.WriteLine(results);
                }

                entGenericDb.CloseConnection();
            }

            return 0;
        }
    }
}
