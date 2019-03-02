using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class When_determining_if_we_should_use_the_RotateWhiteCornerFacingOutStep
    {
        [Theory]
        [MemberData(nameof(Algorithms_for_should_use))]
        public void True_if_we_should_use(RotateWhiteCornerFacingOutStep step, string moves)
        {
            var puzzle = Rotator.ApplyMoves(Puzzle.Solved, moves);

            Assert.True(step.ShouldUse(puzzle));
        }

        public static IEnumerable<object[]> Algorithms_for_should_use()
            => new[]
            {
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F2 U" },      // UpFrontLeft
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F2" },        // UpFrontRight
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F2 U2" },     // UpBackLeft
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F2 U'" },     // UpBackRight
            };

        [Theory]
        [MemberData(nameof(Algorithms_for_should_not_use))]
        public void False_if_we_should_not_use(RotateWhiteCornerFacingOutStep step, string moves)
        {
            var puzzle = Rotator.ApplyMoves(Puzzle.Solved, moves);

            Assert.False(step.ShouldUse(puzzle));
        }

        public static IEnumerable<object[]> Algorithms_for_should_not_use()
            => new[]
            {
                // UpFrontLeft
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F" },
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "L'" },

                // UpFrontRight
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F U'" },
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "L' U'" },

                // UpBackLeft
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F U" },
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "L' U" },

                // UpBackRight
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "F U" },
                new object[] { RotateWhiteCornerFacingOutStep.InstanceDownFrontLeft, "L' U" },
            };
    }
}