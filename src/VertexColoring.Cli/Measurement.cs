using System;
using VertexColoring.Graphs;

namespace VertexColoring.Cli
{
    class Measurement
    {
        public Algorithm Algorithm { get; set; }
        public TimeSpan Duration { get; set; }
        public GraphColoring Coloring { get; internal set; }
        public string Filename { get; internal set; }

        public override string ToString()
        {
            return $"Calculated '{Algorithm}' for '{Filename}'" +
                $" in {Duration} ({Duration.TotalMilliseconds}ms)," +
                $" total color cost: {Coloring.SummaryCost}" +
                $" (for {Coloring.Graph.Vertices.Count} vertices)";
        }
    }
}
