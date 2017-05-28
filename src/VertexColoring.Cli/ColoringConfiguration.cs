using System.IO;
using VertexColoring.Graphs;

namespace VertexColoring.Cli
{
    class ColoringConfiguration
    {
        public int VertexCount { get; set; }

        public int EdgeCount { get; set; }

        public int Index { get; set; }

        public string FilenameFormat { get; set; }

        public Graph Graph { get; private set; }

        public GraphAdjacency Adjacency { get; private set; }

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
    }
}
