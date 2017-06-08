using System;
using System.Collections.Generic;
using System.Linq;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Provides methods for creating graphs, including random graphs.
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Adds to <paramref name="graph"/> new edges connecting each of <paramref name="vertices"/> with every other.
        /// </summary>
        /// <param name="graph">Graph to add edges to.</param>
        /// <param name="vertices">Vertices to connect with new edges.</param>
        public static void ConnectAll(this MutableGraph graph, params MutableVertex[] vertices)
        {
            graph.Edges.AddRange(vertices.SelectMany(v1 => vertices.Select(v2 => v1.Id != v2.Id ? v1.Connecting(v2) : null).Where(e => e != null)));
        }

        /// <summary>
        /// Adds to <paramref name="graph"/> new edges connecting each of <paramref name="vertices"/> with every other.
        /// </summary>
        /// <param name="graph">Graph to add edges to.</param>
        /// <param name="vertices">Vertices to connect with new edges.</param>
        public static void ConnectAll(this MutableGraph graph, IEnumerable<MutableVertex> vertices)
        {
            graph.ConnectAll(vertices.ToArray());
        }

        /// <summary>
        /// Creates new edge connecting given vertices.
        /// </summary>
        /// <param name="v1">Vertex to be connected be new edge.</param>
        /// <param name="v1">Other vertex to be connected be new edge.</param>
        /// <returns>Created edge.</returns>
        public static MutableEdge Connecting(this MutableVertex v1, MutableVertex v2)
        {
            return new MutableEdge { Vertex1 = v1, Vertex2 = v2, Label = $"e{v1.Id}:{v2.Id}" };
        }

        /// <summary>
        /// Creates a list of <paramref name="count"/> vertices with ids starting from <paramref name="start"/>.
        /// </summary>
        /// <param name="start">Starting id.</param>
        /// <param name="count">Number of vertices to create.</param>
        /// <returns>Created list.</returns>
        public static List<MutableVertex> VerticesInIdRange(int start, int count)
        {
            return Enumerable.Range(start, count).Select(id => new MutableVertex { Id = id, Label = $"v{id}" }).ToList();
        }

        /// <summary>
        /// Creates new random connected graph with <paramref name="vertexCount"/> vertices
        /// and up to <paramref name="edgeCount"/> connecting them. Some edges may be duplicates
        /// on creation and as a result, be lost (duplicate edges are ignored).
        /// </summary>
        /// <param name="vertexCount">Number of vertices to create.</param>
        /// <param name="edgeCount">Number of edges to create.</param>
        /// <returns>Created graph.</returns>
        public static Graph RandomConnectedGraph(int vertexCount, int edgeCount)
        {
            CheckRandomArguments(vertexCount, edgeCount);
            var random = new Random();
            return RandomConnectedGraphInternal(vertexCount, edgeCount, random);
        }


        /// <summary>
        /// Creates new random connected graph with <paramref name="vertexCount"/> vertices
        /// and up to <paramref name="edgeCount"/> connecting them. Some edges may be duplicates
        /// on creation and as a result, be lost (duplicate edges are ignored).
        /// </summary>
        /// <param name="vertexCount">Number of vertices to create.</param>
        /// <param name="edgeCount">Number of edges to create.</param>
        /// <param name="random">Random to use while randomizing edge associations.</param>
        /// <returns>Created graph.</returns>
        public static Graph RandomConnectedGraph(int vertexCount, int edgeCount, Random random)
        {
            CheckRandomArguments(vertexCount, edgeCount);
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }
            return RandomConnectedGraphInternal(vertexCount, edgeCount, random);
        }
        
        private static void CheckRandomArguments(int vertexCount, int edgeCount)
        {
            if (vertexCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount), "must be non-negative");
            }
            if (edgeCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(edgeCount), "must be non-negative");
            }
            if (edgeCount < vertexCount - 1 && vertexCount > 0)
            {
                throw new ArgumentException($"Cannot be smaller than {nameof(vertexCount)} - 1", nameof(edgeCount));
            }
        }

        private static Graph RandomConnectedGraphInternal(int vertexCount, int edgeCount, Random random)
        {
            var ids = Enumerable.Range(0, vertexCount);
            var graph = new MutableGraph();
            graph.Vertices.AddRange(VerticesInIdRange(0, vertexCount));
            var vertices = graph.Vertices.ToDictionary(v => v.Id);
            // making graph connected
            foreach (var id in ids)
            {
                var otherId = random.Next(id);
                if (id > 0)
                {
                    var edge = new MutableEdge { Vertex1 = vertices[id], Vertex2 = vertices[otherId], Label = $"e{id}:{otherId}" };
                    graph.Edges.Add(edge);
                }
            }
            // adding remaining vertices
            foreach (var edgeId in Enumerable.Range(vertexCount, edgeCount - vertexCount + 1))
            {
                var id1 = random.Next(vertexCount);
                var id2 = random.Next(vertexCount);
                var edge = new MutableEdge { Vertex1 = vertices[id1], Vertex2 = vertices[id2], Label = $"e{id1}:{id2}" };
                graph.Edges.Add(edge);
            }
            return graph.ToImmutable();
        }
    }
}
