using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Contains extensions useful to writing to/reading from TGF (Trivial Graph Format)
    /// - plain text graph representation. See https://en.wikipedia.org/wiki/Trivial_Graph_Format
    /// </summary>
    public static class TrivialGraphFormatExtensions
    {
        private static Regex IdRegex { get; } = new Regex(@"\d+");

        private static Regex EdgeRegex { get; } = new Regex(@"(\d+) (\d+)");

        /// <summary>
        /// Saves given <paramref name="graph"/> into <paramref name="writer"/> in TGF text representation.
        /// </summary>
        /// <param name="writer">Writer to save graph to.</param>
        /// <param name="graph">Graph to be saved.</param>
        public static void WriteGraphAsTgf(this TextWriter writer, Graph graph)
        {
            foreach (var vertex in graph.Vertices)
            {
                writer.WriteLine(vertex.ToString());
            }
            writer.WriteLine("#");
            foreach (var edge in graph.Edges)
            {
                writer.WriteLine(edge.ToString());
            }
        }
        /// <summary>
        /// Saves given <paramref name="coloring"/> into <paramref name="writer"/> in TGF-like text representation.
        /// The color of each vertex is saved as a second number, after vertex id. So a vertex with id=1, label=sth
        /// colored with number 132 will be saved in TGF vertex list as "1 132 sth".
        /// </summary>
        /// <param name="writer">Writer to save coloring to.</param>
        /// <param name="graph">Coloring to be saved.</param>
        public static void WriteColoringAsTgf(this TextWriter writer, GraphColoring coloring)
        {
            var graph = coloring.Graph;
            foreach (var vertex in graph.Vertices)
            {
                writer.WriteLine($"{vertex.Id} {coloring[vertex]} {vertex.Label}");
            }
            writer.WriteLine("#");
            foreach (var edge in graph.Edges)
            {
                writer.WriteLine(edge.ToString());
            }
        }

        /// <summary>
        /// Reads a graph in TGF from <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">Reads TGF containing graph.</param>
        /// <returns>Graph read.</returns>
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
