using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveBlueCenterToLeftFaceStep
    {
        [Fact]
        public void No_algorithms_if_already_at_goal()
        {
            var algorithms = MoveBlueCenterToLeftFaceStep.Instance.GetPossibleAlgorithms(Solver.Solved);

            Assert.Empty(algorithms);
        }

        [Theory]
        [InlineData("E")]
        [InlineData("E'")]
        [InlineData("E2")]
        [InlineData("S")]
        [InlineData("S'")]
        public void Can_solve(string scrambleMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Solver.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var algorithmInfos = MoveBlueCenterToLeftFaceStep.Instance.GetPossibleAlgorithms(scrambledPuzzle);

            var algorithmInfo = Assert.Single(algorithmInfos);
            var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithmInfo.Algorithm.Moves);

            Assert.True(MoveBlueCenterToLeftFaceStep.Instance.EndGoal(solvedPuzzle));
        }
    }
}