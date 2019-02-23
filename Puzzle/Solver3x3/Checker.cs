using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class Checker
    {
        public static bool Corner(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
        {
            if (puzzle[location] != color1) return false;

            var location2 = location.AdjacentCorner;
            if (puzzle[location2] != color2) return false;

            var location3 = location2.AdjacentCorner;
            return puzzle[location3] == color3;
        }

        public static bool CornerOrRotated(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
        {
            var location2 = location.AdjacentCorner;
            var location3 = location2.AdjacentCorner;

            return ((puzzle[location] == color1) && (puzzle[location2] == color2) && (puzzle[location3] == color3))
                || ((puzzle[location2] == color1) && (puzzle[location3] == color2) && (puzzle[location] == color3))
                || ((puzzle[location3] == color1) && (puzzle[location] == color2) && (puzzle[location2] == color3));
        }

        public static bool Edge(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2)
            => Edge(puzzle, location, location.AdjacentEdge, color1, color2);

        public static bool EdgeOrFlipped(Puzzle puzzle, Location location, PuzzleColor color1, PuzzleColor color2)
            => EdgeOrFlipped(puzzle, location, location.AdjacentEdge, color1, color2);

        public static bool SingleColor(Puzzle puzzle, Location location, PuzzleColor color)
            => puzzle[location] == color;

        private static bool Edge(Puzzle puzzle, Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => (puzzle[location1] == color1) && (puzzle[location2] == color2);

        private static bool EdgeOrFlipped(Puzzle puzzle, Location location1, Location location2, PuzzleColor color1, PuzzleColor color2)
            => ((puzzle[location1] == color1) && (puzzle[location2] == color2))
                || ((puzzle[location1] == color2) && (puzzle[location2] == color1));
    }
}