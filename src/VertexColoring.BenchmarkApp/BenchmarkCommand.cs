using EntryPoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VertexColoring.BenchmarkApp
{
    sealed partial class BenchmarkCommand
    {
        public BenchmarkCommand(CliArguments options)
        {
            Options = options;
            Log = new Logger(Options.Debug ? Console.Out : null, Console.Out);
            Sizes = options.VertexCount.Zip(options.EdgeCount, (v, e) => (vertices: v, edges: e)).ToList();
        }

        private CliArguments Options { get; }

        private List<(int vertices, int edges)> Sizes { get; }

        private Logger Log { get; }

        public static void Execute(string[] args)
        {
            var options = Cli.Parse<CliArguments>(args);
            new BenchmarkCommand(options).Run();
#if DEBUG
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
#endif
        }

        public void Run()
        {
            var runner = new BenchmarkRunner(Sizes, Options.Number, Options.Filename)
            {
                Log = Log
            };
            runner.Run();
        }
    }
}
