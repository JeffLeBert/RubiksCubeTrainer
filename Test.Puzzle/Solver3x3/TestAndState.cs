using System.Collections.Immutable;
using FakeItEasy;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_AndState
    {
        [Fact]
        public void True_if_all_parts_are_true()
        {
            var state1 = A.Fake<IState>();
            A.CallTo(() => state1.Matches(A<Puzzle>.Ignored)).Returns(true);
            var state2 = A.Fake<IState>();
            A.CallTo(() => state2.Matches(A<Puzzle>.Ignored)).Returns(true);
            var state3 = A.Fake<IState>();
            A.CallTo(() => state3.Matches(A<Puzzle>.Ignored)).Returns(true);

            var andState = new AndState(ImmutableArray.Create(state1, state2, state3));

            Assert.True(andState.Matches(null));
        }

        [Theory]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void False_if_any_part_is_false(bool value1, bool value2, bool value3)
        {
            var state1 = A.Fake<IState>();
            A.CallTo(() => state1.Matches(A<Puzzle>.Ignored)).Returns(value1);
            var state2 = A.Fake<IState>();
            A.CallTo(() => state2.Matches(A<Puzzle>.Ignored)).Returns(value2);
            var state3 = A.Fake<IState>();
            A.CallTo(() => state3.Matches(A<Puzzle>.Ignored)).Returns(value3);

            var andState = new AndState(ImmutableArray.Create(state1, state2, state3));

            Assert.False(andState.Matches(null));
        }
    }

    public class When_negating_the_AndState
    {
        [Fact]
        public void False_if_all_parts_are_true()
        {
            var trueState1 = A.Fake<IState>();
            A.CallTo(() => trueState1.Matches(A<Puzzle>.Ignored)).Returns(true);
            var trueState2 = A.Fake<IState>();
            A.CallTo(() => trueState2.Matches(A<Puzzle>.Ignored)).Returns(true);
            var trueState3 = A.Fake<IState>();
            A.CallTo(() => trueState3.Matches(A<Puzzle>.Ignored)).Returns(true);

            var andState = new AndState(ImmutableArray.Create(trueState1, trueState2, trueState3));

            Assert.False(andState.Negate().Matches(null));
        }

        [Theory]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void True_if_any_part_is_false(bool value1, bool value2, bool value3)
        {
            var state1 = A.Fake<IState>();
            A.CallTo(() => state1.Matches(A<Puzzle>.Ignored)).Returns(value1);
            A.CallTo(() => state1.Negate()).ReturnsLazily(() => NegateFakeState(state1));
            var state2 = A.Fake<IState>();
            A.CallTo(() => state2.Matches(A<Puzzle>.Ignored)).Returns(value2);
            A.CallTo(() => state2.Negate()).ReturnsLazily(() => NegateFakeState(state2));
            var state3 = A.Fake<IState>();
            A.CallTo(() => state3.Matches(A<Puzzle>.Ignored)).Returns(value3);
            A.CallTo(() => state3.Negate()).ReturnsLazily(() => NegateFakeState(state3));

            var andState = new AndState(ImmutableArray.Create(state1, state2, state3));

            Assert.True(andState.Negate().Matches(null));
        }

        [Fact]
        public void Converts_to_OrState_with_each_part_negationed()
        {
            var state1 = SingleColorState.Create("Left", "Blue");
            var state2 = SingleColorState.Create("Front", "Red");
            var stete3 = SingleColorState.Create("Right", "Green");

            var andState = new AndState(ImmutableArray.Create<IState>(state1, state2, stete3));
            var negatedState = Assert.IsType<OrState>(andState.Negate());

            Assert.Collection(
                negatedState.States,
                state => Assert.Equal("!Left Blue", state.ToString()),
                state => Assert.Equal("!Front Red", state.ToString()),
                state => Assert.Equal("!Right Green", state.ToString()));
        }

        private static IState NegateFakeState(IState state)
        {
            var negatedState = A.Fake<IState>();
            A.CallTo(() => negatedState.Matches(A<Puzzle>.Ignored)).ReturnsLazily(
                callInfo => !state.Matches((Puzzle)callInfo.Arguments[0]));

            return negatedState;
        }
    }
}