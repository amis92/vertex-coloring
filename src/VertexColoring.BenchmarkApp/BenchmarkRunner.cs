using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using VertexColoring.Graphs;

namespace VertexColoring.BenchmarkApp
{
    class BenchmarkRunner
    {
        public BenchmarkRunner(List<(int vertices, int edges)> sizes, int number, string filenameFormat)
        {
            Sizes = sizes.Any() ? sizes : new[] { (0,0) }.ToList();
            Number = number;
            Benchmark = new ColoringBenchmark
            {
                FilenameFormat = filenameFormat
            };
        }

        public Logger Log { get; set; }

        private List<(int vertices, int edges)> Sizes { get; }

        private int Number { get; }

        private ColoringBenchmark Benchmark { get; }

        private List<Measurement> Measurements { get; } = new List<Measurement>();

        private Stopwatch Watch { get; } = new Stopwatch();

        public void Run()
        {
            Func<GraphColoring> greedySimple = Benchmark.ColorGreedySimple;
            Func<GraphColoring> greedyLF = Benchmark.ColorGreedyLF;
            Func<GraphColoring> greedySF = Benchmark.ColorGreedySF;
            Func<GraphColoring> greedyGIS = Benchmark.ColorGreedyGIS;

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
                    greedySimple();
                    greedyLF();
                    greedySF();
                    greedyGIS();

                    Log.Debug?.WriteLine("Prepared! Coloring... ");

                    // target runs (measured)
                    RunCore(greedySimple, "1. Greedy Simple");
                    RunCore(greedyLF, "2. Greedy LF");
                    RunCore(greedySF, "3. Greedy SF");
                    RunCore(greedyGIS, "4. G.I.S.");
                }
            }
            Log.Info?.WriteSummaryTable(Measurements);
        }

        private void RunCore(Func<GraphColoring> color, string algorithmName)
        {
            Watch.Restart();

            var coloring = color();

            Watch.Stop();
            var measurement = new Measurement
            {
                AlgorithmName = algorithmName,
                Duration = Watch.Elapsed,
                Coloring = coloring,
                Filename = Benchmark.Filename
            };
            Log.Info?.Write(measurement);
            Measurements.Add(measurement);
        }
    }
}
