using EntryPoint;
using System;
using System.Diagnostics;

namespace VertexColoring.Cli
{
    public class CliCommands : BaseCliCommands
    {
        [DefaultCommand]
        [Command("color")]
        [Help("Runs coloring algorithms: simple greedy, LF (Largest First)," +
            " SF (Smallest First), GIS (Greedy Independent Sets).")]
        public void Color(string[] args)
        {
            var watch = Stopwatch.StartNew();
            ColoringCommand.Execute(args);
            Console.WriteLine($"Finished in: {watch.Elapsed}");
        }

        [Command("random")]
        [Help("Generates random connected graph.")]
        public void Random(string[] args)
        {
            var watch = Stopwatch.StartNew();
            RandomGraphCommand.Execute(args);
            Console.WriteLine($"Finished in: {watch.Elapsed}");
        }

        [Command("benchmark")]
        [Help("Benchmarks coloring algorithms: simple greedy, LF (Largest First)," +
            " SF (Smallest First), GIS (Greedy Independent Sets).")]
        public void Benchmark(string[] args)
        {
            var watch = Stopwatch.StartNew();
            BenchmarkCommand.Execute(args);
            Console.WriteLine($"Finished in: {watch.Elapsed}");
        }
    }
}
