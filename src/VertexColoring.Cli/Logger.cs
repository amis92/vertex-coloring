using System.IO;

namespace VertexColoring.Cli
{
    [Record]
    partial class Logger
    {
        public TextWriter Debug { get; }

        public TextWriter Info { get; }
    }
}
