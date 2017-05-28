using System;
using VertexColoring.Graphs;
using System.Diagnostics;
using System.IO;

namespace VertexColoring.Cli
{
    sealed partial class RandomGraphCommand
    {
        private const string TgfFileExtension = ".tgf";

        public RandomGraphCommand(CliArguments options)
        {
            Options = options;
            Log = new Logger(Options.Debug ? Console.Out : null, Console.Out);
            Filename = FixFilename(options.OutputFilename);
        }

        private CliArguments Options { get; }

        private Logger Log { get; }

        private string Filename { get; }

        public static void Execute(string[] args)
        {
            var options = EntryPoint.Cli.Parse<CliArguments>(args);
            new RandomGraphCommand(options).Run();
        }

        private void Run()
        {
            for (int i = 0; i < Options.Number; i++)
            {
                GenerateGraph(i);
            }
        }

        private void GenerateGraph(int i)
        {
            Log.Debug?.WriteLine($"{i}. Generating random graph with {Options.VertexCount} vertices" +
                $" and {Options.EdgeCount} edges.");

            var watch = Stopwatch.StartNew();
            var graph = Generator.RandomConnectedGraph(Options.VertexCount, Options.EdgeCount);
            watch.Stop();
            var filename = string.Format(Options.OutputFilename, i, Options.VertexCount, Options.EdgeCount);

            Log.Debug?.WriteLine($"{i}. Generated {graph.Edges.Count} edges" +
                $" ({Options.EdgeCount - graph.Edges.Count} were duplicated).");
            Log.Debug?.WriteLine($"{i}. Generated in {watch.ElapsedMilliseconds}ms.");
            Log.Debug?.WriteLine($"{i}. Saving to '{filename}'.");

            SaveGraph(graph, filename);

            Log.Debug?.WriteLine($"{i}. Saved.");
        }

        private static void SaveGraph(Graph graph, string filename)
        {
            using (var writer = File.CreateText(filename))
            {
                writer.WriteGraphAsTgf(graph);
            }
        }

        private static string FixFilename(string filename)
        {
            if (!filename.Contains("{0}"))
            {
                filename = filename.LastIndexOf(TgfFileExtension) is int extensionIndex && extensionIndex >= 0
                    ? filename.Insert(extensionIndex, "{0}")
                    : filename + "{0}";
            }
            if (!filename.EndsWith(TgfFileExtension))
            {
                filename = filename + TgfFileExtension;
            }
            return filename;
        }
    }
}
