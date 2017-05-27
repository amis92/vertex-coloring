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
            Log = Options.Debug ? new ConsoleLogger() : null;
            Sizes = options.VertexCount.Zip(options.EdgeCount, (v, e) => (vertices: v, edges: e)).ToList();
        }

        private CliArguments Options { get; }

        private List<(int vertices, int edges)> Sizes { get; }

        private ConsoleLogger Log { get; }

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
            var runner = new BenchmarkRunner(Sizes, Options.Number, Options.Filename);
            runner.Log = Log;
            runner.Run();
        }

        private class ConsoleLogger : ILogger
        {
            public void Write(string message)
            {
                Console.Write(message);
            }

            public void WriteLine(string message)
            {
                Console.WriteLine(message);
            }
            public void WriteLine(object obj)
            {
                Console.WriteLine(obj);
            }
        }
    }
}
