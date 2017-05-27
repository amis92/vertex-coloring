using EntryPoint;

namespace VertexColoring.GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = Cli.Execute<CliCommands>(args);
        }
    }
}
