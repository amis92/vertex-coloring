using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    public class InducedSubgraph
    {
        public InducedSubgraph(VertexAdjacency adjacency, IEnumerable<Vertex> removedVertices)
        {
            OriginalGraph = adjacency.Graph;
            OriginalAdjacency = adjacency;
            RemovedVertices = removedVertices.ToImmutableSortedSet();
            RemovedEdges = removedVertices.SelectMany(v => adjacency.IncidentEdges[v]).ToImmutableSortedSet();
            var vertices = adjacency.Graph.Vertices.Except(RemovedVertices);
            var edges = adjacency.Graph.Edges.Except(RemovedEdges);
            Subgraph = new Graph(vertices, edges);
        }

        public Graph OriginalGraph { get; }

        public VertexAdjacency OriginalAdjacency { get; }

        public Graph Subgraph { get; }

        public ImmutableSortedSet<Edge> RemovedEdges { get; }

        public ImmutableSortedSet<Vertex> RemovedVertices { get; }
    }
}
