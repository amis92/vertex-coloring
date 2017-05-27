using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextTableFormatter;
using VertexColoring.Graphs;

namespace VertexColoring.BenchmarkApp
{
    static class MeasurementPrinter
    {
        public static void WriteSummaryTable(this TextWriter writer, IEnumerable<Measurement> measurements)
        {
            // | Vertices | Algorithm | Duration [ms] |
            var table = new TextTable(3, TableBordersStyle.CLASSIC, TableVisibleBorders.SURROUND_HEADER_AND_COLUMNS);

            var rightAlignmentStyle = new CellStyle(CellHorizontalAlignment.Right);

            table.AddCell(" Vertices ");
            table.AddCell(" Algorithm ");
            table.AddCell(" Duration (ms) ");

            var groups = measurements
                .GroupBy(m => m.Coloring.Graph.Vertices.Count)
                .Select(g => (key: g.Key, items: g.GroupBy(m => m.AlgorithmName)));

            foreach (var sizeGroups in groups)
            {
                foreach (var group in sizeGroups.items)
                {
                    var durationAvg = group.Average(m => m.Duration.TotalMilliseconds);
                    // vertices
                    table.AddCell($" {sizeGroups.key} ", rightAlignmentStyle);
                    // algorithm
                    table.AddCell($" {group.Key} ");
                    // duration
                    table.AddCell($" {durationAvg} ", rightAlignmentStyle);
                }
            }

            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Summary:");
            writer.WriteLine("========");
            writer.WriteLine();
            foreach (var line in table.RenderAsStringArray())
            {
                writer.WriteLine(line);
            }
            writer.WriteLine();
        }

        public static void Write(this TextWriter writer, Measurement measurement)
        {
            var m = measurement;
            writer.WriteLine($"Calculated '{m.AlgorithmName}' for '{m.Filename}'" +
                $" in {m.Duration} ({m.Duration.TotalMilliseconds}ms)," +
                $" total color cost: {m.Coloring.SummaryCost()}" +
                $" (for {m.Coloring.Graph.Vertices.Count} vertices)");
        }
    }
}
