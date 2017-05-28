using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace VertexColoring.Cli
{
    abstract class BaseColoringRunner
    {
        protected BaseColoringRunner(IEnumerable<(int vertices, int edges)> sizes, int number, string filenameFormat, IEnumerable<Algorithm> algorithms)
        {
            Number = number;
            Runner = new ColoringConfiguration
            {
                FilenameFormat = filenameFormat
            };
            Algorithms = algorithms.Distinct().ToImmutableArray();
            Sizes = sizes.ToImmutableArray() is var sizesImmutable && sizesImmutable.Any()
                ? sizesImmutable
                : new[] { (0, 0) }.ToImmutableArray();
        }

        protected int Number { get; }

        protected ColoringConfiguration Runner { get; }

        protected ImmutableArray<Algorithm> Algorithms { get; }

        protected ImmutableArray<(int vertices, int edges)> Sizes { get; }

        protected List<Measurement> Measurements { get; } = new List<Measurement>();

        protected Stopwatch Watch { get; } = new Stopwatch();

        public virtual void Run()
        {
            for (int i = 0; i < Number; i++)
            {
                foreach (var size in Sizes)
                {
                    RunLoop(i, size.vertices, size.edges);
                }
            }
        }

        protected abstract void RunLoop(int i, int vertices, int edges);

        protected virtual void RunNotMeasured(Algorithm algorithm)
        {
            Runner.Algorithm = algorithm;
            var coloring = Runner.Color();
        }

        protected virtual Measurement RunMeasured(Algorithm algorithm)
        {
            Runner.Algorithm = algorithm;
            Watch.Restart();

            var coloring = Runner.Color();

            Watch.Stop();
            var measurement = new Measurement
            {
                Algorithm = Runner.Algorithm,
                Duration = Watch.Elapsed,
                Coloring = coloring,
                Filename = Runner.Filename
            };
            return measurement;
        }
    }
}
