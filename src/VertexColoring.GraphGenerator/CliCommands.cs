using EntryPoint;

namespace VertexColoring.GraphGenerator
{
    public class CliCommands : BaseCliCommands
    {
        [DefaultCommand]
        [Command("random")]
        [Help("Generates random connected graph.")]
        public void Random(string[] args)
        {
            RandomGraphCommand.Execute(args);
        }
    }
}
