using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System;

namespace VertexColoring.BenchmarkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<ColoringBenchmark>(DefaultConfig.Instance.With(new ColorSumDiagnoser()));
        }
    }
}