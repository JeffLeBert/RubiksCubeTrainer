using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_a_single_color
    {
        [Fact]
        public void True_if_color_is_correct()
        {
            var checker = new SingleColorChecker("Left", "Blue");
            Assert.True(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_not_color_is_correct()
        {
            var checker = new SingleColorChecker("!Left", "Blue");
            Assert.False(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_color_is_correct()
        {
            var checker = new SingleColorChecker("Front", "Blue");
            Assert.False(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void True_if_not_color_is_correct()
        {
            var checker = new SingleColorChecker("!Front", "Blue");
            Assert.True(checker.Matches(Puzzle.Solved));
        }
    }
}