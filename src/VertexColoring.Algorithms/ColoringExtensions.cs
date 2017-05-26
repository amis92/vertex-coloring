using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VertexColoring.Graphs;

namespace VertexColoring.Algorithms
{
    public static class ColoringExtensions
    {
        public static GraphColoring ColorGreedily(this Graph graph)
        {
            var adjacency = new GraphAdjacency(graph);
            return graph.ColorGreedily(adjacency);
        }

        public static GraphColoring ColorGreedily(this Graph graph, GraphAdjacency adjacency)
        {
            return graph.ColorGreedilyWithVertexOrder(graph.Vertices, adjacency);
        }

        public static GraphColoring ColorGreedilyWithVertexOrder(this Graph graph, IEnumerable<Vertex> verticesOrdered, GraphAdjacency adjacency)
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

        public static GraphColoring ColorLargestFirst(this Graph graph, GraphAdjacency adjacency)
        {
            var verticesLf = graph.Vertices
                .Select(v => v.ToDegreeTuple(adjacency))
                .OrderByDescending(t => t.degree)
                .Select(t => t.vertex)
                .ToImmutableArray();
            return graph.ColorGreedilyWithVertexOrder(verticesLf, adjacency);
        }

        public static GraphColoring ColorSmallestFirst(this Graph graph, GraphAdjacency adjacency)
        {
            var verticesLf = graph.Vertices
                .Select(v => v.ToDegreeTuple(adjacency))
                .OrderBy(t => t.degree)
                .Select(t => t.vertex)
                .ToImmutableArray();
            return graph.ColorGreedilyWithVertexOrder(verticesLf, adjacency);
        }

        public static GraphColoring ColorGreedyIndependentSets(this Graph graph)
        {
            return graph.ColorGreedyIndependentSets(graph.Adjacency());
        }

        public static GraphColoring ColorGreedyIndependentSets(this Graph graph, GraphAdjacency adjacency)
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
                    var vertex = induced.Subgraph.MinDegreeVertex(induced.Subgraph.Degrees(inducedAdjacency));
                    coloring[vertex] = color;
                    induced = inducedAdjacency.InducedSubgraphByRemovingVertices(inducedAdjacency.AdjacentVertices[vertex].Add(vertex));
                    inducedAdjacency = induced.Adjacency();
                }
                color++;
            }
            return coloring.ToImmutable();
        }

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

        private static (Vertex vertex, int degree) ToDegreeTuple(this Vertex v, GraphAdjacency adjacency)
        {
            return (v, adjacency.AdjacentVertices[v].Count);
        }
    }
}
