using System;
using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Cli
{
    sealed partial class BenchmarkCommand
    {
        public BenchmarkCommand(CliArguments options)
        {
            Options = options;
            Log = new Logger(Options.Debug ? Console.Out : null, Console.Out);
        }

        private CliArguments Options { get; }

        private Logger Log { get; }

        public static void Execute(string[] args)
        {
            var options = EntryPoint.Cli.Parse<CliArguments>(args);
            new BenchmarkCommand(options).Run();
#if DEBUG
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
#endif
        }

        public void Run()
        {
            var runner = new BenchmarkRunner(
                Options.VertexCounts.Zip(Options.EdgeCounts, (v, e) => (vertices: v, edges: e)),
                Options.Number,
                Options.Filename,
                Options.Algorithms.Distinct().ToImmutableArray())
            {
                Log = Log,
                Baseline = Options.Baseline
            };
            runner.Run();
        }
    }
}
