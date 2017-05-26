using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace VertexColoring.Graphs
{
    public static class GraphExtensions
    {
        public static Graph ToImmutable(this MutableGraph mutable)
        {
            var vertices = mutable.Vertices.Select(v => v.ToImmutable()).ToImmutableSortedSet();
            var index = new Graph(vertices, ImmutableSortedSet.Create<Edge>()).Index();
            var edges = mutable.Edges.Select(e => e.ToImmutable(index)).ToImmutableSortedSet();
            return new Graph(vertices, edges);
        }

        public static Vertex OtherVertex(this Edge e, Vertex v)
        {
            return e.Vertex1 == v ? e.Vertex2 : e.Vertex1;
        }

        public static Vertex ToImmutable(this MutableVertex mutable)
        {
            return new Vertex(mutable.Id, mutable.Label);
        }

        public static Edge ToImmutable(this MutableEdge mutable)
        {
            return new Edge(mutable.Vertex1.ToImmutable(), mutable.Vertex2.ToImmutable(), mutable.Label);
        }

        public static Edge ToImmutable(this MutableEdge mutable, GraphIndex index)
        {
            return new Edge(index.Vertices[mutable.Vertex1.Id], index.Vertices[mutable.Vertex2.Id], mutable.Label);
        }

        public static GraphIndex Index(this Graph graph) => new GraphIndex(graph);
    }
}
