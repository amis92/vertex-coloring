using System;

namespace VertexColoring.Graphs
{
    /// <summary>
    /// Represents a vertex with an Id that identifies vertex as unique, and an arbitrary label.
    /// The Id defines vertex and two different instances with same Id are considered equal
    /// for all operations (Labels have no meaning).
    /// </summary>
    [Record]
    public sealed partial class Vertex : IEquatable<Vertex>, IComparable<Vertex>
    {
        /// <summary>
        /// Gets the numeric identifier of this vertex.
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Gets an arbitrary label of this vertex.
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// Compares vertices based on their <see cref="Id"/>s.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Result of identifier comparison or a positive value if <paramref name="other"/> is null.</returns>
        public int CompareTo(Vertex other)
        {
            return other == null ? 1 : Id.CompareTo(other.Id);
        }

        public bool Equals(Vertex other)
        {
            return ReferenceEquals(this, other) || Id == other?.Id;
        }
       
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || (obj is Vertex other && Id == other.Id);
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Id} {Label}";
        }

        public static bool operator==(Vertex left, Vertex right)
        {
            return ReferenceEquals(left, right) || (left?.Equals(right) ?? false);
        }

        public static bool operator!=(Vertex left, Vertex right)
        {
            return !(left == right);
        }
    }
}
