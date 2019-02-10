using System;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class Checker
    {
        public static Func<Puzzle, bool> Edge(Location location, PuzzleColor color1, PuzzleColor color2)
            => Edge(location, CoordinateMapper.GetLocationForOtherEdgeFace(location), color1, color2);

        public static Func<Puzzle, bool> EdgeOrFlipped(Location location, PuzzleColor color1, PuzzleColor color2)
            => EdgeOrFlipped(location, CoordinateMapper.GetLocationForOtherEdgeFace(location), color1, color2);

        public static Func<Puzzle, bool> SingleColor(Location location, PuzzleColor color)
            => puzzle => puzzle[location] == color;

        private static Func<Puzzle, bool> Edge(Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => puzzle => (puzzle[location1] == color1) && (puzzle[location2] == color2);

        private static Func<Puzzle, bool> EdgeOrFlipped(Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => puzzle => ((puzzle[location1] == color1) && (puzzle[location2] == color2))
                || ((puzzle[location1] == color2) && (puzzle[location2] == color1));
    }
}