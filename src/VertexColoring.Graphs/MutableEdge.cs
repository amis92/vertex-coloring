using System.Collections;
using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// A mutable version of <see cref="Edge"/>. Easily transformable to immutable instance
    /// using the <see cref="GraphExtensions.ToImmutable(MutableEdge)"/> extension method.
    /// Can be enumerated same as immutable instance.
    /// </summary>
    public sealed class MutableEdge : IEnumerable<MutableVertex>
    {
        /// <summary>
        /// Gets or sets one end of the edge.
        /// </summary>
        public MutableVertex Vertex1 { get; set; }

        /// <summary>
        /// Gets or sets other end of the edge.
        /// </summary>
        public MutableVertex Vertex2 { get; set; }

        /// <summary>
        /// Gets or sets an arbitrary label.
        /// </summary>
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
