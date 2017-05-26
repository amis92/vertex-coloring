using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace VertexColoring.Graphs
{
    public sealed class GraphDegree
    {
        public GraphDegree(Graph graph, GraphAdjacency adjacency)
        {
            Degrees = graph.Vertices.ToImmutableSortedDictionary(v => v, v => v.Degree(adjacency));
        }

        public Graph Graph { get; }

        public ImmutableSortedDictionary<Vertex, int> Degrees { get; }

        public int this[Vertex vertex] => Degrees[vertex];
    }
}
