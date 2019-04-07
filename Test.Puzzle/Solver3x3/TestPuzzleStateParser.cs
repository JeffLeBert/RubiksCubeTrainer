using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3
{
    public class When_parsing_single_color_puzzle_states
    {
        [Fact]
        public void Can_parse_the_main_parts()
        {
            var state = Assert.IsType<SingleColorState>(
                StateParser.Parse(null, null, new XElement("State", "Left Blue"), null).State);

            Assert.Equal(Location.Left, state.Location);
            Assert.Equal(PuzzleColor.Blue, state.Color);
            Assert.False(state.IsNot);
            Assert.False(state.IsRotated);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var state = Assert.IsType<SingleColorState>(
                StateParser.Parse(null, null, new XElement("State", "!Left Blue"), null).State);

            Assert.True(state.IsNot);
        }
    }

    public class When_parsing_edge_puzzle_states
    {
        [Fact]
        public void Can_parse_main_parts()
        {
            var state = Assert.IsType<EdgeState>(
                StateParser.Parse(null, null, new XElement("State", "LeftDown Blue White"), null).State);

            Assert.Equal(Location.LeftDown, state.Location);
            Assert.Equal(Location.LeftDown.AdjacentEdge, state.Location2);
            Assert.Equal(PuzzleColor.Blue, state.Color);
            Assert.Equal(PuzzleColor.White, state.Color2);
            Assert.False(state.IsNot);
            Assert.False(state.IsRotated);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var state = Assert.IsType<EdgeState>(
                StateParser.Parse(null, null, new XElement("State", "!LeftUp Blue White"), null).State);

            Assert.True(state.IsNot);
        }

        [Fact]
        public void Edge_false_if_flipped()
        {
            var state = Assert.IsType<EdgeState>(
                StateParser.Parse(null, null, new XElement("State", "LeftDown* White Blue"), null).State);

            Assert.True(state.IsRotated);
        }
    }

    public class When_parsing_corner_puzzle_states
    {
        [Fact]
        public void Parses_main_parts()
        {
            var state = Assert.IsType<CornerState>(
                StateParser.Parse(null, null, new XElement("State", "LeftFrontDown Blue White Red"), null).State);

            Assert.Equal(Location.LeftFrontDown, state.Location);
            Assert.Equal(Location.DownLeftFront, state.Location2);
            Assert.Equal(Location.FrontLeftDown, state.Location3);
            Assert.Equal(PuzzleColor.Blue, state.Color);
            Assert.Equal(PuzzleColor.White, state.Color2);
            Assert.Equal(PuzzleColor.Red, state.Color3);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var state = Assert.IsType<CornerState>(
                StateParser.Parse(null, null, new XElement("State", "!LeftFrontDown Blue White Red"), null).State);

            Assert.True(state.IsNot);
        }

        [Fact]
        public void Corner_false_if_rotated()
        {
            var state = Assert.IsType<CornerState>(
                StateParser.Parse(null, null, new XElement("State", "LeftFrontDown* Red Blue White"), null).State);

            Assert.True(state.IsRotated);
        }
    }

    public class When_parsing_combination_puzzle_states
    {
        [Fact]
        public void Can_parse_multiple_comma_separated_states()
        {
            var andState = Assert.IsType<AndState>(
                StateParser.Parse(null, null, new XElement("State", "Left Blue, LeftDown Blue White"), null).State);

            Assert.Collection(
                andState.States,
                state => Assert.Equal(Location.Left, ((SingleColorState)state).Location),
                state => Assert.Equal(Location.LeftDown, ((EdgeState)state).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}, LeftDown Blue White")]
        [InlineData("Left Blue, {LeftDown}")]
        [InlineData("{LeftCenter}, {LeftDown}")]
        public void Can_parse_named_states(string xmlText)
        {
            var solver = Solver.Empty
                .WithState("LeftCenter", SingleColorState.Create("Left", "Blue"))
                .WithState("LeftDown", EdgeState.Create("LeftDown", "Blue", "White"));
            var andState = Assert.IsType<AndState>(
                StateParser.Parse(null, null, new XElement("State", xmlText), solver).State);

            Assert.Collection(
                andState.States,
                state => Assert.Equal(Location.Left, ((SingleColorState)state).Location),
                state => Assert.Equal(Location.LeftDown, ((EdgeState)state).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}", "LeftDown Blue White")]
        [InlineData("Left Blue", "{LeftDown}")]
        [InlineData("{LeftCenter}", "{LeftDown}")]
        public void Can_parse_named_states_in_separate_elements(string state1, string state2)
        {
            var solver = Solver.Empty
                .WithState("LeftCenter", SingleColorState.Create("Left", "Blue"))
                .WithState("LeftDown", EdgeState.Create("LeftDown", "Blue", "White"));
            var xml =
                new XElement(
                    "State",
                    new XElement("State", state1),
                    new XElement("State", state2));
            var andState = Assert.IsType<AndState>(
                StateParser.Parse(null, null, xml, solver).State);

            Assert.Collection(
                andState.States,
                state => Assert.Equal(Location.Left, ((SingleColorState)state).Location),
                state => Assert.Equal(Location.LeftDown, ((EdgeState)state).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}", "LeftDown Blue White")]
        [InlineData("Left Blue", "{LeftDown}")]
        [InlineData("{LeftCenter}", "{LeftDown}")]
        public void Can_parse_named_states_in_Or_elements(string state1, string state2)
        {
            var solver = Solver.Empty
                .WithState("LeftCenter", SingleColorState.Create("Left", "Blue"))
                .WithState("LeftDown", EdgeState.Create("LeftDown", "Blue", "White"));
            var xml =
                new XElement(
                    "State",
                    new XElement(
                        "Or",
                        new XElement("State", state1),
                        new XElement("State", state2)));
            var orState = Assert.IsType<OrState>(
                StateParser.Parse(null, null, xml, solver).State);

            Assert.Collection(
                orState.States,
                state => Assert.Equal(Location.Left, ((SingleColorState)state).Location),
                state => Assert.Equal(Location.LeftDown, ((EdgeState)state).Location));
        }
    }
}