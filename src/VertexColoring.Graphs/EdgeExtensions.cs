using System;
using System.Collections.Generic;
using System.Text;

namespace VertexColoring.Graphs
{
    public static class EdgeExtensions
    {
        public static Vertex OtherVertex(this Edge e, Vertex v)
        {
            return e.Vertex1 == v ? e.Vertex2 : e.Vertex1;
        }
    }
}
