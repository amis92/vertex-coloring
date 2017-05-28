using System.Collections.Generic;
using System.IO;
using VertexColoring.Graphs;

namespace VertexColoring.Cli
{
    class ColoringRunner : BaseColoringRunner
    {
        public ColoringRunner(
            IEnumerable<(int vertices, int edges)> sizes,
            int number,
            string filenameFormat,
            string outputFilenameFormat,
            IEnumerable<Algorithm> algorithms)
            : base(sizes, number, filenameFormat, algorithms)
        {
            OutputFilename = outputFilenameFormat;
        }

        public Logger Log { get; set; }

        public string OutputFilename { get; }

        public override void Run()
        {
            base.Run();
            Log.Info?.WriteSummaryTable(Measurements);
        }

        protected override void RunLoop(int i, int vertices, int edges)
        {
            Runner.Index = i;
            Runner.VertexCount = vertices;
            Runner.EdgeCount = edges;

            Log.Debug?.Write($"Coloring '{Runner.Filename}': Loading... ");

            Runner.Setup();

            Log.Debug?.WriteLine("Loaded! Coloring... ");

            foreach (var algorithm in Algorithms)
            {
                var measurement = RunMeasured(algorithm);
                Log.Info?.WriteLine(measurement);
                Measurements.Add(measurement);
                SaveResult(measurement);
            }
        }

        private void SaveResult(Measurement measurement)
        {
            if (string.IsNullOrWhiteSpace(OutputFilename))
            {
                return;
            }

            var output = string.Format(OutputFilename, Runner.Index, Runner.VertexCount, Runner.EdgeCount, Runner.Algorithm);

            Log.Debug?.Write($"Saving result to '{output}'... ");

            using (var writer = new StreamWriter(File.OpenWrite(output)))
            {
                writer.WriteColoringAsTgf(measurement.Coloring);
            }
            Log.Debug?.WriteLine("Saved!");
        }
    }
}
