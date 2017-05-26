using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    public class GraphAdjacency
    {
        public GraphAdjacency(Graph graph)
        {
            Graph = graph;
            var incidentEdgesMutable = new Dictionary<Vertex, HashSet<Edge>>();
            foreach (var edge in graph.Edges)
            {
                addEdgeVertex(edge, edge.Vertex1);
                addEdgeVertex(edge, edge.Vertex2);
            }
            IncidentEdges = graph.Vertices.ToImmutableSortedDictionary(v => v, v => incidentEdgesMutable.TryGetValue(v, out var set) ? (IImmutableSet<Edge>)set.ToImmutableHashSet() : ImmutableHashSet.Create<Edge>());
            AdjacentVertices = graph.Vertices.ToImmutableSortedDictionary(v => v, v => (IImmutableSet<Vertex>)IncidentEdges[v].Select(e => e.OtherVertex(v)).ToImmutableHashSet());

            void addEdgeVertex(Edge edge, Vertex vertex)
            {
                if (incidentEdgesMutable.TryGetValue(vertex, out var edgeSet))
                {
                    edgeSet.Add(edge);
                }
                else
                {
                    edgeSet = new HashSet<Edge>();
                    edgeSet.Add(edge);
                    incidentEdgesMutable.Add(vertex, edgeSet);
                }
            }
        }

        public Graph Graph { get; }

        public IImmutableDictionary<Vertex, IImmutableSet<Vertex>> AdjacentVertices { get; }

        public IImmutableDictionary<Vertex, IImmutableSet<Edge>> IncidentEdges { get; }
    }
}
