using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VertexColoring.Graphs;

namespace VertexColoring.Algorithms
{
    public class GreedyColoring
    {
        public void Do(Graph graph)
        {
            var adjacency = new GraphAdjacency(graph);
            var maxDegree = adjacency.AdjacentVertices.Max(p => p.Value.Count);
            var colors = Enumerable.Range(1, maxDegree).ToImmutableSortedSet();
            var coloring = new GraphColoringMutable(graph);
            foreach (var vertex in graph.Vertices)
            {
                var adjacentVertices = adjacency.AdjacentVertices[vertex];
                var usedColors = adjacentVertices.Select(v => coloring[v]).Where(c => c != null).Select(c => c.Value);
                var color = colors.Except(usedColors).First();
            }
        }
    }
}
