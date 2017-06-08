using System;
using System.Collections;
using System.Collections.Generic;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Defines an immutable connection between two vertices as unordered vertex pair. Although 
    /// vertices are accessible in ordered manner, all operators, comparisons, equality etc. make
    /// no distinction to order. For all intents and purposes edge (v1,v2) is the same as (v2,v1).
    /// Enumerating this object will yield both vertices in arbitrary order (their order must not
    /// be relied upon).
    /// </summary>
    [Record]
    public sealed partial class Edge : IEquatable<Edge>, IComparable<Edge>, IEnumerable<Vertex>
    {
        /// <summary>
        /// Gets one of edge's vertices.
        /// </summary>
        public Vertex Vertex1 { get; }

        /// <summary>
        /// Gets the other of edge's vertices (other to <see cref="Vertex1"/>).
        /// </summary>
        public Vertex Vertex2 { get; }

        /// <summary>
        /// Gets this edge's arbitrary label.
        /// </summary>
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


        /// <summary>
        /// Compares two edges by comparing their smallest-id vertex and returning the result or, if the result is 0,
        /// the result of comparing their other vertex.
        /// </summary>
        /// <param name="other">The edge to compare this instance to.</param>
        /// <returns>0 if both vertices are the same, otherwise depending on numerical order of vertices, positive or negative.</returns>
        public int CompareTo(Edge other)
        {
            if (other == null) return 1;
            if (ReferenceEquals(this, other)) return 0;
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
            if (left is null) return right is null;
            return left.CompareTo(right) == 0;
        }

        public static bool operator !=(Edge left, Edge right)
        {
            return !(left == right);
        }

        public static bool operator <(Edge left, Edge right)
        {
            if (left is null) return !(right is null);
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(Edge left, Edge right)
        {
            if (left is null) return false;
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(Edge left, Edge right)
        {
            return !(left > right);
        }

        public static bool operator >=(Edge left, Edge right)
        {
            return !(left < right);
        }
    }
}
