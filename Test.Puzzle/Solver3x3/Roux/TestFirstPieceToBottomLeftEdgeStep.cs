using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestFirstPieceToBottomLeftEdgeStep
    {
        private static readonly Step step = WellKnownSolvers.Roux.Steps["FirstPieceToBottomLeftEdge"];

        [Fact]
        public void No_algorithms_if_already_at_goal()
        {
            var algorithms = step.GetPossibleAlgorithms(Puzzle.Solved);

            Assert.Empty(algorithms);
        }

        [Theory]
        // Left side...
        [InlineData("L' F' D'")]        // Down face, left side, flip.
        [InlineData("L")]               // Back face, left side, no flip.
        [InlineData("D' B'")]           // Back face, left side, flip.
        [InlineData("L2")]              // Top face, left side, no flip.
        [InlineData("D' B' L")]         // Top face, left side, flip.
        [InlineData("L'")]              // Front face, left side, no flip.
        [InlineData("D F")]             // Front face, left side, flip.
        // Middle slice...
        [InlineData("D'")]              // Back face, down side, no flip.
        [InlineData("L B")]             // Back face, down side, flip.
        [InlineData("L2 U")]            // Back face, up side, no flip.
        [InlineData("L B'")]            // Back face, up side, flip.
        [InlineData("D")]               // Front face, down side, no flip.
        [InlineData("L' F'")]           // Front face, down side, flip.
        [InlineData("D F2")]            // Front face, up side, no flip.
        [InlineData("L' F")]            // Front face, up side, flip.
        // Right side...
        [InlineData("D2")]              // Right face, down side, no flip.
        [InlineData("D F' R'")]         // Right face, down side, flip.
        [InlineData("D' B")]            // Right face, back side, no flip.
        [InlineData("D2 R'")]           // Right face, back side, flip.
        [InlineData("D F'")]            // Right face, front side, no flip.
        [InlineData("D2 R")]            // Right face, front side, flip.
        [InlineData("L2 U2")]           // Right face, up side, no flip.
        [InlineData("L' F U'")]         // Right face, up side, flip.
        public void Can_solve(string scrambleMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Puzzle.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var algorithmInfos = step.GetPossibleAlgorithms(scrambledPuzzle);

            var algorithmInfo = Assert.Single(algorithmInfos);
            foreach (var algorithm in algorithmInfo.Algorithms)
            {
                var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithm);

                Assert.True(step.FinishedState(solvedPuzzle));
            }
        }
    }
}