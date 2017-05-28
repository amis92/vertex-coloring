using System;
using System.Collections.Generic;
using System.Text;

namespace VertexColoring.Cli
{
    /// <summary>
    /// Coloring algorithms
    /// </summary>
    enum Algorithm
    {
        /// <summary>
        /// Simple greedy coloring algorithm
        /// </summary>
        Simple,
        /// <summary>
        /// Greedy Largest First
        /// </summary>
        LF,
        /// <summary>
        /// Greedy Smallest First
        /// </summary>
        SF,
        /// <summary>
        /// G.I.S. - Greedy Independent Sets
        /// </summary>
        GIS
    }
}
