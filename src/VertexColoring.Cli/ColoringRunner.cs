using System.IO;
using VertexColoring.Graphs;
using VertexColoring.Algorithms;
using System;

namespace VertexColoring.Cli
{
    class ColoringRunner
    {
        public int VertexCount { get; set; }

        public int EdgeCount { get; set; }

        public int Index { get; set; }

        public string FilenameFormat { get; set; }

        private Graph Graph { get; set; }

        private GraphAdjacency Adjacency { get; set; }

        public string Filename => string.Format(FilenameFormat, Index, VertexCount, EdgeCount);

        public Algorithm Algorithm { get; set; }
        
        public void Setup()
        {
            using (var reader = new StreamReader(File.OpenRead(Filename)))
            {
                Graph = reader.ReadTgfGraph();
            }
            Adjacency = Graph.Adjacency();
        }

        public GraphColoring Color()
        {
            var coloring = ColorUnoptimized();
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }

        private GraphColoring ColorUnoptimized()
        {
            switch (Algorithm)
            {
                case Algorithm.Simple:
                    return Graph.ColorGreedily(Adjacency);
                case Algorithm.LF:
                    return Graph.ColorLargestFirst(Adjacency);
                case Algorithm.SF:
                    return Graph.ColorSmallestFirst(Adjacency);
                case Algorithm.GIS:
                    return Graph.ColorGreedyIndependentSets(Adjacency);
                default:
                    throw new ArgumentException("Unknown algorithm selected");
            }
        }
        
        public GraphColoring ColorGreedySimple()
        {
            var coloring = Graph.ColorGreedily(Adjacency);
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }
        
        public GraphColoring ColorGreedyLF()
        {
            var coloring = Graph.ColorLargestFirst(Adjacency);
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }
        
        public GraphColoring ColorGreedySF()
        {
            var coloring = Graph.ColorSmallestFirst(Adjacency);
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }
        
        public GraphColoring ColorGreedyGIS()
        {
            
            var coloring = Graph.ColorGreedyIndependentSets(Adjacency);
            var optimized = coloring.OptimizeByWeighting();
            return optimized;
        }
    }
}
