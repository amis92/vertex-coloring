using System.Collections.Immutable;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Associates vertices with their degree in a given graph. Provides indexer to simplify access.
    /// </summary>
    public sealed class VertexDegrees
    {
        public VertexDegrees(VertexAdjacency adjacency)
        {
            Degrees = adjacency.Graph.Vertices.ToImmutableSortedDictionary(v => v, v => v.Degree(adjacency));
        }

        /// <summary>
        /// Gets graph in which the degrees are counted.
        /// </summary>
        public Graph Graph { get; }

        /// <summary>
        /// Gets the underlying dictionary of vertex degrees.
        /// </summary>
        public ImmutableSortedDictionary<Vertex, int> Degrees { get; }

        /// <summary>
        /// Gets the degree of a given <paramref name="vertex"/> in <see cref="Graph"/>.
        /// </summary>
        /// <param name="vertex">Vertex to get degree of.</param>
        /// <returns>Vertex's degree.</returns>
        public int this[Vertex vertex] => Degrees[vertex];
    }
}
