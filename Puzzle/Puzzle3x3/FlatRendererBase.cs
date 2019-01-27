namespace RubiksCubeTrainer.Puzzle3x3
{
    public abstract class FlatRendererBase
    {
        protected FlatRendererBase(Puzzle puzzle)
        {
            this.Puzzle = puzzle;
        }

        public Puzzle Puzzle { get; }

        protected void DrawFaces()
        {
            this.DrawFace(this.Puzzle[FaceName.Up], 1, 0, 1, -1);
            this.DrawFace(this.Puzzle[FaceName.Left], 0, 1, -1, -1);
            this.DrawFace(this.Puzzle[FaceName.Front], 1, 1, 1, -1);
            this.DrawFace(this.Puzzle[FaceName.Down], 1, 2, 1, 1);
            this.DrawFace(this.Puzzle[FaceName.Right], 2, 1, 1, -1);
            this.DrawFace(this.Puzzle[FaceName.Back], 3, 1, -1, -1);
        }

        private void DrawFace(Face face, int faceX, int faceY, int transformX, int transformY)
        {
            var colors = new PuzzleColor[Face.Size, Face.Size];
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    colors[x + 1, y + 1] = face[x * transformX, y * transformY];
                }
            }

            this.DrawFace(faceX, faceY, colors);
        }

        protected abstract void DrawFace(int faceX, int faceY, PuzzleColor[,] colors);
    }
}