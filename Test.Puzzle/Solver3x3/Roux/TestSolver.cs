using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class When_looking_at_a_solved_cube
    {
        [Theory]
        [InlineData(FaceName.Up, PuzzleColor.Yellow)]
        [InlineData(FaceName.Front, PuzzleColor.Red)]
        [InlineData(FaceName.Down, PuzzleColor.White)]
        [InlineData(FaceName.Back, PuzzleColor.Orange)]
        [InlineData(FaceName.Left, PuzzleColor.Blue)]
        [InlineData(FaceName.Right, PuzzleColor.Green)]
        public void Solved_puzzle_is_rotated_correctly(FaceName faceName, PuzzleColor centerColor)
        {
            Assert.Equal(centerColor, Solver.Solved[new Location(faceName, 0, 0)]);
        }
    }

    public class When_getting_the_next_step
    {
        [Fact]
        public void Move_blue_center_to_left_face_is_first()
        {
            var solver = new Solver();
            var puzzle = Rotator.ApplyMove(
                Solver.Solved,
                new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise));

            var nextStep = Assert.Single(solver.NextSteps(puzzle));
            Assert.IsType<MoveBlueCenterToLeftFaceStep>(nextStep);
        }

        [Fact]
        public void First_piece_to_bottom_left_edge_is_second()
        {
            var solver = new Solver();
            var puzzle = Rotator.ApplyMove(
                Solver.Solved,
                new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise));

            var nextStep = Assert.Single(solver.NextSteps(puzzle));
            Assert.IsType<FirstPieceToBottomLeftEdgeStep>(nextStep);
        }
    }
}