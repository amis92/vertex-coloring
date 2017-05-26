using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    [Record]
    public sealed partial class GraphColoring
    {
        public Graph Graph { get; }

        public IImmutableDictionary<Vertex, int> VertexColors { get; }

        public int? this[Vertex vertex]
        {
            get => VertexColors.TryGetValue(vertex, out var color) ? color : default(int?);
        }

        public int? this[long vertexId]
        {
            get => VertexColors.Keys.FirstOrDefault(v => v.Id == vertexId) is Vertex vertex && VertexColors.TryGetValue(vertex, out var color) ? color : default(int?);
        }
    }
}
