using EntryPoint;
using System.Collections.Generic;

namespace VertexColoring.Cli
{
    sealed partial class ColoringCommand
    {
        public class CliArguments : BaseCliArguments
        {
            public CliArguments() : base("color")
            {
            }

            [OptionParameter(ShortName: 'v', LongName: "vertexCounts")]
            [Help("Vertex counts, must be non-negative.")]
            public List<int> VertexCount { get; set; } = new List<int>();

            [OptionParameter(ShortName: 'e', LongName: "edgeCounts")]
            [Help("Edge counts, must be greater than v - 1 and non-negative.")]
            public List<int> EdgeCount { get; set; } = new List<int>();

            [OptionParameter(ShortName: 'f', LongName: "filename")]
            [Help("Filename path format with {0} for graph index (0..n)." +
                " Additional usable parameters: {1} - vertexCount, {2} - edgeCount." +
                " Default graph{0}.tgf")]
            public string Filename { get; set; } = "graph{0}.tgf";

            [Option(ShortName: 'd', LongName: "debug")]
            [Help("Verbose behavior. Prints detailed output of performed operations when set.")]
            public bool Debug { get; set; } = false;

            [OptionParameter(ShortName: 'n', LongName: "number")]
            [Help("Number of graphs to benchmark (for each pair of 'v' and 'e' values). Default 1.")]
            public int Number { get; set; } = 1;

            [OptionParameter(ShortName: 'o', LongName: "output")]
            [Help("File to save summary to.")]
            public string OutputFilename { get; set; }
        }
    }
}
