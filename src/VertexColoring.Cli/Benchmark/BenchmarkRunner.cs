using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace VertexColoring.Cli
{
    class BenchmarkRunner
    {
        public BenchmarkRunner(List<(int vertices, int edges)> sizes, int number, string filenameFormat, ImmutableArray<Algorithm> algorithms)
        {
            Algorithms = algorithms;
            Sizes = sizes.Any() ? sizes : new[] { (0,0) }.ToList();
            Number = number;
            Benchmark = new ColoringRunner
            {
                FilenameFormat = filenameFormat
            };
        }

        public Logger Log { get; set; }

        private List<(int vertices, int edges)> Sizes { get; }

        private int Number { get; }

        private ColoringRunner Benchmark { get; }

        private List<Measurement> Measurements { get; } = new List<Measurement>();

        private Stopwatch Watch { get; } = new Stopwatch();

        private ImmutableArray<Algorithm> Algorithms { get; }

        public void Run()
        {
            for (int i = 0; i < Number; i++)
            {
                foreach (var size in Sizes)
                {
                    Benchmark.Index = i;
                    Benchmark.VertexCount = size.vertices;
                    Benchmark.EdgeCount = size.edges;

                    Log.Debug?.Write($"Coloring '{Benchmark.Filename}': Loading... ");

                    Benchmark.Setup();

                    Log.Debug?.Write("Loaded! Preparing... ");

                    // prep-run
                    foreach (var algorithm in Algorithms)
                    {
                        RunNotMeasured(algorithm);
                    }

                    Log.Debug?.WriteLine("Prepared! Coloring... ");
                    
                    foreach (var algorithm in Algorithms)
                    {
                        RunMeasured(algorithm);
                    }
                }
            }
            Log.Info?.WriteSummaryTable(Measurements);
        }

        private void RunNotMeasured(Algorithm algorithm)
        {
            Benchmark.Algorithm = algorithm;
            var coloring = Benchmark.Color();
        }

        private void RunMeasured(Algorithm algorithm)
        {
            Benchmark.Algorithm = algorithm;
            Watch.Restart();

            var coloring = Benchmark.Color();

            Watch.Stop();
            var measurement = new Measurement
            {
                Algorithm = Benchmark.Algorithm,
                Duration = Watch.Elapsed,
                Coloring = coloring,
                Filename = Benchmark.Filename
            };
            Log.Info?.Write(measurement);
            Measurements.Add(measurement);
        }
    }
}
