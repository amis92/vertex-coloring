using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VertexColoring.Graphs
{
    public static class TrivialGraphFormatExtensions
    {
        public static void WriteGraphAsTgf(this TextWriter writer, Graph graph)
        {
            var vertices = graph.Vertices.ToList();
            vertices.Sort();
            var edges = graph.Edges.ToList();
            edges.Sort();
            foreach (var vertex in vertices)
            {
                writer.WriteLine(vertex.ToString());
            }
            writer.WriteLine("#");
            foreach (var edge in edges)
            {
                writer.WriteLine(edge.ToString());
            }
        }

        private static Regex IdRegex { get; } = new Regex(@"\d+");

        private static Regex EdgeRegex { get; } = new Regex(@"(\d+) (\d+)");

        public static Graph ReadTgfGraph(this TextReader reader)
        {
            var vertices = new List<Vertex>();
            long lineNumber = 0;
            string line;
            while ((line = reader.ReadLine()) != null && !line.StartsWith("#"))
            {
                lineNumber++;
                var vertex = ReadTgfVertex(line);
                if (vertex != null)
                {
                    vertices.Add(vertex);
                }
            }
            lineNumber++;
            var vertexDictionary = vertices.ToImmutableDictionary(v => v.Id);
            var edges = new List<Edge>();
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                var edge = ReadTgfEdge(line, vertexDictionary);
                if (edge != null)
                {
                    edges.Add(edge);
                }
            }
            return new Graph(vertices.ToImmutableSortedSet(), edges.ToImmutableSortedSet());
        }

        private static Vertex ReadTgfVertex(string line)
        {
            // read long id and label is the rest
            var idMatch = IdRegex.Match(line);
            if (!idMatch.Success)
            {
                return null;
            }
            var idString = idMatch?.Value;
            var id = long.Parse(idString);
            var label = line.Substring(idString.Length).TrimStart(' ');
            var vertex = new Vertex(id, label);
            return vertex;
        }

        private static Edge ReadTgfEdge(string line, IImmutableDictionary<long, Vertex> vertices)
        {
            var match = EdgeRegex.Match(line);
            if (!match.Success)
            {
                return null;
            }
            var id1 = long.Parse(match.Groups[1].Value);
            var id2 = long.Parse(match.Groups[2].Value);
            var label = line.Substring(match.Value.Length).TrimStart(' ');
            var edge = new Edge(vertices[id1], vertices[id2], label);
            return edge;
        }
    }
}
