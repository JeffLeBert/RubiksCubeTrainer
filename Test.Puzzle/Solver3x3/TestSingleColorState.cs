using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_a_single_color
    {
        [Fact]
        public void True_if_color_is_correct()
        {
            var state = SingleColorState.Create("Left", "Blue");
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_negated_color_is_correct()
        {
            var state = SingleColorState.Create("!Left", "Blue");
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void False_if_color_is_not_correct()
        {
            var state = SingleColorState.Create("Front", "Blue");
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void True_if_negated_and_color_is_not_correct()
        {
            var state = SingleColorState.Create("!Front", "Blue");
            Assert.True(state.Matches(Puzzle.Solved));
        }
    }

    public class When_negating_the_single_color_state
    {
        [Fact]
        public void IsNot_is_true_if_negating_a_not_negated_state()
        {
            var state = Assert.IsType<SingleColorState>(
                SingleColorState.Create("Left", "Blue").Negate());

            Assert.True(state.IsNot);
        }

        [Fact]
        public void IsNot_false_if_negating_a_false_single_color_state()
        {
            var state = Assert.IsType<SingleColorState>(
                SingleColorState.Create("!Left", "Blue").Negate());

            Assert.False(state.IsNot);
        }
    }
}