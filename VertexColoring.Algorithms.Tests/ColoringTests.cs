using System;
using VertexColoring.Graphs;
using Xunit;

namespace VertexColoring.Algorithms.Tests
{
    public class ColoringTests
    {
        [Fact]
        public void Test1()
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
    }
}
