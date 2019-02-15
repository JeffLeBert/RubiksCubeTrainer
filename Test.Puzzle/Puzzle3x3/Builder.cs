namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Builder
    {
        public static Face BuildTestFace(FaceName faceName, int offset)
            => new Face(
                faceName,
                new PuzzleColor[Face.Size, Face.Size]
                {
                    { (PuzzleColor)offset, (PuzzleColor)(offset + 3), (PuzzleColor)(offset + 6) },
                    { (PuzzleColor)(offset + 1), (PuzzleColor)(offset + 4), (PuzzleColor)(offset + 7) },
                    { (PuzzleColor)(offset + 2), (PuzzleColor)(offset + 5), (PuzzleColor)(offset + 8) }
                });
    }
}