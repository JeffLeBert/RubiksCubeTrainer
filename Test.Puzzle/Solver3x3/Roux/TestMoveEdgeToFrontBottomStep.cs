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
            var algorithms = MoveEdgeToFrontBottomStep.InstanceBlueRed.GetPossibleAlgorithms(Solver.Solved);

            Assert.Empty(algorithms);
        }

        //[Theory]
        //[InlineData("F")]
        //public void Can_solve(string scrambleMoves)
        //{
        //    var scrambledPuzzle = Rotator.ApplyMoves(Solver.Solved, NotationParser.EnumerateMoves(scrambleMoves));

        //    var algorithmInfos = MoveEdgeToFrontBottomStep.InstanceBlueRed.GetPossibleAlgorithms(scrambledPuzzle);

        //    var algorithmInfo = Assert.Single(algorithmInfos);
        //    var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithmInfo.Algorithm.Moves);

        //    Assert.True(MoveEdgeToFrontBottomStep.InstanceBlueRed.EndGoal.Check(solvedPuzzle));
        //}
    }
}