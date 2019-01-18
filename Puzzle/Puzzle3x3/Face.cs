using System.Collections.Immutable;
using System.Linq;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class Face
    {
        private readonly ImmutableArray<PuzzleColor> squares;

        public const int Size = 3;

        public Face(PuzzleColor color)
        {
            this.squares = ImmutableArray<PuzzleColor>
                .Empty
                .AddRange(Enumerable.Repeat(color, 9));
        }

        public Face(ImmutableArray<PuzzleColor> squares)
        {
            this.squares = squares;
        }

        public PuzzleColor this[int index] => this.squares[index];

        public PuzzleColor this[int x, int y] => this.squares[GetIndex(x, y)];

        public Face With(int index, PuzzleColor color)
            => new Face(this.squares.SetItem(index, color));

        private static int GetIndex(int x, int y) => x + y * Size;
    }
}