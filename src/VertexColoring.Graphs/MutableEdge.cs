using System.Collections;
using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    public sealed class MutableEdge : IEnumerable<MutableVertex>
    {
        public MutableVertex Vertex1 { get; set; }

        public MutableVertex Vertex2 { get; set; }

        public string Label { get; set; }

        public override string ToString()
        {
            return $"{Vertex1?.Id.ToString() ?? "-"} {Vertex2?.Id.ToString() ?? "-"} {Label}";
        }

        public IEnumerator<MutableVertex> GetEnumerator()
        {
            yield return Vertex1;
            yield return Vertex2;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
