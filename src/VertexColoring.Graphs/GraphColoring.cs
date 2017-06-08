using System.Collections.Immutable;
using System.Linq;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Associates vertices with colors represented by integers. Exposes indexer
    /// that returns a color or null if vertex has no color in this coloring instance.
    /// </summary>
    [Record]
    public sealed partial class GraphColoring
    {
        /// <summary>
        /// Gets the graph that this coloring is of.
        /// </summary>
        public Graph Graph { get; }

        /// <summary>
        /// Gets the underlying dictionary of vertex colors. May not contain all the <see cref="Graph"/>'s vertices.
        /// Those not contained have no color in this coloring.
        /// </summary>
        public IImmutableDictionary<Vertex, int> VertexColors { get; }

        /// <summary>
        /// Gets the color value of <paramref name="vertex"/>.
        /// </summary>
        /// <param name="vertex">Vertex to get color of.</param>
        /// <returns>A color value or null if vertex has no color.</returns>
        public int? this[Vertex vertex]
        {
            get => VertexColors.TryGetValue(vertex, out var color) ? color : default(int?);
        }

        /// <summary>
        /// Gets the color value of a vertex with Id = <paramref name="vertexId"/>.
        /// </summary>
        /// <param name="vertexId">Id of vertex to get color of.</param>
        /// <returns>A color value or null if vertex has no color.</returns>
        public int? this[long vertexId]
        {
            get => VertexColors.Keys.FirstOrDefault(v => v.Id == vertexId) is Vertex vertex && VertexColors.TryGetValue(vertex, out var color) ? color : default(int?);
        }

        private int? _summaryCost;
        /// <summary>
        /// Gets the summary cost that this coloring generates, meaning a sum of all colored vertices' color values.
        /// </summary>
        public int SummaryCost => (_summaryCost ?? (_summaryCost = VertexColors.Sum(c => c.Value))).Value;
    }
}
