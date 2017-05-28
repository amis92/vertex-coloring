using EntryPoint;
using System.Collections.Generic;
using System.Linq;

namespace VertexColoring.Cli
{
    sealed partial class BenchmarkCommand
    {
        public class CliArguments : BaseCliArguments
        {
            public CliArguments() : base("benchmark")
            {
            }

            [OptionParameter(ShortName: 'v', LongName: "vertexCounts")]
            [Help("Vertex counts, must be non-negative.")]
            public List<int> VertexCounts { get; set; } = new List<int>();

            [OptionParameter(ShortName: 'e', LongName: "edgeCounts")]
            [Help("Edge counts, must be greater than v - 1 and non-negative.")]
            public List<int> EdgeCounts { get; set; } = new List<int>();

            [OptionParameter(ShortName: 'f', LongName: "filename")]
            [Help("Filename path format with {0} for graph index (0..n)." +
                " Additional usable parameters: {1} - vertexCount, {2} - edgeCount." +
                " Default graph-v{1}-e{2}-{0}.tgf")]
            public string Filename { get; set; } = "graph-v{1}-e{2}-{0}.tgf";

            [Option(ShortName: 'd', LongName: "debug")]
            [Help("Verbose behavior. Prints detailed output of performed operations when set.")]
            public bool Debug { get; set; } = false;

            [OptionParameter(ShortName: 'n', LongName: "number")]
            [Help("Number of graphs to benchmark (for each pair of 'v' and 'e' values). Default 1.")]
            public int Number { get; set; } = 1;

            [OptionParameter(ShortName: 'a', LongName: "algorithms")]
            [Help("Algorithms to use. (0/Simple (Simple Greedy), 1/LF (Largest First greedy)," +
                " 2/SF (Smallest First greedy), 3/GIS (Greedy Independent Sets). e.g. '-a=0,3,4' or '-a 0,SF,GIS')." +
                " Default is all.")]
            public List<Algorithm> Algorithms { get; set; }
                = new[] { Algorithm.Simple, Algorithm.LF, Algorithm.SF, Algorithm.GIS }.ToList();
        }
    }
}
