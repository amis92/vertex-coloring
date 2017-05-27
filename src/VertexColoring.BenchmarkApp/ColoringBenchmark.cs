using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Parameters;
using BenchmarkDotNet.Attributes;
using System.Linq;
using System.IO;
using VertexColoring.Graphs;
using VertexColoring.Algorithms;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Engines;

namespace VertexColoring.BenchmarkApp
{
    [SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 1, targetCount: 5, id: "ColoringJob")]
    
    public class ColoringBenchmark
    {
        [Params(10/*,30,100,300,1000,3000*/)]
        public int VertexCount { get; set; }

        private int EdgeCount => VertexCount * 4;

        private int Index { get; set; }

        private Graph Graph { get; set; }

        private GraphAdjacency Adjacency { get; set; }

        [Setup]
        public void Setup()
        {
            using (var reader = new StreamReader(File.OpenRead($@"graph-v{VertexCount}-e{EdgeCount}-{Index}.tgf")))
            {
                Graph = reader.ReadTgfGraph();
            }
            Adjacency = Graph.Adjacency();
        }

        [Benchmark(Description = "Greedy Simple")]
        public int ColorGreedySimple()
        {
            var coloring = Graph.ColorGreedily();
            var optimized = coloring.OptimizeByWeighting();
            return optimized.SummaryCost();
        }

        [Benchmark(Description = "Greedy LF")]
        public int ColorGreedyLF()
        {
            var coloring = Graph.ColorGreedily();
            var optimized = coloring.OptimizeByWeighting();
            return optimized.SummaryCost();
        }

        [Benchmark(Description = "Greedy SF")]
        public int ColorGreedySF()
        {
            var coloring = Graph.ColorGreedily();
            var optimized = coloring.OptimizeByWeighting();
            return optimized.SummaryCost();
        }

        [Benchmark(Description = "G.I.S.")]
        public int ColorGreedyGIS()
        {
            
            var coloring = Graph.ColorGreedily();
            var optimized = coloring.OptimizeByWeighting();
            return optimized.SummaryCost();
        }
    }

    public class ColorSumDiagnoser : IDiagnoser
    {
        public void AfterSetup(DiagnoserActionParameters parameters)
        {
        }

        public void BeforeAnythingElse(DiagnoserActionParameters parameters)
        {
        }

        public void BeforeCleanup()
        {
        }

        public void BeforeMainRun(DiagnoserActionParameters parameters)
        {
        }

        public void DisplayResults(ILogger logger)
        {
        }

        public IColumnProvider GetColumnProvider()
        {
            return EmptyColumnProvider.Instance;
        }

        public void ProcessResults(Benchmark benchmark, BenchmarkReport report)
        {

        }

        public IEnumerable<ValidationError> Validate(ValidationParameters validationParameters)
        {
            return Enumerable.Empty<ValidationError>();
        }
    }
}
