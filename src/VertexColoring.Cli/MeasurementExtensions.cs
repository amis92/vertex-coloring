using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextTableFormatter;
using VertexColoring.Graphs;

namespace VertexColoring.Cli
{
    static class MeasurementExtensions
    {
        public static void WriteSummaryTable(this TextWriter writer, IEnumerable<Measurement> measurements)
        {
            // | Vertices | Algorithm | Duration [ms] |
            var table = new TextTable(4, TableBordersStyle.CLASSIC, TableVisibleBorders.SURROUND_HEADER_AND_COLUMNS);
            
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

            var groups = measurements
                .GroupBy(m => m.Coloring.Graph.Vertices.Count)
                .Select(g => (key: g.Key, items: g.GroupBy(m => m.Algorithm)));

            foreach (var sizeGroups in groups)
            {
                foreach (var group in sizeGroups.items)
                {
                    var durationAvg = group.Average(m => m.Duration.TotalMilliseconds);
                    var avgCost = (decimal) group.Average(m => m.Coloring.SummaryCost());
                    // vertices
                    table.AddCell($" {sizeGroups.key} ", rightAlignmentStyle);
                    // algorithm
                    table.AddCell($" {group.Key} ", leftAlignmentStyle);
                    // duration
                    table.AddCell($" {durationAvg} ", rightAlignmentStyle);
                    // total color cost
                    table.AddCell($" {avgCost} ", rightAlignmentStyle);
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
