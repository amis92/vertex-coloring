using System;
using System.Collections.Generic;
using System.Text;

namespace VertexColoring.Cli
{
    sealed partial class ColoringCommand
    {
        public ColoringCommand(CliArguments options)
        {
            Options = options;
            Log = new Logger(Options.Debug ? Console.Out : null, Console.Out);
        }

        private CliArguments Options { get; }

        private List<(int vertices, int edges)> Sizes { get; }

        private Logger Log { get; }

        public static void Execute(string[] args)
        {
            var options = EntryPoint.Cli.Parse<CliArguments>(args);
            new ColoringCommand(options).Run();
#if DEBUG
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
#endif
        }

        public void Run()
        {
            // TODO
        }
    }
}
