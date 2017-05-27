using System;
using System.Collections.Generic;
using System.Linq;
using TextTableFormatter;
using VertexColoring.Graphs;

namespace VertexColoring.BenchmarkApp
{
    class MeasurementPrinter
    {
        public static void PrintSummary(IEnumerable<Measurement> measurements)
        {
            // | Vertices | Algorithm | Duration [ms] |
            var table = new TextTable(3);
            table.AddCell("Vertices");
            table.AddCell("Algorithm");
            table.AddCell("Duration [ms]");

            var groups = measurements
                .GroupBy(m => m.Coloring.Graph.Vertices.Count)
                .Select(g => (key: g.Key, items: g.GroupBy(m => m.AlgorithmName)));

            foreach (var sizeGroups in groups)
            {
                foreach (var group in sizeGroups.items)
                {
                    var durationAvg = group.Average(m => m.Duration.TotalMilliseconds);
                    // vertices
                    table.AddCell(sizeGroups.key.ToString());
                    // algorithm
                    table.AddCell(group.Key);
                    // duration
                    table.AddCell(durationAvg.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Summary:");
            Console.WriteLine("========");
            Console.WriteLine();
            foreach (var line in table.RenderAsStringArray())
            {
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        public static void Print(Measurement measurement)
        {
            var m = measurement;
            Console.WriteLine($"Calculated '{m.AlgorithmName}' for '{m.Filename}'" +
                $" in {m.Duration} ({m.Duration.TotalMilliseconds}ms)," +
                $" total color cost: {m.Coloring.SummaryCost()}" +
                $" (for {m.Coloring.Graph.Vertices.Count} vertices)");
        }
    }
}
