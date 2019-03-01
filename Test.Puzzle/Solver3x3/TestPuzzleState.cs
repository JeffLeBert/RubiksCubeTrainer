using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class TestPuzzleState
    {
        [Fact]
        public void Center_true_if_correct()
        {
            var state = new PuzzleState("Left Blue");
            Assert.True(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Center_false_if_not_correct()
        {
            var state = new PuzzleState("Front Blue");
            Assert.False(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Edge_true_if_correct()
        {
            var state = new PuzzleState("LeftDown Blue White");
            Assert.True(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Edge_false_if_not_correct()
        {
            var state = new PuzzleState("LeftUp Blue White");
            Assert.False(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Edge_false_if_flipped()
        {
            var state = new PuzzleState("LeftDown White Blue");
            Assert.False(state.Matches(Solver.Solved));
        }

        [Theory]
        [InlineData("LeftDown* Blue White")]
        [InlineData("LeftDown* White Blue")]
        public void All_edge_true_if_flipped(string stateText)
        {
            var state = new PuzzleState(stateText);
            Assert.True(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Corner_true_if_correct()
        {
            var state = new PuzzleState("LeftFrontDown Blue White Red");
            Assert.True(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Corner_false_if_rotated()
        {
            var state = new PuzzleState("LeftFrontDown Red Blue White");
            Assert.False(state.Matches(Solver.Solved));
        }

        [Theory]
        [InlineData("LeftFrontDown* Blue White Red")]
        [InlineData("LeftFrontDown* Red Blue White")]
        [InlineData("LeftFrontDown* White Red Blue")]
        public void All_corner_true_if_rotated(string stateText)
        {
            var state = new PuzzleState(stateText);
            Assert.True(state.Matches(Solver.Solved));
        }

        [Fact]
        public void Handles_multiple_checks()
        {
            var state = new PuzzleState("Left Blue, LeftDown Blue White");
            Assert.True(state.Matches(Solver.Solved));
        }
    }
}