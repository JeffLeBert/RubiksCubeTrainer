using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class ExpressionParser
    {
        public static (string Name, PuzzleColor[] Colors) Parse(string expression)
            => Parse(expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        private static (string Name, PuzzleColor[] Colors) Parse(string[] parts)
            => (parts[0], ParseColors(parts.Skip(1)));

        private static PuzzleColor[] ParseColors(IEnumerable<string> parts)
            => (from color in parts
                select PuzzleColorParser.Parse(color))
                .ToArray();
    }
}