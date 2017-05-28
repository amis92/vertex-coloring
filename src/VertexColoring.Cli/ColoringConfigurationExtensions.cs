using System;
using VertexColoring.Algorithms;
using VertexColoring.Graphs;

namespace VertexColoring.Cli
{
    static class ColoringConfigurationExtensions
    {
        public static GraphColoring Color(this ColoringConfiguration config)
        {
            var coloring = config.ColorUnoptimized();
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }

        private static GraphColoring ColorUnoptimized(this ColoringConfiguration config)
        {
            switch (config.Algorithm)
            {
                case Algorithm.Simple:
                    return config.Graph.ColorGreedily(config.Adjacency);
                case Algorithm.LF:
                    return config.Graph.ColorLargestFirst(config.Adjacency);
                case Algorithm.SF:
                    return config.Graph.ColorSmallestFirst(config.Adjacency);
                case Algorithm.GIS:
                    return config.Graph.ColorGreedyIndependentSets(config.Adjacency);
                default:
                    throw new ArgumentException("Unknown algorithm selected");
            }
        }
    }
}
