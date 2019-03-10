using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class CornerChecker : CheckerBase
    {
        private CornerChecker(
            Location location,
            Location location2,
            Location location3,
            PuzzleColor color,
            PuzzleColor color2,
            PuzzleColor color3,
            bool isNot,
            bool isRotated)
            : base(location, isNot, isRotated)
        {
            this.Location2 = location2;
            this.Location3 = location3;
            this.Color = color;
            this.Color2 = color2;
            this.Color3 = color3;
        }

        public PuzzleColor Color { get; }

        public PuzzleColor Color2 { get; }

        public PuzzleColor Color3 { get; }

        public Location Location2 { get; }

        public Location Location3 { get; }

        public static CornerChecker Create(string location, string color1, string color2, string color3)
        {
            var info = GetLocationInformation(location.Trim());
            var adjacentCorner = info.Location.AdjacentCorner;
            return new CornerChecker(
                info.Location,
                adjacentCorner,
                adjacentCorner.AdjacentCorner,
                PuzzleColorParser.Parse(color1),
                PuzzleColorParser.Parse(color2),
                PuzzleColorParser.Parse(color3),
                info.IsNot,
                info.IsRotated);
        }

        public override bool Matches(Puzzle puzzle)
            => this.MatchesWithoutIsNot(puzzle) != this.IsNot;

        public override string ToString()
            => $"{this.FormattedLocation} {this.Color.ToString()} {this.Color2.ToString()} {this.Color3.ToString()}";

        public override IChecker WithColors(PuzzleColor[] colors)
            => new CornerChecker(
                this.Location,
                this.Location2,
                this.Location3,
                UpdateColorIfTemplated(this.Color, colors),
                UpdateColorIfTemplated(this.Color2, colors),
                UpdateColorIfTemplated(this.Color3, colors),
                this.IsNot,
                this.IsRotated);

        private bool MatchesWithoutIsNot(Puzzle puzzle)
        {
            if (this.IsRotated)
            {
                var actualColor1 = puzzle[this.Location];
                var actualColor2 = puzzle[this.Location2];
                var actualColor3 = puzzle[this.Location3];
                return ((actualColor1 == this.Color) && (actualColor2 == this.Color2) && (actualColor3 == this.Color3))
                    || ((actualColor2 == this.Color) && (actualColor3 == this.Color2) && (actualColor1 == this.Color3))
                    || ((actualColor3 == this.Color) && (actualColor1 == this.Color2) && (actualColor2 == this.Color3));
            }
            else
            {
                return (puzzle[this.Location] == this.Color)
                    && (puzzle[this.Location2] == this.Color2) 
                    && (puzzle[this.Location3] == this.Color3);
            }
        }
    }
}