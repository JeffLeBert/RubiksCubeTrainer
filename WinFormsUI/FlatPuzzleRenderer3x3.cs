using System.Drawing;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.WinFormsUI
{
    public class FlatPuzzleRenderer3x3 : FlatRendererBase
    {
        private static readonly Brush[] brushColors =
            new Brush[]
            {
                // NOTE: These must match the order in PuzzleColor enum.
                new SolidBrush(Color.White),
                new SolidBrush(Color.Blue),
                new SolidBrush(Color.Yellow),
                new SolidBrush(Color.Green),
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Orange)
            };

        private Graphics graphics;
        private SizeF squareSizeWithBorder;

        public FlatPuzzleRenderer3x3(Puzzle puzzle)
            : base(puzzle)
        {
        }

        public void Draw(Graphics graphics, SizeF availableSize)
        {
            this.graphics = graphics;
            this.squareSizeWithBorder = new SizeF(
                availableSize.Width / 4 / Face.Size,
                availableSize.Height / 3 / Face.Size);

            this.DrawFaces();
        }

        protected override void DrawFace(int faceX, int faceY, PuzzleColor[,] colors)
        {
            var startPoint = this.GetSquareStartPoint(faceX, faceY);

            var coloredSize = new SizeF(this.squareSizeWithBorder.Width - 1, this.squareSizeWithBorder.Height - 1);
            for (int x = 0; x < Face.Size; x++)
            {
                var xStart = startPoint.X + x * this.squareSizeWithBorder.Width;
                for (int y = 0; y < Face.Size; y++)
                {
                    var brush = brushColors[(int)colors[x, y]];
                    this.graphics.FillRectangle(
                        brush,
                        xStart,
                        startPoint.Y + y * this.squareSizeWithBorder.Height,
                        coloredSize.Width,
                        coloredSize.Height);
                }
            }
        }

        private PointF GetSquareStartPoint(int x, int y)
            => new PointF(
                x * this.squareSizeWithBorder.Width * Face.Size,
                y * this.squareSizeWithBorder.Height * Face.Size);
    }
}