using System;
using System.Collections.Generic;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class TextRenderer : FlatRendererBase
    {
        private readonly List<char[]> lines;

        public TextRenderer(Puzzle puzzle)
            : base(puzzle)
        {
            this.lines = new List<char[]>
            {
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' },
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' },
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' },
                new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' },
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' },
                new char[] { ' ', ' ', ' ', ' ', '?', '?', '?' }
            };
        }

        public string Draw()
        {
            this.DrawFaces();

            return string.Join(
                Environment.NewLine,
                new[]
                {
                    new string(this.lines[0]),
                    new string(this.lines[1]),
                    new string(this.lines[2]),
                    new string(this.lines[3]),
                    new string(this.lines[4]),
                    new string(this.lines[5]),
                    new string(this.lines[6]),
                    new string(this.lines[7]),
                    new string(this.lines[8])
                });
        }

        protected override void DrawFace(int faceX, int faceY, PuzzleColor[,] colors)
        {
            var startX = faceX * 4;
            var startY = faceY * 3;

            for (int y = 0; y < Face.Size; y++)
            {
                for (int x = 0; x < Face.Size; x++)
                {
                    this.lines[startY + y][startX + x] = GetColorCharacter(colors[x, y]);
                }
            }
        }

        private static char GetColorCharacter(PuzzleColor color)
        {
            switch (color)
            {
                case PuzzleColor.Blue: return 'B';
                case PuzzleColor.Green: return 'G';
                case PuzzleColor.Orange: return 'O';
                case PuzzleColor.Red: return 'R';
                case PuzzleColor.White: return 'W';
                case PuzzleColor.Yellow: return 'Y';

                default: throw new InvalidOperationException();
            }
        }
    }
}