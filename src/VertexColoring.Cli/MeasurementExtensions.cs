using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using TextTableFormatter;

namespace VertexColoring.Cli
{
    static class MeasurementExtensions
    {
        public static void WriteSummaryTable(this TextWriter writer, IEnumerable<Measurement> measurements, Algorithm? baseline = default(Algorithm?))
        {
            // | Vertices | Algorithm | Avg. Duration [ms] | Avg. Total cost | Avg. Diff to <baseline> |
            var isBaseline = baseline != null;
            var columnCount = isBaseline ? 5 : 4;
            var table = new TextTable(columnCount);
            
            var leftAlignmentStyle = new CellStyle(
                CellHorizontalAlignment.Left,
                CellTextTrimmingStyle.Dots,
                CellNullStyle.EmptyString,
                removeTerminalFormats: false /*otherwise '[' creates weird char sequence which is appended to cell*/);

            var rightAlignmentStyle = new CellStyle(
                CellHorizontalAlignment.Right,
                CellTextTrimmingStyle.Dots,
                CellNullStyle.EmptyString,
                removeTerminalFormats: false /*otherwise '[' creates weird char sequence which is appended to cell*/);

            table.AddCell(" Vertices ", leftAlignmentStyle);
            table.AddCell(" Algorithm ", leftAlignmentStyle);
            table.AddCell(" Avg. Duration [ms] ", leftAlignmentStyle);
            table.AddCell(" Avg. Total cost ", leftAlignmentStyle);
            if (baseline != null)
            {
                table.AddCell($" Avg. Diff to {baseline} ");
            }

            var groups = measurements
                .GroupBy(m => m.Coloring.Graph.Vertices.Count)
                .ToImmutableArray();
                

            foreach (var sizeGroup in groups)
            {
                var avgDiff = sizeGroup
                    .GroupBy(m => m.Filename)
                    .SelectMany(g => {
                        var baseCost = g.FirstOrDefault(m => m.Algorithm == baseline)?.Coloring.SummaryCost ?? 1;
                        return g.Select(m => (measurement: m, diff: ((double)m.Coloring.SummaryCost / baseCost) - 1));
                    })
                    .GroupBy(t => t.measurement.Algorithm)
                    .ToDictionary(g => g.Key, g => g.Average(t => t.diff));


                foreach (var algorithmGroup in sizeGroup.GroupBy(m => m.Algorithm))
                {
                    var durationAvg = algorithmGroup.Average(m => m.Duration.TotalMilliseconds);
                    var avgCost = (decimal) algorithmGroup.Average(m => m.Coloring.SummaryCost);
                    // vertices
                    table.AddCell($" {sizeGroup.Key} ", rightAlignmentStyle);
                    // algorithm
                    table.AddCell($" {algorithmGroup.Key} ", leftAlignmentStyle);
                    // duration
                    table.AddCell($" {durationAvg:N5} ", rightAlignmentStyle);
                    // total color cost
                    table.AddCell($" {avgCost:N2} ", rightAlignmentStyle);
                    if (baseline != null)
                    {
                        // diff to baseline
                        table.AddCell($" {(decimal)avgDiff[algorithmGroup.Key]:P2} ", rightAlignmentStyle);
                    }
                }
                if (sizeGroup != groups.Last())
                {
                    // add empty row
                    for (int i = 0; i < columnCount; i++)
                    {
                        table.AddCell(null);
                    }
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
    }
}
