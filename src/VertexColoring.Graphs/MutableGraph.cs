using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// A mutable version of <see cref="Graph"/>. Easily transformable to immutable 
    /// instance using <see cref="GraphExtensions.ToImmutable(MutableGraph)"/> extension method.
    /// </summary>
    public sealed class MutableGraph
    {
        /// <summary>
        /// Gets a list of this graph's vertices. Initialized to empty list.
        /// </summary>
        public List<MutableVertex> Vertices { get; } = new List<MutableVertex>();

        /// <summary>
        /// Gets a list of this graph's edges. Initialized to empty list.
        /// </summary>
        public List<MutableEdge> Edges { get; } = new List<MutableEdge>();
    }
}
