using System.Collections.Immutable;

namespace VertexColoring.Graphs
{
    [Record]
    public sealed partial class GraphColoring
    {
        public Graph Graph { get; }

        public IImmutableDictionary<Vertex, int> VertexColors { get; }
    }
}
