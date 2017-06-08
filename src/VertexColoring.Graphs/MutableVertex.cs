namespace VertexColoring.Graphs
{
    /// <summary>
    /// A mutable version of <see cref="Vertex"/>. Easily transformable to immutable instance
    /// using the <see cref="GraphExtensions.ToImmutable(MutableVertex)"/> extension method.
    /// </summary>
    public sealed class MutableVertex
    {
        /// <summary>
        /// Gets or sets the unique identifier of this vertex.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets an arbitrary label for this vertex.
        /// </summary>
        public string Label { get; set; }
    }
}
