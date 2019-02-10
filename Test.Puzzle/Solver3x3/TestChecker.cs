using RubiksCubeTrainer.Puzzle3x3;
using RubiksCubeTrainer.Solver3x3.Roux;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestChecker
    {
        [Fact]
        public void Single_color_returns_true_if_color_is_correct()
        {
            var checker = Checker.SingleColor(new Location(FaceName.Left, 0, 0), PuzzleColor.Blue);

            Assert.True(checker(Solver.Solved));
        }

        [Fact]
        public void Single_color_returns_false_if_color_is_correct()
        {
            var checker = Checker.SingleColor(new Location(FaceName.Front, 0, 0), PuzzleColor.Blue);

            Assert.False(checker(Solver.Solved));
        }

        [Fact]
        public void Edge_returns_true_if_both_colors_are_correct()
        {
            var checker = Checker.Edge(new Location(FaceName.Left, -1, -1, 0), PuzzleColor.Blue, PuzzleColor.Red);

            Assert.True(checker(Solver.Solved));
        }

        [Theory]
        [InlineData(PuzzleColor.Green, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Green)]
        public void Edge_returns_false_if_either_color_is_incorrect(PuzzleColor color1, PuzzleColor color2)
        {
            var checker = Checker.Edge(new Location(FaceName.Left, -1, -1, 0), color1, color2);

            Assert.False(checker(Solver.Solved));
        }

        [Theory]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Red, PuzzleColor.Blue)]
        public void EdgeOrFlipped_returns_true_if_both_colors_are_correct(PuzzleColor color1, PuzzleColor color2)
        {
            var checker = Checker.EdgeOrFlipped(new Location(FaceName.Left, -1, -1, 0), color1, color2);

            Assert.True(checker(Solver.Solved));
        }

        [Theory]
        [InlineData(PuzzleColor.Green, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Green)]
        public void EdgeOrFlipped_returns_false_if_either_color_is_incorrect(PuzzleColor color1, PuzzleColor color2)
        {
            var checker = Checker.EdgeOrFlipped(new Location(FaceName.Left, -1, -1, 0), color1, color2);

            Assert.False(checker(Solver.Solved));
        }
    }
}