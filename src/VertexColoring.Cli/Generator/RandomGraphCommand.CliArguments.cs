using EntryPoint;
using System.Collections.Generic;

namespace VertexColoring.Cli
{
    partial class RandomGraphCommand
    {
        public class CliArguments : BaseCliArguments
        {
            public CliArguments() : base("random")
            {
            }

            [Required]
            [OptionParameter(ShortName: 'v', LongName: "vertexCounts")]
            [Help("Vertex counts, must be non-negative.")]
            public List<int> VertexCounts { get; set; } = new List<int>();

            [Required]
            [OptionParameter(ShortName: 'e', LongName: "edgeCounts")]
            [Help("Edge counts, must be greater than v - 1 and non-negative.")]
            public List<int> EdgeCounts { get; set; } = new List<int>();

            [OptionParameter(ShortName: 'o', LongName: "output")]
            [Help("Output file path with {0} for graph index when generating more than one." +
                " If {0} is not in pattern, it is inserted at the end of pattern." +
                " Additional usable parameters: {1} - vertexCount, {2} - edgeCount." +
                " If pattern doesn't end with .tgf, the suffix is added automatically. Default graph-v{1}-e{2}-{0}.tgf")]
            public string OutputFilename { get; set; } = "graph-v{1}-e{2}-{0}.tgf";

            [Option(ShortName: 'd', LongName: "debug")]
            [Help("Verbose behavior. Prints detailed output of performed operations when set.")]
            public bool Debug { get; set; } = false;

            [OptionParameter(ShortName: 'n', LongName: "number")]
            [Help("Number of graphs to generate for each (v,e) value pair. Default 1.")]
            public int Number { get; set; } = 1;

            [OptionParameter(ShortName: 's', LongName: "seed")]
            [Help("Random seed. Provide to get deterministic output.")]
            public int? RandomSeed { get; set; }
        }
    }
}
