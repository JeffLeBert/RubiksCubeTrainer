using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveEdgeToFrontBottomStep
    {
        [Theory]
        [InlineData("F'")]
        [InlineData("F M")]
        public void No_algorithm_if_already_at_bottom_front(string moves)
        {
            var puzzle = Rotator.ApplyMoves(Solver.Solved, moves);
            var algorithms = MoveEdgeToFrontBottomStep.InstanceFrontLeft.GetPossibleAlgorithms(puzzle);

            Assert.Empty(algorithms);
        }

        [Theory]
        [InlineData("")]
        [InlineData("F")]
        [InlineData("F2")]
        [InlineData("F U2 B")]
        [InlineData("F M'")]
        [InlineData("F M' B'")]
        [InlineData("F M2")]
        [InlineData("F2 R'")]
        [InlineData("F U2")]
        [InlineData("F U")]
        public void Can_solve_front_left(string scrambleMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Solver.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var algorithmInfos = MoveEdgeToFrontBottomStep.InstanceFrontLeft.GetPossibleAlgorithms(scrambledPuzzle);

            var algorithmInfo = Assert.Single(algorithmInfos);
            var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithmInfo.Algorithms[0]);

            Assert.False(MoveEdgeToFrontBottomStep.InstanceFrontLeft.ShouldUse(solvedPuzzle));
        }
    }
}