using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Text.RegularExpressions;

namespace VertexColoring.Graphs
{
    public class GraphReader
    {
        public static Graph ReadFromTgf(Stream input)
        {
            var idRegex = new Regex(@"\d+");
            var vertices = new List<Vertex>();
            using (var textReader = new StreamReader(input))
            {
                long lineNumber = 0;
                string line;
                while ((line = textReader.ReadLine()) != null)
                {
                    lineNumber++;
                    if (line.StartsWith("#"))
                    {
                        break;
                    }
                    Vertex vertex = ReadVertex(idRegex, line);
                    vertices.Add(vertex);
                }
                // read edges
            }
            return new Graph
            {
                Vertices = vertices.ToImmutableHashSet()
            };
        }

        private static Vertex ReadVertex(Regex idRegex, string line)
        {
            // read long id and label is the rest
            var idMatch = idRegex.Match(line);
            var idString = idMatch?.Value ?? "";
            var id = long.Parse(idString);
            var label = line.Substring(idString.Length).TrimStart(' ');
            var vertex = new Vertex
            {
                Id = id,
                Label = label
            };
            return vertex;
        }
    }
}
