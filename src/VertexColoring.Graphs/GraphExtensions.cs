using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace VertexColoring.Graphs
{
    public static class GraphExtensions
    {
        public static GraphAdjacency Adjacency(this Graph graph) => new GraphAdjacency(graph);
        public static GraphAdjacency Adjacency(this InducedSubgraph graph) => new GraphAdjacency(graph);

        public static GraphDegree Degrees(this Graph graph, GraphAdjacency adjacency) => new GraphDegree(graph, adjacency);

        public static Vertex MaxDegreeVertex(this Graph graph, GraphDegree degrees)
        {
            return graph.Vertices.Aggregate((v1, v2) => degrees[v1] > degrees[v2] ? v1 : v2);
        }

        public static Vertex MinDegreeVertex(this Graph graph, GraphDegree degrees)
        {
            return graph.Vertices.Aggregate((v1, v2) => degrees[v1] < degrees[v2] ? v1 : v2);
        }

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

        public static InducedSubgraph InducedSubgraphByRemovingVertices(this GraphAdjacency adjacency, IEnumerable<Vertex> removedVertices)
        {
            return new InducedSubgraph(adjacency, removedVertices);
        }

        public static InducedSubgraph InducedSubgraphByRemovingVertices(this Graph graph, GraphAdjacency adjacency, IEnumerable<Vertex> removedVertices)
        {
            return adjacency.InducedSubgraphByRemovingVertices(removedVertices);
        }

        public static InducedSubgraph InducedSubgraph(this GraphAdjacency adjacency, ImmutableSortedSet<Vertex> vertices)
        {
            return new InducedSubgraph(adjacency, adjacency.Graph.Vertices.Except(vertices));
        }

        public static InducedSubgraph InducedSubgraph(this Graph graph,  GraphAdjacency adjacency, ImmutableSortedSet<Vertex> vertices)
        {
            // old
            // new Graph(vertices.ToImmutableSortedSet(), graph.Edges.Where(e => e.IsInInduced(vertices)).ToImmutableSortedSet())
            return adjacency.InducedSubgraph(vertices);
        }

        private static bool IsInInduced(this Edge edge, ImmutableSortedSet<Vertex> vertices)
        {
            var t = (v1: false, v2: false);
            foreach (var vertex in vertices)
            {
                t = (t.v1 || edge.Vertex1 == vertex, t.v2 || edge.Vertex2 == vertex);
                if (t.v1 && t.v2)
                {
                    return true;
                }
            }
            return false;
        }

        public static int Degree(this Vertex vertex, GraphAdjacency adjacency) => adjacency.AdjacentVertices[vertex].Count;
    }
}
