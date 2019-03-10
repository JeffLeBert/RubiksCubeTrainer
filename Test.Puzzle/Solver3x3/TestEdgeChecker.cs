using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_an_edge
    {
        [Fact]
        public void True_if_both_colors_are_correct()
        {
            var checker = EdgeChecker.Create("LeftFront", "Blue", "Red");
            Assert.True(checker.Matches(Puzzle.Solved));
        }

        [Theory]
        [InlineData("Green", "Red")]
        [InlineData("Blue", "Green")]
        public void False_if_either_color_is_incorrect(string color1, string color2)
        {
            var checker = EdgeChecker.Create("LeftFront", color1, color2);
            Assert.False(checker.Matches(Puzzle.Solved));
        }

        [Theory]
        [InlineData("Blue", "Red")]
        [InlineData("Red", "Blue")]
        public void EdgeOrFlipped_returns_true_if_both_colors_are_correct(string color1, string color2)
        {
            var checker = EdgeChecker.Create("LeftFront*", color1, color2);
            Assert.True(checker.Matches(Puzzle.Solved));
        }

        [Theory]
        [InlineData("Green", "Red")]
        [InlineData("Blue", "Green")]
        public void EdgeOrFlipped_returns_false_if_either_color_is_incorrect(string color1, string color2)
        {
            var checker = EdgeChecker.Create("LeftFront*", color1, color2);
            Assert.False(checker.Matches(Puzzle.Solved));
        }
    }
}