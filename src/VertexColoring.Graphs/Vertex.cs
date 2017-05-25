using System;

namespace VertexColoring.Graphs
{
    [Record]
    public sealed partial class Vertex : IEquatable<Vertex>, IComparable<Vertex>
    {
        public long Id { get; }

        public string Label { get; }

        public int CompareTo(Vertex other)
        {
            return Id.CompareTo(other?.Id ?? 0);
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
