using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Contains useful extension methods to simplify using <see cref="Graph"/>s and rest of the family
    /// from this namespace.
    /// </summary>
    public static class GraphExtensions
    {
        /// <summary>
        /// Transforms this mutable instance (as well as it's properties) to immutable copy.
        /// </summary>
        /// <param name="coloring">Instance to transform.</param>
        /// <returns>Immutable copy of provided instance.</returns>
        public static GraphColoring ToImmutable(this MutableGraphColoring coloring)
        {
            return new GraphColoring(coloring.Graph, coloring.VertexColors.ToImmutableDictionary());
        }

        /// <summary>
        /// Calculates count of unique colors used in <paramref name="coloring"/>.
        /// </summary>
        /// <param name="coloring">Coloring to count colors in.</param>
        /// <returns>Count of unique colors.</returns>
        public static int ColorsUsed(this GraphColoring coloring)
        {
            return coloring.VertexColors.Values.Distinct().Count();
        }

        /// <summary>
        /// Creates an adjacency instance for the <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">Graph to create adjacency for.</param>
        /// <returns>Graph's adjacency.</returns>
        public static VertexAdjacency Adjacency(this Graph graph) => new VertexAdjacency(graph);

        /// <summary>
        /// Creates an adjacency instance for the <paramref name="graph"/>.
        /// It's optimized in comparison to creating adjacency directly from an actual <see cref="InducedSubgraph.Subgraph"/>
        /// of <paramref name="graph"/>.
        /// </summary>
        /// <param name="graph">Graph to create adjacency for.</param>
        /// <returns>Graph's adjacency.</returns>
        public static VertexAdjacency Adjacency(this InducedSubgraph graph) => new VertexAdjacency(graph);

        /// <summary>
        /// Creates container for graph's vertex degrees.
        /// </summary>
        /// <param name="adjacency"></param>
        /// <returns></returns>
        public static VertexDegrees VertexDegrees(this VertexAdjacency adjacency) => new VertexDegrees(adjacency);

        public static Vertex MaxDegreeVertex(this Graph graph, VertexDegrees degrees)
        {
            return graph.Vertices.Aggregate((v1, v2) => degrees[v1] > degrees[v2] ? v1 : v2);
        }

        public static Vertex MinDegreeVertex(this Graph graph, VertexDegrees degrees)
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

        public static InducedSubgraph InducedSubgraphByRemovingVertices(this VertexAdjacency adjacency, IEnumerable<Vertex> removedVertices)
        {
            return new InducedSubgraph(adjacency, removedVertices);
        }

        public static InducedSubgraph InducedSubgraphByRemovingVertices(this Graph graph, VertexAdjacency adjacency, IEnumerable<Vertex> removedVertices)
        {
            return adjacency.InducedSubgraphByRemovingVertices(removedVertices);
        }

        public static InducedSubgraph InducedSubgraph(this VertexAdjacency adjacency, ImmutableSortedSet<Vertex> vertices)
        {
            return new InducedSubgraph(adjacency, adjacency.Graph.Vertices.Except(vertices));
        }

        public static InducedSubgraph InducedSubgraph(this Graph graph,  VertexAdjacency adjacency, ImmutableSortedSet<Vertex> vertices)
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

        public static int Degree(this Vertex vertex, VertexAdjacency adjacency) => adjacency.AdjacentVertices[vertex].Count;
    }
}
