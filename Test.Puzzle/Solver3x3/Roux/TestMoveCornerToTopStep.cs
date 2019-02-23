using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveCornerToTopStep
    {
        [Theory]
        [MemberData(nameof(No_algorithms_already_at_top_enumerator))]
        public void No_LeftFrontDonw_algorithm_if_already_at_top(IStep step, string moves)
        {
            var puzzle = Rotator.ApplyMoves(Solver.Solved, moves);
            var algorithms = step.GetPossibleAlgorithms(puzzle);

            Assert.Empty(algorithms);
        }

        public static IEnumerable<object[]> No_algorithms_already_at_top_enumerator()
            => new[]
            {
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "F" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "FU" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "FU2" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "FU'" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "B'" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "B'U" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "B'U2" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "B'U'" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "F'" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "F'U" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "F'U2" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "F'U'" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "B" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "BU" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "BU2" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "BU'" },
            };

        [Theory]
        [MemberData(nameof(Can_move_to_top_enumerator))]
        public void LeftFrontDown_can_move_to_top(IStep step, string scrambleMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Solver.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var algorithmInfos = step.GetPossibleAlgorithms(scrambledPuzzle);

            var algorithmInfo = Assert.Single(algorithmInfos);
            var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithmInfo.Moves);

            Assert.False(step.ShouldUse(solvedPuzzle));
        }

        public static IEnumerable<object[]> Can_move_to_top_enumerator()
            => new[]
            {
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "D" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "D2" },
                new object[] { MoveCornerToTopStep.InstanceDownBackLeft, "D'" },

                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "D" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "D2" },
                new object[] { MoveCornerToTopStep.InstanceDownBackRight, "D'" },

                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "D" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "D2" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontLeft, "D'" },

                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "D" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "D2" },
                new object[] { MoveCornerToTopStep.InstanceDownFrontRight, "D'" }
            };
    }
}