using System.Collections.Generic;

namespace VertexColoring.Cli
{
    class BenchmarkRunner : BaseColoringRunner
    {
        public BenchmarkRunner(
            IEnumerable<(int vertices, int edges)> sizes,
            int number,
            string filenameFormat,
            IEnumerable<Algorithm> algorithms)
            : base(sizes, number, filenameFormat, algorithms)
        {
        }

        public Logger Log { get; set; }

        public Algorithm Baseline { get; set; }

        public override void Run()
        {
            base.Run();
            Log.Info?.WriteSummaryTable(Measurements, Baseline);
        }

        protected override void RunLoop(int i, int vertices, int edges)
        {
            Runner.Index = i;
            Runner.VertexCount = vertices;
            Runner.EdgeCount = edges;

            Log.Debug?.Write($"Coloring '{Runner.Filename}': Loading... ");

            Runner.Setup();

            Log.Debug?.Write("Loaded! Preparing... ");

            // prep-run
            foreach (var algorithm in Algorithms)
            {
                RunNotMeasured(algorithm);
            }

            Log.Debug?.WriteLine("Prepared! Coloring... ");

            foreach (var algorithm in Algorithms)
            {
                var measurement = RunMeasured(algorithm);
                Log.Info?.WriteLine(measurement);
                Measurements.Add(measurement);
            }
        }
    }
}
