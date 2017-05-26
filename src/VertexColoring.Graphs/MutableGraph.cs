using System;
using System.Collections.Generic;
using System.Text;

namespace VertexColoring.Graphs
{
    public sealed class MutableGraph
    {
        public List<MutableVertex> Vertices { get; } = new List<MutableVertex>();

        public List<MutableEdge> Edges { get; } = new List<MutableEdge>();
    }
}
