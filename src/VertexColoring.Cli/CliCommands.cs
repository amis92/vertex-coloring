using EntryPoint;

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
            ColoringCommand.Execute(args);
        }

        [Command("random")]
        [Help("Generates random connected graph.")]
        public void Random(string[] args)
        {
            RandomGraphCommand.Execute(args);
        }

        [Command("benchmark")]
        [Help("Benchmarks coloring algorithms: simple greedy, LF (Largest First)," +
            " SF (Smallest First), GIS (Greedy Independent Sets).")]
        public void Benchmark(string[] args)
        {
            BenchmarkCommand.Execute(args);
        }
    }
}
