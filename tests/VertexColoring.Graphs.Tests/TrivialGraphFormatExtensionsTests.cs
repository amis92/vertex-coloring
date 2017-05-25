using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace VertexColoring.Graphs.Tests
{
    public class TrivialGraphFormatExtensionsTests
    {
        [Fact]
        public void WriteSimpleGraphNoEdges()
        {
            var graph = new Graph(new[] { new Vertex(1L, "vertex") }.ToImmutableSortedSet(), ImmutableSortedSet.Create<Edge>());
            var writer = new StringWriter();
            writer.WriteGraphAsTgf(graph);
            var reader = new StringReader(writer.ToString());
            var readGraph = reader.ReadTgfGraph();
            writer.Dispose();
            reader.Dispose();
            Assert.Equal(graph, readGraph);
        }

        [Fact]
        public void ReadSimpleGraphNoEdges()
        {
            const long id = 123L;
            const string label = "Some random label";
            Graph graph;
            using (var writer = new StringWriter())
            {
                writer.WriteLine($"{id} {label}");
                using (var reader = new StringReader(writer.ToString()))
                {
                    graph = reader.ReadTgfGraph();
                }
            }
            Assert.Equal(1, graph.Vertices.Count);
            var vertex = graph.Vertices.ToList().First();
            Assert.Equal(id, vertex.Id);
            Assert.Equal(label, vertex.Label);
        }

        [Fact]
        public void ReadSimpleGraphOneEdge()
        {
            const long id1 = 123L;
            const long id2 = 436L;
            const string label1 = "Some random label";
            const string label2 = "Other random label";
            const string labelEdge = "Edge label";
            Graph graph;
            using (var writer = new StringWriter())
            {
                writer.WriteLine($"{id1} {label1}");
                writer.WriteLine($"{id2} {label2}");
                writer.WriteLine("#");
                writer.WriteLine($"{id1} {id2} {labelEdge}");
                writer.WriteLine();
                using (var reader = new StringReader(writer.ToString()))
                {
                    graph = reader.ReadTgfGraph();
                }
            }
            Assert.Equal(2, graph.Vertices.Count);
            Assert.Equal(1, graph.Edges.Count);
            var vertex1 = graph.Vertices.ToList().First(v => v.Id == id1);
            var vertex2 = graph.Vertices.ToList().First(v => v.Id == id2);
            var edge = graph.Edges.ToList().First();
            Assert.Same(vertex1, edge.Vertex1);
            Assert.Same(vertex2, edge.Vertex2);
        }
    }
}
