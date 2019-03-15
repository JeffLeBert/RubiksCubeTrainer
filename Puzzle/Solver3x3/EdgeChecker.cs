using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class EdgeChecker : CheckerBase
    {
        private EdgeChecker(Location location, Location location2, PuzzleColor color, PuzzleColor color2, bool isNot, bool isRotated)
            : base(location, isNot, isRotated)
        {
            this.Location2 = location2;
            this.Color = color;
            this.Color2 = color2;
        }

        public PuzzleColor Color { get; }

        public PuzzleColor Color2 { get; }

        public Location Location2 { get; }

        public static EdgeChecker Create(string location, string color1, string color2)
        {
            var info = GetLocationInformation(location.Trim());
            return new EdgeChecker(
                info.Location,
                info.Location.AdjacentEdge,
                PuzzleColorParser.Parse(color1),
                PuzzleColorParser.Parse(color2),
                info.IsNot,
                info.IsRotated);
        }

        public override string ToString()
            => $"{this.FormattedLocation} {this.Color.ToString()} {this.Color2.ToString()}";

        public override bool Matches(Puzzle puzzle)
            => this.MatchesWithoutIsNot(puzzle) != this.IsNot;

        public override IChecker WithColors(PuzzleColor[] colors)
            => new EdgeChecker(
                this.Location,
                this.Location2,
                UpdateColorIfTemplated(this.Color, colors),
                UpdateColorIfTemplated(this.Color2, colors),
                this.IsNot,
                this.IsRotated);

        public override IChecker Negate()
            => new EdgeChecker(
                this.Location,
                this.Location2,
                this.Color,
                this.Color2,
                !this.IsNot,
                this.IsRotated);

        private bool MatchesWithoutIsNot(Puzzle puzzle)
        {
            if (this.IsRotated)
            {
                var actualColor1 = puzzle[this.Location];
                var actualColor2 = puzzle[this.Location2];
                return ((actualColor1 == this.Color) && (actualColor2 == this.Color2))
                    || ((actualColor1 == this.Color2) && (actualColor2 == this.Color));
            }
            else
            {
                return (puzzle[this.Location] == this.Color) && (puzzle[this.Location2] == this.Color2);
            }
        }
    }
}