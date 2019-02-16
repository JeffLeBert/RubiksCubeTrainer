using RubiksCubeTrainer.Puzzle3x3;
using RubiksCubeTrainer.Solver3x3.Roux;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestChecker
    {
        [Fact]
        public void Single_color_returns_true_if_color_is_correct()
            => Assert.True(Checker.SingleColor(Solver.Solved, Location.Left, PuzzleColor.Blue));

        [Fact]
        public void Single_color_returns_false_if_color_is_correct()
            => Assert.False(Checker.SingleColor(Solver.Solved, Location.Front, PuzzleColor.Blue));

        [Fact]
        public void Edge_returns_true_if_both_colors_are_correct()
            => Assert.True(Checker.Edge(Solver.Solved, Location.LeftFront, PuzzleColor.Blue, PuzzleColor.Red));

        [Theory]
        [InlineData(PuzzleColor.Green, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Green)]
        public void Edge_returns_false_if_either_color_is_incorrect(PuzzleColor color1, PuzzleColor color2)
            => Assert.False(Checker.Edge(Solver.Solved, Location.LeftFront, color1, color2));

        [Theory]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Red, PuzzleColor.Blue)]
        public void EdgeOrFlipped_returns_true_if_both_colors_are_correct(PuzzleColor color1, PuzzleColor color2)
            => Assert.True(Checker.EdgeOrFlipped(Solver.Solved, Location.LeftFront, color1, color2));

        [Theory]
        [InlineData(PuzzleColor.Green, PuzzleColor.Red)]
        [InlineData(PuzzleColor.Blue, PuzzleColor.Green)]
        public void EdgeOrFlipped_returns_false_if_either_color_is_incorrect(PuzzleColor color1, PuzzleColor color2)
            => Assert.False(Checker.EdgeOrFlipped(Solver.Solved, Location.LeftFront, color1, color2));
    }
}