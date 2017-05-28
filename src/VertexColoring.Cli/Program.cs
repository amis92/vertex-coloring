namespace VertexColoring.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = EntryPoint.Cli.Execute<CliCommands>(args);
        }
    }
}