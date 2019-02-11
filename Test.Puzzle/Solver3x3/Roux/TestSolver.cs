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
                new object[] { Location.BackCenter, PuzzleColor.Orange },
                new object[] { Location.DownCenter, PuzzleColor.White },
                new object[] { Location.FrontCenter, PuzzleColor.Red },
                new object[] { Location.LeftCenter, PuzzleColor.Blue },
                new object[] { Location.RightCenter, PuzzleColor.Green },
                new object[] { Location.UpCenter, PuzzleColor.Yellow }
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