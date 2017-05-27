using EntryPoint;

namespace VertexColoring.BenchmarkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = Cli.Execute<CliCommands>();
        }

        private class CliCommands : BaseCliCommands
        {
            [DefaultCommand]
            [Command("benchmark")]
            [Help("Benchmarks coloring algorithms: simple greedy, LF (Largest First)," +
                " SF (Smallest First), GIS (Greedy Independent Sets).")]
            public void Benchmark(string[] args)
            {
                BenchmarkCommand.Execute(args);
            }
        }
    }
}
