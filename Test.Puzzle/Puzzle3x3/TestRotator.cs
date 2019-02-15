using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class When_rotating_the_puzzle
    {
        private static readonly Puzzle Solved = BuildOldSolvedPuzzle();

        [Theory]
        [InlineData(
            NotationRotationNames.Right,
            NotationRotationType.Clockwise,
            "    WWO",
            "    WWO",
            "    WWO",
            "BBB OOY GGG WRR",
            "BBB OOY GGG WRR",
            "BBB OOY GGG WRR",
            "    YYR",
            "    YYR",
            "    YYR")]
        [InlineData(
            NotationRotationNames.Right,
            NotationRotationType.CounterClockwise,
            "    WWR",
            "    WWR",
            "    WWR",
            "BBB OOW GGG YRR",
            "BBB OOW GGG YRR",
            "BBB OOW GGG YRR",
            "    YYO",
            "    YYO",
            "    YYO")]
        [InlineData(
            NotationRotationNames.Right,
            NotationRotationType.Double,
            "    WWY",
            "    WWY",
            "    WWY",
            "BBB OOR GGG ORR",
            "BBB OOR GGG ORR",
            "BBB OOR GGG ORR",
            "    YYW",
            "    YYW",
            "    YYW")]
        [InlineData(
            NotationRotationNames.Left,
            NotationRotationType.Clockwise,
            "    RWW",
            "    RWW",
            "    RWW",
            "BBB WOO GGG RRY",
            "BBB WOO GGG RRY",
            "BBB WOO GGG RRY",
            "    OYY",
            "    OYY",
            "    OYY")]
        [InlineData(
            NotationRotationNames.Left,
            NotationRotationType.CounterClockwise,
            "    OWW",
            "    OWW",
            "    OWW",
            "BBB YOO GGG RRW",
            "BBB YOO GGG RRW",
            "BBB YOO GGG RRW",
            "    RYY",
            "    RYY",
            "    RYY")]
        [InlineData(
            NotationRotationNames.Left,
            NotationRotationType.Double,
            "    YWW",
            "    YWW",
            "    YWW",
            "BBB ROO GGG RRO",
            "BBB ROO GGG RRO",
            "BBB ROO GGG RRO",
            "    WYY",
            "    WYY",
            "    WYY")]
        [InlineData(
            NotationRotationNames.Up,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "OOO GGG RRR BBB",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Up,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "RRR BBB OOO GGG",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Up,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "GGG RRR BBB OOO",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Down,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "RRR BBB OOO GGG",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Down,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "OOO GGG RRR BBB",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Down,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "BBB OOO GGG RRR",
            "GGG RRR BBB OOO",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Front,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    BBB",
            "BBY OOO WGG RRR",
            "BBY OOO WGG RRR",
            "BBY OOO WGG RRR",
            "    GGG",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Front,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    GGG",
            "BBW OOO YGG RRR",
            "BBW OOO YGG RRR",
            "BBW OOO YGG RRR",
            "    BBB",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Front,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    YYY",
            "BBG OOO BGG RRR",
            "BBG OOO BGG RRR",
            "BBG OOO BGG RRR",
            "    WWW",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.Back,
            NotationRotationType.Clockwise,
            "    GGG",
            "    WWW",
            "    WWW",
            "WBB OOO GGY RRR",
            "WBB OOO GGY RRR",
            "WBB OOO GGY RRR",
            "    YYY",
            "    YYY",
            "    BBB")]
        [InlineData(
            NotationRotationNames.Back,
            NotationRotationType.CounterClockwise,
            "    BBB",
            "    WWW",
            "    WWW",
            "YBB OOO GGW RRR",
            "YBB OOO GGW RRR",
            "YBB OOO GGW RRR",
            "    YYY",
            "    YYY",
            "    GGG")]
        [InlineData(
            NotationRotationNames.Back,
            NotationRotationType.Double,
            "    YYY",
            "    WWW",
            "    WWW",
            "GBB OOO GGB RRR",
            "GBB OOO GGB RRR",
            "GBB OOO GGB RRR",
            "    YYY",
            "    YYY",
            "    WWW")]
        [InlineData(
            NotationRotationNames.WideRight,
            NotationRotationType.Clockwise,
            "    WOO",
            "    WOO",
            "    WOO",
            "BBB OYY GGG WWR",
            "BBB OYY GGG WWR",
            "BBB OYY GGG WWR",
            "    YRR",
            "    YRR",
            "    YRR")]
        [InlineData(
            NotationRotationNames.WideRight,
            NotationRotationType.CounterClockwise,
            "    WRR",
            "    WRR",
            "    WRR",
            "BBB OWW GGG YYR",
            "BBB OWW GGG YYR",
            "BBB OWW GGG YYR",
            "    YOO",
            "    YOO",
            "    YOO")]
        [InlineData(
            NotationRotationNames.WideRight,
            NotationRotationType.Double,
            "    WYY",
            "    WYY",
            "    WYY",
            "BBB ORR GGG OOR",
            "BBB ORR GGG OOR",
            "BBB ORR GGG OOR",
            "    YWW",
            "    YWW",
            "    YWW")]
        [InlineData(
            NotationRotationNames.WideLeft,
            NotationRotationType.Clockwise,
            "    RRW",
            "    RRW",
            "    RRW",
            "BBB WWO GGG RYY",
            "BBB WWO GGG RYY",
            "BBB WWO GGG RYY",
            "    OOY",
            "    OOY",
            "    OOY")]
        [InlineData(
            NotationRotationNames.WideLeft,
            NotationRotationType.CounterClockwise,
            "    OOW",
            "    OOW",
            "    OOW",
            "BBB YYO GGG RWW",
            "BBB YYO GGG RWW",
            "BBB YYO GGG RWW",
            "    RRY",
            "    RRY",
            "    RRY")]
        [InlineData(
            NotationRotationNames.WideLeft,
            NotationRotationType.Double,
            "    YYW",
            "    YYW",
            "    YYW",
            "BBB RRO GGG ROO",
            "BBB RRO GGG ROO",
            "BBB RRO GGG ROO",
            "    WWY",
            "    WWY",
            "    WWY")]
        [InlineData(
            NotationRotationNames.WideUp,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "OOO GGG RRR BBB",
            "OOO GGG RRR BBB",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideUp,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "RRR BBB OOO GGG",
            "RRR BBB OOO GGG",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideUp,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "GGG RRR BBB OOO",
            "GGG RRR BBB OOO",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideDown,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "RRR BBB OOO GGG",
            "RRR BBB OOO GGG",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideDown,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "OOO GGG RRR BBB",
            "OOO GGG RRR BBB",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideDown,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "GGG RRR BBB OOO",
            "GGG RRR BBB OOO",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideFront,
            NotationRotationType.Clockwise,
            "    WWW",
            "    BBB",
            "    BBB",
            "BYY OOO WWG RRR",
            "BYY OOO WWG RRR",
            "BYY OOO WWG RRR",
            "    GGG",
            "    GGG",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideFront,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    GGG",
            "    GGG",
            "BWW OOO YYG RRR",
            "BWW OOO YYG RRR",
            "BWW OOO YYG RRR",
            "    BBB",
            "    BBB",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideFront,
            NotationRotationType.Double,
            "    WWW",
            "    YYY",
            "    YYY",
            "BGG OOO BBG RRR",
            "BGG OOO BBG RRR",
            "BGG OOO BBG RRR",
            "    WWW",
            "    WWW",
            "    YYY")]
        [InlineData(
            NotationRotationNames.WideBack,
            NotationRotationType.Clockwise,
            "    GGG",
            "    GGG",
            "    WWW",
            "WWB OOO GYY RRR",
            "WWB OOO GYY RRR",
            "WWB OOO GYY RRR",
            "    YYY",
            "    BBB",
            "    BBB")]
        [InlineData(
            NotationRotationNames.WideBack,
            NotationRotationType.CounterClockwise,
            "    BBB",
            "    BBB",
            "    WWW",
            "YYB OOO GWW RRR",
            "YYB OOO GWW RRR",
            "YYB OOO GWW RRR",
            "    YYY",
            "    GGG",
            "    GGG")]
        [InlineData(
            NotationRotationNames.WideBack,
            NotationRotationType.Double,
            "    YYY",
            "    YYY",
            "    WWW",
            "GGB OOO GBB RRR",
            "GGB OOO GBB RRR",
            "GGB OOO GBB RRR",
            "    YYY",
            "    WWW",
            "    WWW")]
        [InlineData(
            NotationRotationNames.MiddleM,
            NotationRotationType.Clockwise,
            "    WRW",
            "    WRW",
            "    WRW",
            "BBB OWO GGG RYR",
            "BBB OWO GGG RYR",
            "BBB OWO GGG RYR",
            "    YOY",
            "    YOY",
            "    YOY")]
        [InlineData(
            NotationRotationNames.MiddleM,
            NotationRotationType.CounterClockwise,
            "    WOW",
            "    WOW",
            "    WOW",
            "BBB OYO GGG RWR",
            "BBB OYO GGG RWR",
            "BBB OYO GGG RWR",
            "    YRY",
            "    YRY",
            "    YRY")]
        [InlineData(
            NotationRotationNames.MiddleM,
            NotationRotationType.Double,
            "    WYW",
            "    WYW",
            "    WYW",
            "BBB ORO GGG ROR",
            "BBB ORO GGG ROR",
            "BBB ORO GGG ROR",
            "    YWY",
            "    YWY",
            "    YWY")]
        [InlineData(
            NotationRotationNames.MiddleE,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "RRR BBB OOO GGG",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.MiddleE,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "OOO GGG RRR BBB",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.MiddleE,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "BBB OOO GGG RRR",
            "GGG RRR BBB OOO",
            "BBB OOO GGG RRR",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.MiddleS,
            NotationRotationType.Clockwise,
            "    WWW",
            "    BBB",
            "    WWW",
            "BYB OOO GWG RRR",
            "BYB OOO GWG RRR",
            "BYB OOO GWG RRR",
            "    YYY",
            "    GGG",
            "    YYY")]
        [InlineData(
            NotationRotationNames.MiddleS,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    GGG",
            "    WWW",
            "BWB OOO GYG RRR",
            "BWB OOO GYG RRR",
            "BWB OOO GYG RRR",
            "    YYY",
            "    BBB",
            "    YYY")]
        [InlineData(
            NotationRotationNames.MiddleS,
            NotationRotationType.Double,
            "    WWW",
            "    YYY",
            "    WWW",
            "BGB OOO GBG RRR",
            "BGB OOO GBG RRR",
            "BGB OOO GBG RRR",
            "    YYY",
            "    WWW",
            "    YYY")]
        [InlineData(
            NotationRotationNames.AllFrontUp,
            NotationRotationType.Clockwise,
            "    OOO",
            "    OOO",
            "    OOO",
            "BBB YYY GGG WWW",
            "BBB YYY GGG WWW",
            "BBB YYY GGG WWW",
            "    RRR",
            "    RRR",
            "    RRR")]
        [InlineData(
            NotationRotationNames.AllFrontUp,
            NotationRotationType.CounterClockwise,
            "    RRR",
            "    RRR",
            "    RRR",
            "BBB WWW GGG YYY",
            "BBB WWW GGG YYY",
            "BBB WWW GGG YYY",
            "    OOO",
            "    OOO",
            "    OOO")]
        [InlineData(
            NotationRotationNames.AllFrontUp,
            NotationRotationType.Double,
            "    YYY",
            "    YYY",
            "    YYY",
            "BBB RRR GGG OOO",
            "BBB RRR GGG OOO",
            "BBB RRR GGG OOO",
            "    WWW",
            "    WWW",
            "    WWW")]
        [InlineData(
            NotationRotationNames.AllFrontLeft,
            NotationRotationType.Clockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "OOO GGG RRR BBB",
            "OOO GGG RRR BBB",
            "OOO GGG RRR BBB",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.AllFrontLeft,
            NotationRotationType.CounterClockwise,
            "    WWW",
            "    WWW",
            "    WWW",
            "RRR BBB OOO GGG",
            "RRR BBB OOO GGG",
            "RRR BBB OOO GGG",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.AllFrontLeft,
            NotationRotationType.Double,
            "    WWW",
            "    WWW",
            "    WWW",
            "GGG RRR BBB OOO",
            "GGG RRR BBB OOO",
            "GGG RRR BBB OOO",
            "    YYY",
            "    YYY",
            "    YYY")]
        [InlineData(
            NotationRotationNames.AllFrontClockwise,
            NotationRotationType.Clockwise,
            "    BBB",
            "    BBB",
            "    BBB",
            "YYY OOO WWW RRR",
            "YYY OOO WWW RRR",
            "YYY OOO WWW RRR",
            "    GGG",
            "    GGG",
            "    GGG")]
        [InlineData(
            NotationRotationNames.AllFrontClockwise,
            NotationRotationType.CounterClockwise,
            "    GGG",
            "    GGG",
            "    GGG",
            "WWW OOO YYY RRR",
            "WWW OOO YYY RRR",
            "WWW OOO YYY RRR",
            "    BBB",
            "    BBB",
            "    BBB")]
        [InlineData(
            NotationRotationNames.AllFrontClockwise,
            NotationRotationType.Double,
            "    YYY",
            "    YYY",
            "    YYY",
            "GGG OOO BBB RRR",
            "GGG OOO BBB RRR",
            "GGG OOO BBB RRR",
            "    WWW",
            "    WWW",
            "    WWW")]
        public void Can_do_single_rotate(
            NotationRotationNames rotationName, NotationRotationType rotationType,
            string line0, string line1, string line2, string line3, string line4, string line5, string line6, string line7, string line8)
        {
            var puzzle = Rotator.ApplyMove(
                Solved,
                new NotationMoveType(rotationName, rotationType));

            PuzzleAssert.AssertSame(puzzle, line0, line1, line2, line3, line4, line5, line6, line7, line8);
        }

        [Theory]
        [InlineData(
            NotationRotationNames.Right,
            NotationRotationType.Clockwise,
            NotationRotationNames.Back,
            NotationRotationType.Clockwise,
            "    GGG",
            "    WWO",
            "    WWO",
            "OBB OOY GGR WWW",
            "WBB OOY GGY RRR",
            "WBB OOY GGY RRR",
            "    YYR",
            "    YYR",
            "    BBB")]
        public void Can_do_two_rotates(
            NotationRotationNames rotationName1, NotationRotationType rotationType1,
            NotationRotationNames rotationName2, NotationRotationType rotationType2,
            string line0, string line1, string line2, string line3, string line4, string line5, string line6, string line7, string line8)
        {
            var puzzle = Rotator.ApplyMoves(
                Solved,
                new[]
                {
                    new NotationMoveType(rotationName1, rotationType1),
                    new NotationMoveType(rotationName2, rotationType2)
                });

            PuzzleAssert.AssertSame(puzzle, line0, line1, line2, line3, line4, line5, line6, line7, line8);
        }

        // I changed the colors after writing the above tests so just use the old colors.
        private static Puzzle BuildOldSolvedPuzzle()
            => new Puzzle(
                new Face(FaceName.Up, PuzzleColor.White),
                new Face(FaceName.Front, PuzzleColor.Orange),
                new Face(FaceName.Down, PuzzleColor.Yellow),
                new Face(FaceName.Back, PuzzleColor.Red),
                new Face(FaceName.Left, PuzzleColor.Blue),
                new Face(FaceName.Right, PuzzleColor.Green));
    }
}