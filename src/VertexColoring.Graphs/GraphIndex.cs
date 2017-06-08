using System.Collections.Immutable;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Represents index of graph vertices by their id. Provides indexer for convenience.
    /// </summary>
    public sealed class GraphIndex
    {
        /// <summary>
        /// Creates new instance of index for given graph.
        /// </summary>
        /// <param name="graph">Graph to create index for.</param>
        public GraphIndex(Graph graph)
        {
            Graph = graph;
            Vertices = Graph.Vertices.ToImmutableSortedDictionary(v => v.Id, v => v);
        }

        /// <summary>
        /// Gets the graph that this index is of.
        /// </summary>
        public Graph Graph { get; }
        
        /// <summary>
        /// Gets the association of vertex ids and vertices with that id.
        /// </summary>
        public ImmutableSortedDictionary<long, Vertex> Vertices { get; }

        /// <summary>
        /// Gets a vertex with a given id. Throws exception if id was not found.
        /// </summary>
        /// <param name="id">Id of vertex to be found.</param>
        /// <returns>Vertex with given id.</returns>
        public Vertex this[long id] => Vertices[id];
    }
}
