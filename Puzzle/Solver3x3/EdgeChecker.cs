using System;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class EdgeChecker : IChecker
    {
        private readonly Lazy<Location> lazyLocation2;

        public EdgeChecker(Location location, PuzzleColor color1, PuzzleColor color2)
        {
            this.Location1 = location;
            this.Color1 = color1;
            this.Color2 = color2;

            this.lazyLocation2 = new Lazy<Location>(() => CoordinateMapper.GetLocationForOtherEdgeFace(this.Location1));
        }

        public PuzzleColor Color1 { get; }
        
        public PuzzleColor Color2 { get; }

        public Location Location1 { get; }

        public Location Location2 => this.lazyLocation2.Value;

        public bool Check(Puzzle puzzle)
            => (puzzle[this.Location1] == this.Color1)
            && (puzzle[this.Location2] == this.Color2);
    }
}