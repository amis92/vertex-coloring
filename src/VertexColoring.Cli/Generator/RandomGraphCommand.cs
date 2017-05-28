using System;
using VertexColoring.Graphs;
using System.Diagnostics;
using System.IO;
using System.Collections.Immutable;
using System.Linq;

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
            Random = Options.RandomSeed.HasValue ? new Random(Options.RandomSeed.Value) : new Random();
            var sizesImmutable = Options.VertexCounts.Zip(Options.EdgeCounts, (v, e) => (v, e)).ToImmutableArray();
            Sizes =  sizesImmutable.Any() ? sizesImmutable : new[] { (0, 0) }.ToImmutableArray();
        }

        private CliArguments Options { get; }

        private Logger Log { get; }

        private string Filename { get; }

        private Stopwatch Watch { get; } = new Stopwatch();

        private Random Random { get; }

        private ImmutableArray<(int vertices, int edges)> Sizes { get; }

        public static void Execute(string[] args)
        {
            var options = EntryPoint.Cli.Parse<CliArguments>(args);
            new RandomGraphCommand(options).Run();
        }

        private void Run()
        {
            for (int i = 0; i < Options.Number; i++)
            {
                foreach (var size in Sizes)
                {
                    GenerateGraph(i, size.vertices, size.edges);
                }
            }
        }

        private void GenerateGraph(int i, int vertices, int edges)
        {
            Log.Debug?.WriteLine($"{i}. Generating random graph with {vertices} vertices" +
                $" and {edges} edges.");

            Watch.Restart();
            var graph = Generator.RandomConnectedGraph(vertices, edges, Random);
            Watch.Stop();
            var filename = string.Format(Options.OutputFilename, i, vertices, edges);

            Log.Debug?.WriteLine($"{i}. Generated {graph.Edges.Count} edges" +
                $" ({edges - graph.Edges.Count} were duplicated).");
            Log.Debug?.WriteLine($"{i}. Generated in {Watch.ElapsedMilliseconds}ms.");
            Log.Info?.WriteLine($"{i}. Saving '{filename}'.");

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
