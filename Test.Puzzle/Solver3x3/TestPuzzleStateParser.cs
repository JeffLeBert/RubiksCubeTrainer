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
            var checker = Assert.IsType<SingleColorChecker>(
                PuzzleStateParser.Parse(new XElement("State", "Left Blue"), null).Checker);

            Assert.Equal(Location.Left, checker.Location);
            Assert.Equal(PuzzleColor.Blue, checker.Color);
            Assert.False(checker.IsNot);
            Assert.False(checker.IsRotated);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var checker = Assert.IsType<SingleColorChecker>(
                PuzzleStateParser.Parse(new XElement("State", "!Left Blue"), null).Checker);

            Assert.True(checker.IsNot);
        }
    }

    public class When_parsing_edge_puzzle_states
    {
        [Fact]
        public void Can_parse_main_parts()
        {
            var checker = Assert.IsType<EdgeChecker>(
                PuzzleStateParser.Parse(new XElement("State", "LeftDown Blue White"), null).Checker);

            Assert.Equal(Location.LeftDown, checker.Location);
            Assert.Equal(Location.LeftDown.AdjacentEdge, checker.Location2);
            Assert.Equal(PuzzleColor.Blue, checker.Color);
            Assert.Equal(PuzzleColor.White, checker.Color2);
            Assert.False(checker.IsNot);
            Assert.False(checker.IsRotated);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var checker = Assert.IsType<EdgeChecker>(
                PuzzleStateParser.Parse(new XElement("State", "!LeftUp Blue White"), null).Checker);

            Assert.True(checker.IsNot);
        }

        [Fact]
        public void Edge_false_if_flipped()
        {
            var checker = Assert.IsType<EdgeChecker>(
                PuzzleStateParser.Parse(new XElement("State", "LeftDown* White Blue"), null).Checker);

            Assert.True(checker.IsRotated);
        }
    }

    public class When_parsing_corner_puzzle_states
    {
        [Fact]
        public void Parses_main_parts()
        {
            var checker = Assert.IsType<CornerChecker>(
                PuzzleStateParser.Parse(new XElement("State", "LeftFrontDown Blue White Red"), null).Checker);

            Assert.Equal(Location.LeftFrontDown, checker.Location);
            Assert.Equal(Location.DownFrontLeft, checker.Location2);
            Assert.Equal(Location.FrontLeftDown, checker.Location3);
            Assert.Equal(PuzzleColor.Blue, checker.Color);
            Assert.Equal(PuzzleColor.White, checker.Color2);
            Assert.Equal(PuzzleColor.Red, checker.Color3);
        }

        [Fact]
        public void Can_parse_is_not()
        {
            var checker = Assert.IsType<CornerChecker>(
                PuzzleStateParser.Parse(new XElement("State", "!LeftFrontDown Blue White Red"), null).Checker);

            Assert.True(checker.IsNot);
        }

        [Fact]
        public void Corner_false_if_rotated()
        {
            var checker = Assert.IsType<CornerChecker>(
                PuzzleStateParser.Parse(new XElement("State", "LeftFrontDown* Red Blue White"), null).Checker);

            Assert.True(checker.IsRotated);
        }
    }

    public class When_parsing_combination_puzzle_states
    {
        [Fact]
        public void Can_parse_multiple_comma_separated_states()
        {
            var andChecker = Assert.IsType<AndChecker>(
                PuzzleStateParser.Parse(new XElement("State", "Left Blue, LeftDown Blue White"), null).Checker);

            Assert.Collection(
                andChecker.Checkers,
                checker => Assert.Equal(Location.Left, ((SingleColorChecker)checker).Location),
                checker => Assert.Equal(Location.LeftDown, ((EdgeChecker)checker).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}, LeftDown Blue White")]
        [InlineData("Left Blue, {LeftDown}")]
        [InlineData("{LeftCenter}, {LeftDown}")]
        public void Can_parse_named_states(string xmlText)
        {
            var solver = Solver.Empty
                .With("LeftCenter", SingleColorChecker.Create("Left", "Blue"))
                .With("LeftDown", EdgeChecker.Create("LeftDown", "Blue", "White"));
            var andChecker = Assert.IsType<AndChecker>(
                PuzzleStateParser.Parse(new XElement("State", xmlText), solver).Checker);

            Assert.Collection(
                andChecker.Checkers,
                checker => Assert.Equal(Location.Left, ((SingleColorChecker)checker).Location),
                checker => Assert.Equal(Location.LeftDown, ((EdgeChecker)checker).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}", "LeftDown Blue White")]
        [InlineData("Left Blue", "{LeftDown}")]
        [InlineData("{LeftCenter}", "{LeftDown}")]
        public void Can_parse_named_states_in_separate_elements(string checker1, string checker2)
        {
            var solver = Solver.Empty
                .With("LeftCenter", SingleColorChecker.Create("Left", "Blue"))
                .With("LeftDown", EdgeChecker.Create("LeftDown", "Blue", "White"));
            var xml =
                new XElement(
                    "State",
                    new XElement("Checks", checker1),
                    new XElement("Checks", checker2));
            var andChecker = Assert.IsType<AndChecker>(
                PuzzleStateParser.Parse(xml, solver).Checker);

            Assert.Collection(
                andChecker.Checkers,
                checker => Assert.Equal(Location.Left, ((SingleColorChecker)checker).Location),
                checker => Assert.Equal(Location.LeftDown, ((EdgeChecker)checker).Location));
        }

        [Theory]
        [InlineData("{LeftCenter}", "LeftDown Blue White")]
        [InlineData("Left Blue", "{LeftDown}")]
        [InlineData("{LeftCenter}", "{LeftDown}")]
        public void Can_parse_named_states_in_Or_elements(string checker1, string checker2)
        {
            var solver = Solver.Empty
                .With("LeftCenter", SingleColorChecker.Create("Left", "Blue"))
                .With("LeftDown", EdgeChecker.Create("LeftDown", "Blue", "White"));
            var xml =
                new XElement(
                    "State",
                    new XElement(
                        "Or",
                        new XElement("Checks", checker1),
                        new XElement("Checks", checker2)));
            var orChecker = Assert.IsType<OrChecker>(
                PuzzleStateParser.Parse(xml, solver).Checker);

            Assert.Collection(
                orChecker.Checkers,
                checker => Assert.Equal(Location.Left, ((SingleColorChecker)checker).Location),
                checker => Assert.Equal(Location.LeftDown, ((EdgeChecker)checker).Location));
        }
    }
}