using System;
using EntryPoint;
using VertexColoring.Graphs;
using System.Diagnostics;
using System.IO;

namespace VertexColoring.GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var arguments = Cli.Execute<CliCommands>(args);
        }
    }

    public class CliCommands : BaseCliCommands
    {
        [DefaultCommand]
        [Command("random")]
        [Help("Generates random connected graph.")]
        public void Random(string[] args)
        {
            var options = Cli.Parse<CliArguments>(args);

            options.OutputFilename = options.OutputFilename.Contains("{0}") && options.OutputFilename.EndsWith(".tgf") ? options.OutputFilename : options.OutputFilename + "{0}.tgf";

            for (int i = 0; i < options.Number; i++)
            {
                if (options.Debug)
                {
                    Console.WriteLine($"{i}. Generating random graph with {options.VertexCount} vertices and {options.EdgeCount} edges.");
                }
                var watch = Stopwatch.StartNew();
                var graph = Generator.RandomConnectedGraph(options.VertexCount, options.EdgeCount);
                watch.Stop();
                var filename = string.Format(options.OutputFilename, i, options.VertexCount, options.EdgeCount);
                if (options.Debug)
                {
                    Console.WriteLine($"{i}. Generated {graph.Edges.Count} edges ({options.EdgeCount - graph.Edges.Count} were duplicated)");
                    Console.WriteLine($"{i}. Generated in {watch.ElapsedMilliseconds}ms");
                    Console.WriteLine($"{i}. Saving to {filename}");
                }
                using (var writer = File.CreateText(filename))
                {
                    writer.WriteGraphAsTgf(graph);
                }
                if (options.Debug)
                {
                    Console.WriteLine($"{i}. Saved.");
                }
            }
        }
    }

    public class CliArguments : BaseCliArguments
    {
        public CliArguments() : base("random1")
        {
        }

        [Required]
        [OptionParameter(ShortName:'v', LongName:"vertexCount")]
        [Help("Vertex count, must be non-negative.")]
        public int VertexCount { get; set; }

        [OptionParameter(ShortName: 'e', LongName: "edgeCount")]
        [Help("Edge count, must be > v - 1 and non-negative.")]
        public int EdgeCount { get; set; }

        [OptionParameter(ShortName: 'o', LongName: "output")]
        [Help("Output file path with {0} for graph index when generating more than one. Additional parameters: {1} - vertexCount, {2} - edgeCount.")]
        public string OutputFilename { get; set; } = "graph{0}.tgf";

        [Option(ShortName: 'd', LongName: "debug")]
        public bool Debug { get; set; } = false;

        [OptionParameter(ShortName: 'n', LongName: "number")]
        public int Number { get; set; } = 1;
    }
}
