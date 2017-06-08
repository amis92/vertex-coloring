using Xunit;

namespace VertexColoring.Graphs.Tests
{
    public class EdgeTests : TestBase
    {
        [Theory]
        [InlineData(1, 2)]
        public void SameEdge_SameOrder_Equals_True(long id1, long id2)
        {
            var edge1 = EdgeForId(id1, id2);
            var edge2 = EdgeForId(id1, id2);
            Assert.True(edge1.Equals(edge2));
            Assert.True(edge2.Equals(edge1));
            Assert.True(edge1 == edge2);
            Assert.True(edge2 == edge1);
            Assert.False(edge1 != edge2);
            Assert.False(edge2 != edge1);
            Assert.True(edge1.CompareTo(edge2) == 0);
            Assert.True(edge2.CompareTo(edge1) == 0);
        }

        [Theory]
        [InlineData(1,2)]
        public void SameEdge_DifferentOrder_Equals_True(long id1, long id2)
        {
            var edge1 = EdgeForId(id1, id2);
            var edge2 = EdgeForId(id2, id1);
            Assert.True(edge1.Equals(edge2));
            Assert.True(edge2.Equals(edge1));
            Assert.True(edge1 == edge2);
            Assert.True(edge2 == edge1);
            Assert.False(edge1 != edge2);
            Assert.False(edge2 != edge1);
            Assert.True(edge1.CompareTo(edge2) == 0);
            Assert.True(edge2.CompareTo(edge1) == 0);
        }

        [Theory]
        [InlineData(1, 2, 1, 3)]
        public void DifferentEdge_Equals_False(long id11, long id12, long id21, long id22)
        {
            var edge1 = EdgeForId(id11, id12);
            var edge2 = EdgeForId(id21, id22);
            Assert.False(edge1.Equals(edge2));
            Assert.False(edge2.Equals(edge1));
            Assert.False(edge1 == edge2);
            Assert.False(edge2 == edge1);
            Assert.True(edge1 != edge2);
            Assert.True(edge2 != edge1);
            Assert.False(edge1.CompareTo(edge2) == 0);
            Assert.False(edge2.CompareTo(edge1) == 0);
        }

        [Fact]
        public void TestIsNull()
        {
            Edge e = null;
            Assert.True(e == null);
        }
    }
}
