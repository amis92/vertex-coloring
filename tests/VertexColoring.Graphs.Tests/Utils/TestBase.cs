namespace VertexColoring.Graphs.Tests
{
    public class TestBase
    {
        protected static Vertex VertexForId(long id) => new Vertex(id, $"vertex{id}");

        protected static Edge EdgeForId(long id1, long id2) => new Edge(VertexForId(id1), VertexForId(id2), $"edge-{id1}-{id2}");
    }
}
