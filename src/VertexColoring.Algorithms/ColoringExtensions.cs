using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using VertexColoring.Graphs;

namespace VertexColoring.Algorithms
{
    /// <summary>
    /// Provides <see cref="Graph"/> extension methods that calculate vertex coloring using different algorithms.
    /// </summary>
    public static class ColoringExtensions
    {
        /// <summary>
        /// Colors graph vertices greedily processing them in order of <see cref="Graph.Vertices"/>.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorGreedily(this Graph graph)
        {
            var adjacency = new VertexAdjacency(graph);
            return graph.ColorGreedily(adjacency);
        }

        /// <summary>
        /// Colors graph vertices greedily processing them in order of <see cref="Graph.Vertices"/>.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <param name="adjacency"><paramref name="graph"/> adjacency to save calculations
        /// when doing multiple colorings.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorGreedily(this Graph graph, VertexAdjacency adjacency)
        {
            return graph.ColorGreedilyWithVertexOrder(graph.Vertices, adjacency);
        }

        /// <summary>
        /// Colors graph vertices greedily processing them in order of <paramref name="verticesOrdered"/>.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <param name="verticesOrdered">Ordering of graph vertices to use in algorithm.</param>
        /// <param name="adjacency"><paramref name="graph"/> adjacency to save calculations
        /// when doing multiple colorings.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorGreedilyWithVertexOrder(this Graph graph, IEnumerable<Vertex> verticesOrdered,
            VertexAdjacency adjacency)
        {
            var maxDegree = adjacency.AdjacentVertices.Max(p => p.Value.Count);
            var colors = Enumerable.Range(1, maxDegree + 2);
            var coloring = new MutableGraphColoring(graph);
            foreach (var vertex in verticesOrdered)
            {
                var adjacentVertices = adjacency.AdjacentVertices[vertex];
                var usedColors = adjacentVertices.Select(v => coloring[v]).Where(c => c != null).Select(c => c.Value);
                var color = colors.Except(usedColors).First();
                coloring[vertex] = color;
            }
            return coloring.ToImmutable();
        }

        /// <summary>
        /// Colors graph vertices greedily processing them in descending order of vertex degree.
        /// Variant of <see cref="ColorGreedily(Graph)"/> with another vertex processing order.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <param name="adjacency"><paramref name="graph"/> adjacency to save calculations
        /// when doing multiple colorings.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorLargestFirst(this Graph graph, VertexAdjacency adjacency)
        {
            var verticesLf = graph.Vertices
                .Select(v => v.ToDegreeTuple(adjacency))
                .OrderByDescending(t => t.degree)
                .Select(t => t.vertex)
                .ToImmutableArray();
            return graph.ColorGreedilyWithVertexOrder(verticesLf, adjacency);
        }

        /// <summary>
        /// Colors graph vertices greedily processing them in ascending order of vertex degree.
        /// Variant of <see cref="ColorGreedily(Graph)"/> with another vertex processing order.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <param name="adjacency"><paramref name="graph"/> adjacency to save calculations
        /// when doing multiple colorings.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorSmallestFirst(this Graph graph, VertexAdjacency adjacency)
        {
            var verticesLf = graph.Vertices
                .Select(v => v.ToDegreeTuple(adjacency))
                .OrderBy(t => t.degree)
                .Select(t => t.vertex)
                .ToImmutableArray();
            return graph.ColorGreedilyWithVertexOrder(verticesLf, adjacency);
        }

        /// <summary>
        /// Colors graph using G.I.S (Greedy Independent Sets) algorithm. See remarks.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        /// <remarks>
        /// GIS algorithm:
        /// <list type="number">
        /// <item>Take induced subgraph S by removing all colored vertices from base graph G.</item>
        /// <item>Take from S a vertex v with maximum degree and color it.</item>'
        /// <item>S:= Subgraph induced from S with removed vertex v.</item>
        /// <item>If S is not empty, return to 2.</item>
        /// <item>Increase color.</item>
        /// <item>If not all vertices are colored, return to 1.</item>
        /// </list>
        /// </remarks>
        public static GraphColoring ColorGreedyIndependentSets(this Graph graph)
        {
            return graph.ColorGreedyIndependentSets(graph.Adjacency());
        }

        /// <summary>
        /// Colors graph using G.I.S (Greedy Independent Sets) algorithm. See remarks in <see cref="ColorGreedyIndependentSets(Graph)"/>.
        /// </summary>
        /// <param name="graph">Graph to be colored.</param>
        /// <param name="adjacency"><paramref name="graph"/> adjacency to save calculations
        /// when doing multiple colorings.</param>
        /// <returns>Vertex coloring of provided graph.</returns>
        public static GraphColoring ColorGreedyIndependentSets(this Graph graph, VertexAdjacency adjacency)
        {
            var maxDegree = adjacency.AdjacentVertices.Max(p => p.Value.Count);
            var color = 1;
            var coloring = new MutableGraphColoring(graph);
            while (coloring.VertexColors.Count < graph.Vertices.Count)
            {
                var induced = adjacency.InducedSubgraphByRemovingVertices(coloring.VertexColors.Keys);
                var inducedAdjacency = induced.Adjacency();
                while (induced.Subgraph.Vertices.Count > 0)
                {
                    var vertex = induced.Subgraph.MinDegreeVertex(inducedAdjacency.VertexDegrees());
                    coloring[vertex] = color;
                    induced = inducedAdjacency.InducedSubgraphByRemovingVertices(inducedAdjacency.AdjacentVertices[vertex].Add(vertex));
                    inducedAdjacency = induced.Adjacency();
                }
                color++;
            }
            return coloring.ToImmutable();
        }

        /// <summary>
        /// Optimizes color weights by grouping vertices by color, ordering the groups by count,
        /// and assigning colors in rising order to groups of vertices as ordered,
        /// so that the largest group has the smallest color.
        /// </summary>
        /// <param name="coloring">Coloring to be optimized.</param>
        /// <returns>A new, optimized coloring (new instance).</returns>
        public static GraphColoring OptimizeByWeighting(this GraphColoring coloring)
        {
            var colors = coloring.VertexColors
                .GroupBy(p => p.Value, p => p.Key)
                .Select(g => (group: g, count: g.Count()))
                .OrderByDescending(t => t.count)
                .SelectMany((t, i) => t.group.Select(v => (vertex: v, color: i + 1)))
                .ToImmutableSortedDictionary(t => t.vertex, t => t.color);
            return new GraphColoring(coloring.Graph, colors);
        }

        private static (Vertex vertex, int degree) ToDegreeTuple(this Vertex v, VertexAdjacency adjacency)
        {
            return (v, adjacency.AdjacentVertices[v].Count);
        }
    }
}
