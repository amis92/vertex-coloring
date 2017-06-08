using Xunit;

namespace VertexColoring.Graphs.Tests
{
    public class VertexTests : TestBase
    {
        [Theory]
        [InlineData(0L)]
        [InlineData(-1L)]
        [InlineData(1L)]
        [InlineData(long.MinValue)]
        [InlineData(long.MaxValue)]
        [InlineData(-15002900L)]
        [InlineData(15002900L)]
        public void SameVertex_Equals_True(long id)
        {
            var vertex = VertexForId(id);
            Assert.True(vertex.Equals(vertex));
            Assert.True(vertex.Equals((object)vertex));
            Assert.True(Equals(vertex, vertex));
            Assert.True(vertex.CompareTo(vertex) == 0);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(vertex == vertex);
            Assert.False(vertex != vertex);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Theory]
        [InlineData(0L)]
        [InlineData(-1L)]
        [InlineData(1L)]
        [InlineData(long.MinValue)]
        [InlineData(long.MaxValue)]
        [InlineData(-15002900L)]
        [InlineData(15002900L)]
        public void DifferentVertex_Equals_True(long id)
        {
            var vertex1 = VertexForId(id);
            var vertex2 = VertexForId(id);
            Assert.True(vertex1.Equals(vertex2));
            Assert.True(vertex1.Equals((object)vertex2));
            Assert.True(Equals(vertex1, vertex2));
            Assert.True(vertex1.CompareTo(vertex2) == 0);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.True(vertex1 == vertex2);
            Assert.False(vertex1 != vertex2);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Theory]
        [InlineData(0L, 1L)]
        [InlineData(-1L, 1L)]
        [InlineData(1L, -1L)]
        [InlineData(long.MinValue, long.MaxValue)]
        [InlineData(long.MaxValue, long.MinValue)]
        [InlineData(-15002900L, 123L)]
        [InlineData(15002900L, 123L)]
        public void DifferentVertex_Equals_False(long id1, long id2)
        {
            var vertex1 = VertexForId(id1);
            var vertex2 = VertexForId(id2);
            Assert.False(vertex1.Equals(vertex2));
            Assert.False(vertex1.Equals((object)vertex2));
            Assert.False(Equals(vertex1, vertex2));
            Assert.False(vertex1.CompareTo(vertex2) == 0);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.False(vertex1 == vertex2);
            Assert.True(vertex1 != vertex2);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [Theory]
        [InlineData(0L, 1L)]
        [InlineData(-1L, 1L)]
        [InlineData(1L, 100L)]
        [InlineData(long.MinValue, long.MaxValue)]
        [InlineData(long.MinValue, long.MinValue + 1)]
        [InlineData(-15002900L, 123L)]
        [InlineData(123L, 15002900L)]
        public void DifferentVertex_CompareTo_Different(long id1, long id2)
        {
            var vertex1 = VertexForId(id1);
            var vertex2 = VertexForId(id2);
            Assert.True(vertex1.CompareTo(vertex2) < 0);
            Assert.True(vertex2.CompareTo(vertex1) > 0);
        }

        [Theory]
        [InlineData(0L)]
        [InlineData(-1L)]
        [InlineData(1L)]
        [InlineData(long.MinValue)]
        [InlineData(long.MaxValue)]
        public void Vertex_CompareTo_Null_IsBigger(long id)
        {
            var vertex = VertexForId(id);
            Assert.True(vertex.CompareTo(null) > 0);
        }
    }
}
