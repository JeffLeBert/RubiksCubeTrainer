using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class CornerChecker : CheckerBase
    {
        public CornerChecker(params string[] parts)
        {
            var info = GetLocationInformation(parts[0].Trim());

            this.Location1 = info.Location;
            this.Location2 = info.Location.AdjacentCorner;
            this.Location3 = this.Location2.AdjacentCorner;
            this.Color1 = PuzzleColorParser.Parse(parts[1]);
            this.Color2 = PuzzleColorParser.Parse(parts[2]);
            this.Color3 = PuzzleColorParser.Parse(parts[3]);
            this.IsNot = info.IsNot;
            this.IsRotated = info.IsAll;
        }

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public PuzzleColor Color3 { get; }

        public bool IsNot { get; }

        public bool IsRotated { get; }

        public Location Location1 { get; }

        public Location Location2 { get; }

        public Location Location3 { get; }

        public override bool Matches(Puzzle puzzle)
            => this.MatchesWithoutIsNot(puzzle) != this.IsNot;

        private bool MatchesWithoutIsNot(Puzzle puzzle)
        {
            if (this.IsRotated)
            {
                var actualColor1 = puzzle[this.Location1];
                var actualColor2 = puzzle[this.Location2];
                var actualColor3 = puzzle[this.Location3];
                return ((actualColor1 == this.Color1) && (actualColor2 == this.Color2) && (actualColor3 == this.Color3))
                    || ((actualColor2 == this.Color1) && (actualColor3 == this.Color2) && (actualColor1 == this.Color3))
                    || ((actualColor3 == this.Color1) && (actualColor1 == this.Color2) && (actualColor2 == this.Color3));
            }
            else
            {
                return (puzzle[this.Location1] == this.Color1)
                    && (puzzle[this.Location2] == this.Color2) 
                    && (puzzle[this.Location3] == this.Color3);
            }
        }
    }
}