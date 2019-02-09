using FakeItEasy;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestOrChecker
    {
        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        public void Test_or(bool value1, bool value2, bool expectedResult)
        {
            var checker1 = A.Fake<IChecker>();
            A.CallTo(() => checker1.Check(A<Puzzle>.Ignored)).Returns(value1);

            var checker2 = A.Fake<IChecker>();
            A.CallTo(() => checker2.Check(A<Puzzle>.Ignored)).Returns(value2);

            var checker = new OrChecker(checker1, checker2);

            Assert.Equal(expectedResult, checker.Check(null));
        }
    }
}