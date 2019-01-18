using System.Drawing;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.WinFormsUI
{
    public static class PuzzleRenderer3x3
    {
        private static readonly Brush[] brushColors =
            new Brush[]
            {
                new SolidBrush(Color.White),
                new SolidBrush(Color.Blue),
                new SolidBrush(Color.Yellow),
                new SolidBrush(Color.Green),
                new SolidBrush(Color.Red),
                new SolidBrush(Color.Orange)
            };

        public static void Draw(Graphics graphics, Puzzle puzzle, SizeF availableSize)
        {
            var squareSizeWithBorder = new SizeF(
                availableSize.Width / 4 / Face.Size,
                availableSize.Height / 3 / Face.Size);

            DrawFace(graphics, puzzle[FaceName.Up], 1, 0, squareSizeWithBorder);
            DrawFace(graphics, puzzle[FaceName.Left], 0, 1, squareSizeWithBorder);
            DrawFace(graphics, puzzle[FaceName.Front], 1, 1, squareSizeWithBorder);
            DrawFace(graphics, puzzle[FaceName.Down], 1, 2, squareSizeWithBorder);
            DrawFace(graphics, puzzle[FaceName.Right], 2, 1, squareSizeWithBorder);
            DrawFace(graphics, puzzle[FaceName.Back], 3, 1, squareSizeWithBorder);
        }

        private static void DrawFace(Graphics graphics, Face face, int faceX, int faceY, SizeF squareSizeWithBorder)
        {
            var startPoint = GetSquareStartPoint(faceX, faceY, squareSizeWithBorder);

            var coloredSize = new SizeF(squareSizeWithBorder.Width - 1, squareSizeWithBorder.Height - 1);
            for (int x = 0; x < Face.Size; x++)
            {
                var xStart = startPoint.X + x * squareSizeWithBorder.Width;
                for (int y = 0; y < Face.Size; y++)
                {
                    var brush = brushColors[(int)face[x, y]];
                    graphics.FillRectangle(
                        brush,
                        xStart,
                        startPoint.Y + y * squareSizeWithBorder.Height,
                        coloredSize.Width,
                        coloredSize.Height);
                }
            }
        }

        private static PointF GetSquareStartPoint(int x, int y, SizeF squareSizeWithBorder)
            => new PointF(
                x * squareSizeWithBorder.Width * Face.Size,
                y * squareSizeWithBorder.Height * Face.Size);
    }
}