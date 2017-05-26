using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace VertexColoring.Graphs
{
    public static class GraphColoringExtensions
    {
        public static GraphColoring ToImmutable(this MutableGraphColoring coloring)
        {
            return new GraphColoring(coloring.Graph, coloring.VertexColors.ToImmutableDictionary());
        }
    }
}
