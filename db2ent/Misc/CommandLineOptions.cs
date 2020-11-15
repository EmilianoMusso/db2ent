using CommandLine;

namespace db2ent.Misc
{
    public class CommandLineOptions
    {
        [Option(Default = "", Required = true, HelpText = "The connection string to database")]
        public string ConnectionString { get; set; }

        [Option(Default = "", Required = true, HelpText = "Name of table to be processed")]
        public string TableName { get; set; }

        [Option(Default = 99999, Required = false, HelpText = "Maximum number of records to process from table")]
        public int NumRecords { get; set; }
    }
}
