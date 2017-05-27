using System.IO;
using VertexColoring.Graphs;
using VertexColoring.Algorithms;

namespace VertexColoring.BenchmarkApp
{
    public class ColoringBenchmark
    {
        public int VertexCount { get; set; }

        public int EdgeCount { get; set; }

        public int Index { get; set; }

        public string FilenameFormat { get; set; }

        private Graph Graph { get; set; }

        private GraphAdjacency Adjacency { get; set; }

        public string Filename => string.Format(FilenameFormat, Index, VertexCount, EdgeCount);
        
        public void Setup()
        {
            using (var reader = new StreamReader(File.OpenRead(Filename)))
            {
                Graph = reader.ReadTgfGraph();
            }
            Adjacency = Graph.Adjacency();
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
