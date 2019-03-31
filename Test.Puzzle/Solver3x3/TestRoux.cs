using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestRoux
    {
        [Fact]
        public void Can_load_Roux_from_assembly_embedded_resource()
        {
            var solver = SolverParser.ParseFromEmbeddedResource("Roux");
        }
    }
}