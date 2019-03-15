using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_a_single_color
    {
        [Fact]
        public void True_if_color_is_correct()
        {
            var checker = SingleColorChecker.Create("Left", "Blue");
            Assert.True(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_negated_color_is_correct()
        {
            var checker = SingleColorChecker.Create("!Left", "Blue");
            Assert.False(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_color_is_not_correct()
        {
            var checker = SingleColorChecker.Create("Front", "Blue");
            Assert.False(checker.Matches(Puzzle.Solved));
        }

        [Fact]
        public void True_if_negated_and_color_is_not_correct()
        {
            var checker = SingleColorChecker.Create("!Front", "Blue");
            Assert.True(checker.Matches(Puzzle.Solved));
        }
    }

    public class When_negating_the_single_color_checker
    {
        [Fact]
        public void IsNot_is_true_if_negating_a_not_negated_checker()
        {
            var checker = Assert.IsType<SingleColorChecker>(
                SingleColorChecker.Create("Left", "Blue").Negate());

            Assert.True(checker.IsNot);
        }

        [Fact]
        public void IsNot_false_if_negating_a_false_single_color_checker()
        {
            var checker = Assert.IsType<SingleColorChecker>(
                SingleColorChecker.Create("!Left", "Blue").Negate());

            Assert.False(checker.IsNot);
        }
    }
}