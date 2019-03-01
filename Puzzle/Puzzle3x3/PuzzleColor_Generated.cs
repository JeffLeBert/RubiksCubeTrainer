using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public enum PuzzleColor
    {
		White,
		Blue,
		Yellow,
		Green,
		Red,
		Orange,
    }

    public static class PuzzleColorParser
    {
        public static PuzzleColor Parse(string color)
        {
			switch (color)
			{
				case nameof(PuzzleColor.White):
					return PuzzleColor.White;
				case nameof(PuzzleColor.Blue):
					return PuzzleColor.Blue;
				case nameof(PuzzleColor.Yellow):
					return PuzzleColor.Yellow;
				case nameof(PuzzleColor.Green):
					return PuzzleColor.Green;
				case nameof(PuzzleColor.Red):
					return PuzzleColor.Red;
				case nameof(PuzzleColor.Orange):
					return PuzzleColor.Orange;
				default:
					throw new InvalidOperationException();
			}
		}
	}
}