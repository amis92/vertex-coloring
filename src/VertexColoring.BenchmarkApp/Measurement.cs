using System;
using VertexColoring.Graphs;

namespace VertexColoring.BenchmarkApp
{
    class Measurement
    {
        public string AlgorithmName { get; set; }
        public TimeSpan Duration { get; set; }
        public GraphColoring Coloring { get; internal set; }
        public string Filename { get; internal set; }
    }
}
