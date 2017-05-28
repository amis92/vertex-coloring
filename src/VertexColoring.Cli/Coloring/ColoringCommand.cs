using System;
using System.Collections.Immutable;
using System.Linq;

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

        public Logger Log { get; set; }

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
            var runner = new ColoringRunner(
                Options.VertexCounts.Zip(Options.EdgeCounts, (v, e) => (vertices: v, edges: e)),
                Options.Number,
                Options.Filename,
                Options.OutputFilename,
                Options.Algorithms.Distinct().ToImmutableArray())
            {
                Log = Log
            };
            runner.Run();
        }
    }
}
