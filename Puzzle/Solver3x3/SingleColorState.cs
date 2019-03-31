using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SingleColorState : StateBase
    {
        private SingleColorState(Location location, PuzzleColor color, bool isNot, bool isRotated)
            : base(location, isNot, isRotated)
        {
            this.Color = color;
        }

        public PuzzleColor Color { get; }

        public static SingleColorState Create(string location, string color1)
        {
            var info = GetLocationInformation(location.Trim());
            return new SingleColorState(
                info.Location,
                PuzzleColorParser.Parse(color1),
                info.IsNot,
                info.IsRotated);
        }

        public override string ToString()
            => $"{this.FormattedLocation} {this.Color.ToString()}";

        public override bool Matches(Puzzle puzzle)
            => (puzzle[this.Location] == this.Color) != this.IsNot;

        public override IState WithColors(PuzzleColor[] colors)
            => (colors == null) || (colors.Length == 0)
            ? this
            : new SingleColorState(
                this.Location,
                UpdateColorIfTemplated(this.Color, colors),
                this.IsNot,
                this.IsRotated);

        public override IState Negate()
            => new SingleColorState(
                this.Location,
                this.Color,
                !this.IsNot,
                this.IsRotated);
    }
}