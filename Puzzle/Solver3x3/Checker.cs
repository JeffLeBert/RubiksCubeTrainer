using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class Checker
    {
        public static bool Edge(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2)
            => Edge(puzzle, location, CoordinateMapper.GetAdjacentEdge(location), color1, color2);

        public static bool EdgeOrFlipped(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2)
            => EdgeOrFlipped(puzzle, location, CoordinateMapper.GetAdjacentEdge(location), color1, color2);

        public static bool SingleColor(Puzzle puzzle, Location location, PuzzleColor color)
            => puzzle[location] == color;

        private static bool Edge(Puzzle puzzle, Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => (puzzle[location1] == color1) && (puzzle[location2] == color2);

        private static bool EdgeOrFlipped(Puzzle puzzle, Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => ((puzzle[location1] == color1) && (puzzle[location2] == color2))
                || ((puzzle[location1] == color2) && (puzzle[location2] == color1));
    }
}