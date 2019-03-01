using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveBlueCenterToLeftFaceStep
    {
        private static Step step = Solver.Roux.GetStep("MoveBlueCenterToLeft");

        [Fact]
        public void No_algorithms_if_already_at_goal()
        {
            var algorithms = step.GetPossibleAlgorithms(Solver.Solved);

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

            var algorithmInfos = step.GetPossibleAlgorithms(scrambledPuzzle);

            var algorithmInfo = Assert.Single(algorithmInfos);
            foreach (var algorithm in algorithmInfo.Algorithms)
            {
                var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithm);

                Assert.True(step.FinishedState.Matches(solvedPuzzle));
            }
        }
    }
}