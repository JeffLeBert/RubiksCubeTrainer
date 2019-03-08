using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SingleColorChecker : CheckerBase
    {
        public SingleColorChecker(params string[] parts)
        {
            var info = GetLocationInformation(parts[0].Trim());

            this.IsNot = info.IsNot;
            this.Location = info.Location;
            this.Color = PuzzleColorParser.Parse(parts[1]);
        }

        public PuzzleColor Color { get; }

        public bool IsNot { get; }

        public Location Location { get; }

        public override bool Matches(Puzzle puzzle)
            => (puzzle[this.Location] == this.Color) != this.IsNot;
    }
}