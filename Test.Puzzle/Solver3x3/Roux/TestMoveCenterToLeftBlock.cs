using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveCenterToLeftBlock
    {
        [Fact]
        public void No_algorithms_if_already_at_goal()
        {
            var step = new MoveCenterToLeftBlock(Puzzle.Solved);
            var algorithms = step.GetPossibleAlgorithms();
            Assert.Empty(algorithms);
        }

        [Theory]
        [InlineData("E", "y")]
        [InlineData("E'", "y'")]
        [InlineData("E2", "y2")]
        [InlineData("S", "z'")]
        [InlineData("S'", "z")]
        public void Can_solve(string scrambleMoves, string solveMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Puzzle.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var step = new MoveCenterToLeftBlock(scrambledPuzzle);
            var algorithms = step.GetPossibleAlgorithms();

            var algorithm = Assert.Single(algorithms);
            Assert.Equal(solveMoves, NotationParser.FormatMoves(algorithm.Moves));
        }
    }
}