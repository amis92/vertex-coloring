using System;
using VertexColoring.Graphs;
using Xunit;

namespace VertexColoring.Algorithms.Tests
{
    public class ColoringTests
    {
        [Fact]
        public void GreedySimple()
        {
            var vertices = Generator.VerticesInIdRange(0, 6);
            var graph = new MutableGraph();
            graph.Vertices.AddRange(vertices);
            graph.ConnectAll(vertices[0], vertices[3]);
            graph.ConnectAll(vertices[0], vertices[4]);
            graph.ConnectAll(vertices[0], vertices[5]);
            graph.ConnectAll(vertices[1], vertices[3]);
            graph.ConnectAll(vertices[1], vertices[4]);
            graph.ConnectAll(vertices[1], vertices[5]);
            graph.ConnectAll(vertices[2], vertices[3]);
            graph.ConnectAll(vertices[2], vertices[4]);
            graph.ConnectAll(vertices[2], vertices[5]);

            var coloring = graph.ToImmutable().ColorGreedily();

            Assert.Equal(1, coloring[0]);
            Assert.Equal(1, coloring[1]);
            Assert.Equal(1, coloring[2]);
            Assert.Equal(2, coloring[3]);
            Assert.Equal(2, coloring[4]);
            Assert.Equal(2, coloring[5]);
        }

        [Fact]
        public void GreedyTwopart()
        {
            var vertices = Generator.VerticesInIdRange(0, 8);
            var graph = new MutableGraph();
            graph.Vertices.AddRange(vertices);
            graph.ConnectAll(vertices[0], vertices[3]);
            graph.ConnectAll(vertices[0], vertices[5]);
            graph.ConnectAll(vertices[0], vertices[7]);
            graph.ConnectAll(vertices[2], vertices[1]);
            graph.ConnectAll(vertices[2], vertices[5]);
            graph.ConnectAll(vertices[2], vertices[7]);
            graph.ConnectAll(vertices[4], vertices[1]);
            graph.ConnectAll(vertices[4], vertices[3]);
            graph.ConnectAll(vertices[4], vertices[7]);
            graph.ConnectAll(vertices[6], vertices[1]);
            graph.ConnectAll(vertices[6], vertices[3]);
            graph.ConnectAll(vertices[6], vertices[5]);

            var coloring = graph.ToImmutable().ColorGreedily();

            Assert.Equal(1, coloring[0]);
            Assert.Equal(1, coloring[1]);
            Assert.Equal(2, coloring[2]);
            Assert.Equal(2, coloring[3]);
            Assert.Equal(3, coloring[4]);
            Assert.Equal(3, coloring[5]);
            Assert.Equal(4, coloring[6]);
            Assert.Equal(4, coloring[7]);
        }

        [Fact]
        public void GreedyIndependentSets()
        {
            var vertices = Generator.VerticesInIdRange(0, 8);
            var graph = new MutableGraph();
            graph.Vertices.AddRange(vertices);
            graph.ConnectAll(vertices[0], vertices[3]);
            graph.ConnectAll(vertices[0], vertices[5]);
            graph.ConnectAll(vertices[0], vertices[7]);
            graph.ConnectAll(vertices[2], vertices[1]);
            graph.ConnectAll(vertices[2], vertices[5]);
            graph.ConnectAll(vertices[2], vertices[7]);
            graph.ConnectAll(vertices[4], vertices[1]);
            graph.ConnectAll(vertices[4], vertices[3]);
            graph.ConnectAll(vertices[4], vertices[7]);
            graph.ConnectAll(vertices[6], vertices[1]);
            graph.ConnectAll(vertices[6], vertices[3]);
            graph.ConnectAll(vertices[6], vertices[5]);

            var coloring = graph.ToImmutable().ColorGreedyIndependentSets();

            Assert.Equal(2, coloring[0]);
            Assert.Equal(1, coloring[1]);
            Assert.Equal(2, coloring[2]);
            Assert.Equal(1, coloring[3]);
            Assert.Equal(2, coloring[4]);
            Assert.Equal(1, coloring[5]);
            Assert.Equal(2, coloring[6]);
            Assert.Equal(1, coloring[7]);
        }

        [Fact]
        public void ColoringOptimization()
        {
            var vertices = Generator.VerticesInIdRange(0, 3);
            var graph = new MutableGraph();
            graph.Vertices.AddRange(vertices);
            graph.ConnectAll(vertices[0], vertices[1]);
            graph.ConnectAll(vertices[0], vertices[2]);

            var coloring = graph.ToImmutable().ColorGreedily();

            var originalCost = coloring.SummaryCost();

            var optimizedColoring = coloring.OptimizeByWeighting();

            var optimizedCost = optimizedColoring.SummaryCost();

            Assert.Equal(5, originalCost);
            Assert.Equal(4, optimizedCost);
        }

        [Fact]
        public void TimeTest()
        {
            var graph = Generator.RandomConnectedGraph(5000, 20000);
            var greedyColoring = graph.ColorGreedily();
            greedyColoring.OptimizeByWeighting();
            var gisColoring = graph.ColorGreedyIndependentSets();
            gisColoring.OptimizeByWeighting();
        }
    }
}
