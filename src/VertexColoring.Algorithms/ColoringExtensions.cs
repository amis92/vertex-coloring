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
            var maxDegree = adjacency.AdjacentVertices.Max(p => p.Value.Count);
            var colors = Enumerable.Range(1, maxDegree + 2);
            var coloring = new MutableGraphColoring(graph);
            foreach (var vertex in graph.Vertices)
            {
                var adjacentVertices = adjacency.AdjacentVertices[vertex];
                var usedColors = adjacentVertices.Select(v => coloring[v]).Where(c => c != null).Select(c => c.Value);
                var color = colors.Except(usedColors).First();
                coloring[vertex] = color;
            }
            return coloring.ToImmutable();
        }
    }
}
