# vertex-coloring

Vertex coloring algorithm implementation, minimizing summarized cost of vertex colors using different algorithms.

## Description

The initial release contains a `VertexColoring.Graph` and `VertexColoring.Algorithms` libraries and a CLI tool.
The format used for text representation of graphs is TGF ([Trivial Graph Format](https://en.wikipedia.org/wiki/Trivial_Graph_Format)).

Command Line Interface tool provides three different commands to run:
* **random** - generates a set of random connected graphs having a given number of vertices and edges and saves them
* **color** - colors graphs using a selection of algorithms provided and outputs the results to file(s)
* **benchmark** - colors graphs using a selection of algorithms and measures time required for these operations
*  (the output contains only benchmark results, not coloring itself)

You can run `vertexcoloring --help` to get a description of each command.
You can also run `vertexcoloring [command] --help` to get help for each of the commands.

There is a zip containing the CLI tool compiled for win7 x64 attached
to the [v1.0 release](https://github.com/amis92/vertex-coloring/releases/tag/v1.0).
The tool itself is cross platform thanks to .NET Core.

## Examples

### Random

To generate some graphs to start with, run

`vertexcoloring random -v 10,500,3000 -e 100,5000,30000 -n 12`

This will generate three batches of graphs (36 files):
* 12 graphs with 10 vertices and 100 edges;
* 12 graphs with 500 vertices and 5000 edges;
* 12 graphs with 3000 vertices and 300000 edges;

These graphs will have a default filename format, e.g. a third generated graph with 10 vertices will be named
`graph-v10-e100-2.tgf` (its index is 2 because it's zero-based).

### Color

To color graphs generated in previous example, run

`vertexcoloring color -v 10,500,3000 -e 100,5000,30000 -n 12 -o colored-v{1}-e{2}-{0}-{3}.tgf`

This will create colorings for all previously generated graphs using all available alorithms and save each of them to file.
It'll create 4*36 files because there are four coloring algorithms.

If you wouldn't provide `-o` (output) parameter, the resulting colorings won't be saved.

This command will result in graph `graph-v10-e100-2.tgf` having GIS coloring saved to `colored-v10-e100-2-GIS.tgf`.

### Benchmark

To benchmark coloring algorithms, run

`vertexcoloring benchmark -v 10,500,3000 -e 100,5000,30000 -n 12`

This will color all graphs using all algorithms and calculate time used by each algorithm, 
time differences between algorithms and other benchmarking information.
