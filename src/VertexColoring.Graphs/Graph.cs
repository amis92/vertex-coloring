using System;
using System.Collections.Immutable;
using System.IO;

namespace VertexColoring.Graphs
{
    [Record]
    public sealed partial class Graph : IEquatable<Graph>
    {
        public ImmutableSortedSet<Vertex> Vertices { get; }

        public ImmutableSortedSet<Edge> Edges { get; }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is Graph other && Vertices.SetEquals(other.Vertices) && Edges.SetEquals(other.Edges));
        }

        public bool Equals(Graph other)
        {
            return other != null && (ReferenceEquals(this, other) || Vertices.SetEquals(other.Vertices) && Edges.SetEquals(other.Edges));
        }

        public override int GetHashCode()
        {
            var code = 0;
            unchecked
            {
                code = Vertices.GetHashCode() + Edges.GetHashCode() * 31;
            }
            return code;
        }

        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                writer.WriteGraphAsTgf(this);
                return writer.ToString();
            }
        }

        public static bool operator ==(Graph left, Graph right)
        {
            return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
        }

        public static bool operator!=(Graph left, Graph right)
        {
            return !(left == right);
        }
    }
}
