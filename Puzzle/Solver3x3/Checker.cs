using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class Checker
    {
        public static bool CornerUpFrontLeft(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.UpFrontLeft, Location.LeftFrontUp, Location.FrontLeftUp, color1, color2, color3);

        public static bool CornerUpFrontRight(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.UpFrontRight, Location.FrontRightUp, Location.RightFrontUp, color1, color2, color3);

        public static bool CornerUpBackLeft(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.UpBackLeft, Location.BackLeftUp, Location.LeftBackUp, color1, color2, color3);

        public static bool CornerUpBackRight(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.UpBackRight, Location.RightBackUp, Location.BackRightUp, color1, color2, color3);

        public static bool CornerDownFrontLeft(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.DownFrontLeft, Location.FrontLeftDown, Location.LeftFrontDown, color1, color2, color3);

        public static bool CornerDownFrontRight(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.DownFrontRight, Location.RightFrontDown, Location.FrontRightDown, color1, color2, color3);

        public static bool CornerDownBackLeft(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.DownBackLeft, Location.LeftBackDown, Location.BackLeftDown, color1, color2, color3);

        public static bool CornerDownBackRight(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2, PuzzleColor color3)
            => CornerOrRotated(puzzle, Location.DownBackRight, Location.BackRightDown, Location.RightBackDown, color1, color2, color3);

        private static bool CornerOrRotated(
            Puzzle puzzle,
            Location location1,
            Location location2,
            Location location3,
            PuzzleColor color1,
            PuzzleColor color2,
            PuzzleColor color3)
            => ((puzzle[location1] == color1) && (puzzle[location2] == color2) && (puzzle[location3] == color3))
            || ((puzzle[location2] == color1) && (puzzle[location3] == color2) && (puzzle[location1] == color3))
            || ((puzzle[location3] == color1) && (puzzle[location1] == color2) && (puzzle[location2] == color3));

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