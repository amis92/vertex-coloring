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

        public override string ToString()
        {
            return $"Calculated '{AlgorithmName}' for '{Filename}'" +
                $" in {Duration} ({Duration.TotalMilliseconds}ms)," +
                $" total color cost: {Coloring.SummaryCost()}" +
                $" (for {Coloring.Graph.Vertices.Count} vertices)";
        }
    }
}
