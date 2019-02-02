using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SingleColorChecker : CheckerBase
    {
        public SingleColorChecker(Location location, PuzzleColor color)
        {
            this.Location = location;
            this.Color = color;
        }

        public PuzzleColor Color { get; }

        public Location Location { get; }

        public override bool Check(Puzzle puzzle)
            => puzzle[this.Location] == this.Color;
    }
}