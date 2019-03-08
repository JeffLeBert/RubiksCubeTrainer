using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SingleColorChecker : CheckerBase
    {
        public SingleColorChecker(string location, string color)
        {
            var info = GetLocationInformation(location.Trim());

            this.IsNot = info.IsNot;
            this.Location = info.Location;
            this.Color = PuzzleColorParser.Parse(color);
        }

        public PuzzleColor Color { get; }

        public bool IsNot { get; }

        public Location Location { get; }

        public override string ToString()
            => $"{this.Location.ToString()} {this.Color.ToString()}";

        public override bool Matches(Puzzle puzzle)
            => (puzzle[this.Location] == this.Color) != this.IsNot;
    }
}