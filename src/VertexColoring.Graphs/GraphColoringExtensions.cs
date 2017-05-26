﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace VertexColoring.Graphs
{
    public static class GraphColoringExtensions
    {
        public static GraphColoring ToImmutable(this MutableGraphColoring coloring)
        {
            return new GraphColoring(coloring.Graph, coloring.VertexColors.ToImmutableDictionary());
        }

        public static int SummaryCost(this GraphColoring coloring)
        {
            return coloring.VertexColors.Sum(c => c.Value);
        }
    }
}
