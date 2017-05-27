using EntryPoint;

namespace VertexColoring.GraphGenerator
{
    partial class RandomGraphCommand
    {
        public class CliArguments : BaseCliArguments
        {
            public CliArguments() : base("random")
            {
            }

            [Required]
            [OptionParameter(ShortName: 'v', LongName: "vertexCount")]
            [Help("Vertex count, must be non-negative.")]
            public int VertexCount { get; set; }

            [Required]
            [OptionParameter(ShortName: 'e', LongName: "edgeCount")]
            [Help("Edge count, must be greater than v - 1 and non-negative.")]
            public int EdgeCount { get; set; }

            [OptionParameter(ShortName: 'o', LongName: "output")]
            [Help("Output file path with {0} for graph index when generating more than one." +
                " If {0} is not in pattern, it is inserted at the end of pattern." +
                " Additional usable parameters: {1} - vertexCount, {2} - edgeCount." +
                " If pattern doesn't end with .tgf, the suffix is added automatically. Default graph{0}.tgf")]
            public string OutputFilename { get; set; } = "graph{0}.tgf";

            [Option(ShortName: 'd', LongName: "debug")]
            [Help("Verbose behavior. Prints detailed output of performed operations when set.")]
            public bool Debug { get; set; } = false;

            [OptionParameter(ShortName: 'n', LongName: "number")]
            [Help("Number of graphs to generate. Default 1.")]
            public int Number { get; set; } = 1;
        }
    }
}
