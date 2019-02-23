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
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "F" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "FU" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "FU2" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "FU'" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "B'" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "B'U" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "B'U2" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "B'U'" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "F'" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "F'U" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "F'U2" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "F'U'" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "B" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "BU" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "BU2" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "BU'" },
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
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "D" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "D2" },
                new object[] { MoveCornerToTopStep.InstanceLeftBackDown, "D'" },

                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "D" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "D2" },
                new object[] { MoveCornerToTopStep.InstanceRightBackDown, "D'" },

                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "D" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "D2" },
                new object[] { MoveCornerToTopStep.InstanceLeftFrontDown, "D'" },

                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "D" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "D2" },
                new object[] { MoveCornerToTopStep.InstanceRightFrontDown, "D'" }
            };
    }
}