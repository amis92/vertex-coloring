using System;
using System.Collections;
using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    [Record]
    public sealed partial class Edge : IEquatable<Edge>, IComparable<Edge>, IEnumerable<Vertex>
    {
        public Vertex Vertex1 { get; }

        public Vertex Vertex2 { get; }

        public string Label { get; }

        private (long, long) VerticesAsOrderedTuple()
        {
            return Vertex1.Id < Vertex2.Id ? (Vertex1.Id, Vertex2.Id) : (Vertex2.Id, Vertex1.Id);
        }

        private bool HasSameVertices(Edge otherNotNull)
        {
            return Vertex1 == otherNotNull.Vertex1 && Vertex2 == otherNotNull.Vertex2
                || Vertex1 == otherNotNull.Vertex2 && Vertex2 == otherNotNull.Vertex1;
        }

        public bool Equals(Edge other)
        {
            return other != null && (ReferenceEquals(this, other) || HasSameVertices(other));
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is Edge other && HasSameVertices(other));
        }

        public override int GetHashCode()
        {
            int code = 1;
            unchecked
            {
                code = Vertex1.GetHashCode() + Vertex2.GetHashCode();
            }
            return code;
        }

        public override string ToString()
        {
            return $"{Vertex1.Id} {Vertex2.Id} {Label}";
        }

        public int CompareTo(Edge other)
        {
            var (x1, x2) = VerticesAsOrderedTuple();
            var (y1, y2) = other.VerticesAsOrderedTuple();
            var diffOn1 = x1.CompareTo(y1);
            return diffOn1 != 0 ? diffOn1 : x2.CompareTo(y2);
        }

        public IEnumerator<Vertex> GetEnumerator()
        {
            yield return Vertex1;
            yield return Vertex2;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static bool operator==(Edge left, Edge right)
        {
            return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
        }

        public static bool operator !=(Edge left, Edge right)
        {
            return !(left == right);
        }
    }
}
