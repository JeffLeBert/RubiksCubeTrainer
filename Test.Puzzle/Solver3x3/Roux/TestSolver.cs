using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class When_looking_at_a_solved_cube
    {
        [Theory]
        [MemberData(nameof(AllCentersAndColors))]
        public void Solved_puzzle_is_rotated_correctly(Location location, PuzzleColor centerColor)
        {
            Assert.Equal(centerColor, Solver.Solved[location]);
        }

        public static IEnumerable<object[]> AllCentersAndColors()
            => new[]
            {
                new object[] { Location.Back, PuzzleColor.Orange },
                new object[] { Location.Down, PuzzleColor.White },
                new object[] { Location.Front, PuzzleColor.Red },
                new object[] { Location.Left, PuzzleColor.Blue },
                new object[] { Location.Right, PuzzleColor.Green },
                new object[] { Location.Up, PuzzleColor.Yellow }
            };
    }

    public class When_getting_the_next_step
    {
        [Fact]
        public void Move_blue_center_to_left_face_is_first()
        {
            var solver = new Solver();
            var puzzle = Rotator.ApplyMove(Solver.Solved, NotationMoveType.AllFrontClockwise);

            var nextStep = Assert.Single(solver.NextSteps(puzzle));
            Assert.IsType<MoveBlueCenterToLeftFaceStep>(nextStep);
        }

        [Fact]
        public void First_piece_to_bottom_left_edge_is_second()
        {
            var solver = new Solver();
            var puzzle = Rotator.ApplyMove(Solver.Solved, NotationMoveType.DownClockwise);

            var nextStep = Assert.Single(solver.NextSteps(puzzle));
            Assert.IsType<FirstPieceToBottomLeftEdgeStep>(nextStep);
        }
    }
}