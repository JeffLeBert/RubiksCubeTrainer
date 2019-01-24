namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Builder
    {
        public static Face BuildTestFace(FaceName faceName, int offset)
            => new Face(
                faceName,
                new[]
                {
                    (PuzzleColor)offset,
                    (PuzzleColor)(offset + 1),
                    (PuzzleColor)(offset + 2),
                    (PuzzleColor)(offset + 3),
                    (PuzzleColor)(offset + 4),
                    (PuzzleColor)(offset + 5),
                    (PuzzleColor)(offset + 6),
                    (PuzzleColor)(offset + 7),
                    (PuzzleColor)(offset + 8)
                });

        public static Puzzle TestPuzzle
            = new Puzzle(
                BuildTestFace(FaceName.Up, 0),
                BuildTestFace(FaceName.Front, 100),
                BuildTestFace(FaceName.Down, 200),
                BuildTestFace(FaceName.Back, 300),
                BuildTestFace(FaceName.Left, 400),
                BuildTestFace(FaceName.Right, 500));
    }
}