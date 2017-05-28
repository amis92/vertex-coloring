using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VertexColoring.BenchmarkApp
{
    [Record]
    partial class Logger
    {
        public TextWriter Debug { get; }

        public TextWriter Info { get; }
    }
}
