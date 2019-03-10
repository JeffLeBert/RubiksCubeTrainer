using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SingleColorChecker : CheckerBase
    {
        private SingleColorChecker(Location location, PuzzleColor color, bool isNot, bool isRotated)
            : base(location, isNot, isRotated)
        {
            this.Color = color;
        }

        public PuzzleColor Color { get; }

        public static SingleColorChecker Create(string location, string color1)
        {
            var info = GetLocationInformation(location.Trim());
            return new SingleColorChecker(
                info.Location,
                PuzzleColorParser.Parse(color1),
                info.IsNot,
                info.IsRotated);
        }

        public override string ToString()
            => $"{this.FormattedLocation} {this.Color.ToString()}";

        public override bool Matches(Puzzle puzzle)
            => (puzzle[this.Location] == this.Color) != this.IsNot;

        public override IChecker WithColors(PuzzleColor[] colors)
            => new SingleColorChecker(
                this.Location,
                UpdateColorIfTemplated(this.Color, colors),
                this.IsNot,
                this.IsRotated);
    }
}