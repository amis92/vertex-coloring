using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Associates vertices and their neighbours.
    /// </summary>
    public sealed class VertexAdjacency
    {
        /// <summary>
        /// Calculates and creates adjacency for given graph.
        /// </summary>
        /// <param name="graph">Graph to calculate vertex adjacency in.</param>
        public VertexAdjacency(Graph graph)
        {
            Graph = graph;
            var incidentEdgesMutable = new Dictionary<Vertex, HashSet<Edge>>();
            foreach (var edge in graph.Edges)
            {
                addEdgeVertex(edge, edge.Vertex1);
                addEdgeVertex(edge, edge.Vertex2);
            }
            IncidentEdges = graph.Vertices
                .ToImmutableSortedDictionary(
                v => v,
                v => incidentEdgesMutable.TryGetValue(v, out var set) ? (IImmutableSet<Edge>)set.ToImmutableHashSet() : ImmutableHashSet.Create<Edge>());
            AdjacentVertices = CreateAdjacentVertices();

            void addEdgeVertex(Edge edge, Vertex vertex)
            {
                if (incidentEdgesMutable.TryGetValue(vertex, out var edgeSet))
                {
                    edgeSet.Add(edge);
                }
                else
                {
                    edgeSet = new HashSet<Edge>
                    {
                        edge
                    };
                    incidentEdgesMutable.Add(vertex, edgeSet);
                }
            }
        }

        /// <summary>
        /// Calculates and creates adjacency for given graph. Optimized for induced subgraphs
        /// in comparison to <see cref="VertexAdjacency(Graph)"/>.
        /// </summary>
        /// <param name="subgraph">Graph to calculate vertex adjacency in.</param>
        public VertexAdjacency(InducedSubgraph subgraph)
        {
            Graph = subgraph.Subgraph;
            IncidentEdges = Graph.Vertices.ToImmutableSortedDictionary(v => v, v => subgraph.OriginalAdjacency.IncidentEdges[v].Except(subgraph.RemovedEdges));
            AdjacentVertices = CreateAdjacentVertices();
        }

        /// <summary>
        /// Gets the graph that this adjacency is of.
        /// </summary>
        public Graph Graph { get; }

        /// <summary>
        /// Gets the association of vertices and their neighbours.
        /// </summary>
        public IImmutableDictionary<Vertex, IImmutableSet<Vertex>> AdjacentVertices { get; }

        /// <summary>
        /// Gets the association of vertices and their incident edges.
        /// </summary>
        public IImmutableDictionary<Vertex, IImmutableSet<Edge>> IncidentEdges { get; }
        
        private IImmutableDictionary<Vertex, IImmutableSet<Vertex>> CreateAdjacentVertices()
        {
            return Graph.Vertices.ToImmutableSortedDictionary(v => v, v => (IImmutableSet<Vertex>)IncidentEdges[v].Select(e => e.OtherVertex(v)).ToImmutableHashSet());
        }
    }
}
