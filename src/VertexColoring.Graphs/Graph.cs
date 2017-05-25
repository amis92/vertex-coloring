using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace VertexColoring.Graphs
{
    public class Graph
    {
        public IImmutableSet<Vertex> Vertices { get; set; }

        public IImmutableSet<Edge> Edges { get; set; }
    }
}
