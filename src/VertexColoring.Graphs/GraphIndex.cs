using System.Collections.Immutable;

namespace VertexColoring.Graphs
{
    public sealed class GraphIndex
    {
        public GraphIndex(Graph graph)
        {
            Graph = graph;
            Vertices = Graph.Vertices.ToImmutableSortedDictionary(v => v.Id, v => v);
        }

        public Graph Graph { get; }
        
        public ImmutableSortedDictionary<long, Vertex> Vertices { get; }
    }
}
