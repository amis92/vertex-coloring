using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace VertexColoring.Graphs.Tests
{
    public class GraphReaderTests
    {
        [Fact]
        public void SimpleGraphNoEdges()
        {
            const long id = 123L;
            const string label = "Some random label";
            Graph graph;
            using (var tgfStream = new MemoryStream())
            using (var writer = new StreamWriter(tgfStream))
            {
                writer.WriteLine($"{id} {label}");
                writer.Flush();
                tgfStream.Seek(0, SeekOrigin.Begin);
                graph = GraphReader.ReadFromTgf(tgfStream);
            }
            Assert.Equal(1, graph.Vertices.Count);
            var vertex = graph.Vertices.ToList().First();
            Assert.Equal(id, vertex.Id);
            Assert.Equal(label, vertex.Label);
        }
    }
}
