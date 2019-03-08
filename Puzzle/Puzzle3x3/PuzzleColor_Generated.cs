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
			if ("White".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.White;
			if ("Blue".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.Blue;
			if ("Yellow".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.Yellow;
			if ("Green".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.Green;
			if ("Red".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.Red;
			if ("Orange".Equals(color, StringComparison.OrdinalIgnoreCase)) return PuzzleColor.Orange;

			throw new InvalidOperationException();
		}
	}
}