using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestPuzzleState
    {
        [Fact]
        public void Center_true_if_correct()
        {
            var state = PuzzleStateParser.Parse("Left Blue");
            Assert.True(state(Puzzle.Solved));
        }

        [Fact]
        public void Center_false_if_not_correct()
        {
            var state = PuzzleStateParser.Parse("Front Blue");
            Assert.False(state(Puzzle.Solved));
        }

        [Fact]
        public void Edge_true_if_correct()
        {
            var state = PuzzleStateParser.Parse("LeftDown Blue White");
            Assert.True(state(Puzzle.Solved));
        }

        [Fact]
        public void Edge_false_if_not_correct()
        {
            var state = PuzzleStateParser.Parse("LeftUp Blue White");
            Assert.False(state(Puzzle.Solved));
        }

        [Fact]
        public void Edge_false_if_flipped()
        {
            var state = PuzzleStateParser.Parse("LeftDown White Blue");
            Assert.False(state(Puzzle.Solved));
        }

        [Theory]
        [InlineData("LeftDown* Blue White")]
        [InlineData("LeftDown* White Blue")]
        public void All_edge_true_if_flipped(string stateText)
        {
            var state = PuzzleStateParser.Parse(stateText);
            Assert.True(state(Puzzle.Solved));
        }

        [Fact]
        public void Corner_true_if_correct()
        {
            var state = PuzzleStateParser.Parse("LeftFrontDown Blue White Red");
            Assert.True(state(Puzzle.Solved));
        }

        [Fact]
        public void Corner_false_if_rotated()
        {
            var state = PuzzleStateParser.Parse("LeftFrontDown Red Blue White");
            Assert.False(state(Puzzle.Solved));
        }

        [Theory]
        [InlineData("LeftFrontDown* Blue White Red")]
        [InlineData("LeftFrontDown* Red Blue White")]
        [InlineData("LeftFrontDown* White Red Blue")]
        public void All_corner_true_if_rotated(string stateText)
        {
            var state = PuzzleStateParser.Parse(stateText);
            Assert.True(state(Puzzle.Solved));
        }

        [Fact]
        public void Handles_multiple_checks()
        {
            var state = PuzzleStateParser.Parse("Left Blue, LeftDown Blue White");
            Assert.True(state(Puzzle.Solved));
        }
    }
}