using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    public sealed class MutableGraphColoring
    {
        public MutableGraphColoring(Graph graph)
        {
            Graph = graph;
        }

        public Graph Graph { get; }

        public IReadOnlyDictionary<Vertex, int> VertexColors => VertexColorsMutable;

        private Dictionary<Vertex, int> VertexColorsMutable { get; } = new Dictionary<Vertex, int>();

        public int? this[Vertex vertex]
        {
            get => VertexColorsMutable.TryGetValue(vertex, out var color) ? color : default(int?);
            set
            {
                if (value is null)
                {
                    VertexColorsMutable.Remove(vertex);
                }
                else
                {
                    VertexColorsMutable[vertex] = value.Value;
                }
            }
        }
    }
}
