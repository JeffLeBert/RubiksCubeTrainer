using System.Collections.Immutable;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_parsing_center_puzzle_states
    {
        [Fact]
        public void Center_true_if_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "Left Blue"), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Center_false_if_not_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "Front Blue"), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Not_center_false_if_not_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "!Left Blue"), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Not_center_true_if_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "!Front Blue"), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }
    }

    public class When_parsing_edge_puzzle_states
    {
        [Fact]
        public void Edge_true_if_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "LeftDown Blue White"), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Edge_false_if_not_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "LeftUp Blue White"), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Edge_false_if_flipped()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "LeftDown White Blue"), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }
    }

    public class When_parsing_corner_puzzle_states
    {
        [Theory]
        [InlineData("LeftDown* Blue White")]
        [InlineData("LeftDown* White Blue")]
        public void All_edge_true_if_flipped(string stateText)
        {
            var state = PuzzleStateParser.Parse(new XElement("State", stateText), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Corner_true_if_correct()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "LeftFrontDown Blue White Red"), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Fact]
        public void Corner_false_if_rotated()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "LeftFrontDown Red Blue White"), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }

        [Theory]
        [InlineData("LeftFrontDown* Blue White Red")]
        [InlineData("LeftFrontDown* Red Blue White")]
        [InlineData("LeftFrontDown* White Red Blue")]
        public void All_corner_true_if_rotated(string stateText)
        {
            var state = PuzzleStateParser.Parse(new XElement("State", stateText), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }
    }

    public class When_parsing_combination_puzzle_states
    {
        [Fact]
        public void All_true_checks_are_true()
        {
            var state = PuzzleStateParser.Parse(new XElement("State", "Left Blue, LeftDown Blue White"), null);
            Assert.True(state.Matches(Puzzle.Solved));
        }

        [Theory]
        [InlineData("!Left Blue, LeftDown Blue White")]
        [InlineData("Left Blue, !LeftDown Blue White")]
        [InlineData("!Left Blue, !LeftDown Blue White")]
        public void All_false_checks_are_false(string stateText)
        {
            var state = PuzzleStateParser.Parse(new XElement("State", stateText), null);
            Assert.False(state.Matches(Puzzle.Solved));
        }
    }

    public class When_parsing_with_previous_step
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void FinishedState_from_previous_step_is_used(bool checksValue)
        {
            var previousStep = new Step(
                "Step1",
                null,
                new BoolChecker(checksValue),
                ImmutableArray<AlgorithmCollection>.Empty);
            var solver = Solver.Empty.With(previousStep);
            var state = PuzzleStateParser.Parse(
                new XElement(
                    "State",
                    new XAttribute("PreviousStep", "Step1"),
                    "Left Blue"),
                solver);

            Assert.Equal(checksValue, state.Matches(Puzzle.Solved));
        }

        private class BoolChecker : IChecker
        {
            private readonly bool value;

            public BoolChecker(bool value)
            {
                this.value = value;
            }

            public bool Matches(Puzzle puzzle)
                => this.value;

            public IChecker WithColors(PuzzleColor[] colors)
            {
                throw new System.NotImplementedException();
            }
        }
    }

    public class When_parsing_Or_puzzle_states
    {
        [Theory]
        [InlineData("Left Blue", "Front White", "Front Red", "Right Green")]        // First OR is wrong.
        [InlineData("Left Blue", "Front Red", "Front White", "Right Green")]        // Second OR is wrong.
        public void True_if_any_part_is_true(string beforeCheck, string orCheck1, string orCheck2, string afterCheck)
        {
            var state = PuzzleStateParser.Parse(
                new XElement(
                    "State",
                    new XElement("Checks", beforeCheck),
                    new XElement(
                        "Or",
                        new XElement("Checks", orCheck1),
                        new XElement("Checks", orCheck2)),
                    new XElement("Checks", afterCheck)),
                null);
            Assert.True(state.Matches(Puzzle.Solved));
        }
    }

    //    public class When_parsing_complex_puzzle_states
    //    {
    //        [Fact]
    //        public void MyTestMethod()
    //        {
    //            const string xmlText =
    //@"<InitialState>
    //  <Checks>!FrontDown* Blue Red</Checks>
    //  <Checks>!FrontDown* Blue Orange</Checks>
    //  <Checks>!LeftFrontUp White Red Blue, !FrontUp Red Blue</Checks>
    //  <Checks>!RightFrontUp White Red Blue, !FrontUp Blue Red</Checks>
    //  <Checks>!LeftBackUp White Red Blue, !BackUp Blue Red</Checks>
    //  <Checks>!RightBackUp White Red Blue, !BackUp Red Blue</Checks>
    //  <Or>
    //    <Checks>!LeftFront Blue Red</Checks>
    //    <Checks>!LeftFrontDown Blue White Red</Checks>
    //  </Or>
    //</InitialState>";
    //            var xml = XElement.Parse(xmlText);
    //            var checker = PuzzleStateParser.Parse(xml, null);
    //        }
    //    }
}