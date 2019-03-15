using System.Collections.Immutable;
using FakeItEasy;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_checking_AndChecker
    {
        [Fact]
        public void True_if_all_parts_are_true()
        {
            var checker1 = A.Fake<IChecker>();
            A.CallTo(() => checker1.Matches(A<Puzzle>.Ignored)).Returns(true);
            var checker2 = A.Fake<IChecker>();
            A.CallTo(() => checker2.Matches(A<Puzzle>.Ignored)).Returns(true);
            var checker3 = A.Fake<IChecker>();
            A.CallTo(() => checker3.Matches(A<Puzzle>.Ignored)).Returns(true);

            var andChecker = new AndChecker(ImmutableArray.Create(checker1, checker2, checker3));

            Assert.True(andChecker.Matches(null));
        }

        [Theory]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void False_if_any_part_is_false(bool value1, bool value2, bool value3)
        {
            var checker1 = A.Fake<IChecker>();
            A.CallTo(() => checker1.Matches(A<Puzzle>.Ignored)).Returns(value1);
            var checker2 = A.Fake<IChecker>();
            A.CallTo(() => checker2.Matches(A<Puzzle>.Ignored)).Returns(value2);
            var checker3 = A.Fake<IChecker>();
            A.CallTo(() => checker3.Matches(A<Puzzle>.Ignored)).Returns(value3);

            var andChecker = new AndChecker(ImmutableArray.Create(checker1, checker2, checker3));

            Assert.False(andChecker.Matches(null));
        }
    }

    public class When_negating_the_AndChecker
    {
        [Fact]
        public void False_if_all_parts_are_true()
        {
            var trueChecker1 = A.Fake<IChecker>();
            A.CallTo(() => trueChecker1.Matches(A<Puzzle>.Ignored)).Returns(true);
            var trueChecker2 = A.Fake<IChecker>();
            A.CallTo(() => trueChecker2.Matches(A<Puzzle>.Ignored)).Returns(true);
            var trueChecker3 = A.Fake<IChecker>();
            A.CallTo(() => trueChecker3.Matches(A<Puzzle>.Ignored)).Returns(true);

            var andChecker = new AndChecker(ImmutableArray.Create(trueChecker1, trueChecker2, trueChecker3));

            Assert.False(andChecker.Negate().Matches(null));
        }

        [Theory]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(true, true, false)]
        public void True_if_any_part_is_false(bool value1, bool value2, bool value3)
        {
            var checker1 = A.Fake<IChecker>();
            A.CallTo(() => checker1.Matches(A<Puzzle>.Ignored)).Returns(value1);
            A.CallTo(() => checker1.Negate()).ReturnsLazily(() => NegateFakeChecker(checker1));
            var checker2 = A.Fake<IChecker>();
            A.CallTo(() => checker2.Matches(A<Puzzle>.Ignored)).Returns(value2);
            A.CallTo(() => checker2.Negate()).ReturnsLazily(() => NegateFakeChecker(checker2));
            var checker3 = A.Fake<IChecker>();
            A.CallTo(() => checker3.Matches(A<Puzzle>.Ignored)).Returns(value3);
            A.CallTo(() => checker3.Negate()).ReturnsLazily(() => NegateFakeChecker(checker3));

            var andChecker = new AndChecker(ImmutableArray.Create(checker1, checker2, checker3));

            Assert.True(andChecker.Negate().Matches(null));
        }

        [Fact]
        public void Converts_to_OrChecker_with_each_part_negationed()
        {
            var checker1 = SingleColorChecker.Create("Left", "Blue");
            var checker2 = SingleColorChecker.Create("Front", "Red");
            var checker3 = SingleColorChecker.Create("Right", "Green");

            var andChecker = new AndChecker(ImmutableArray.Create<IChecker>(checker1, checker2, checker3));
            var negatedChecker = Assert.IsType<OrChecker>(andChecker.Negate());

            Assert.Collection(
                negatedChecker.Checkers,
                checker => Assert.Equal("!Left Blue", checker.ToString()),
                checker => Assert.Equal("!Front Red", checker.ToString()),
                checker => Assert.Equal("!Right Green", checker.ToString()));
        }

        private static IChecker NegateFakeChecker(IChecker checker)
        {
            var negatedChecker = A.Fake<IChecker>();
            A.CallTo(() => negatedChecker.Matches(A<Puzzle>.Ignored)).ReturnsLazily(
                callInfo => !checker.Matches((Puzzle)callInfo.Arguments[0]));

            return negatedChecker;
        }
    }
}