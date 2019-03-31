using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class ExpressionParser
    {
        public static bool IsExpression(string expression, bool allowNot)
            => allowNot
            ? expression.StartsWith("{") || expression.StartsWith("!{")
            : expression.StartsWith("{");

        public static (string Name, bool IsNot, PuzzleColor[] Colors) Parse(string expression)
        {
            if ((!expression.StartsWith("{") && !expression.StartsWith("!{"))
                || !expression.EndsWith("}"))
            {
                throw new InvalidOperationException($"Invalid expression: '{expression}'");
            }

            var isNot = expression.StartsWith("!");
            expression = isNot
                ? expression.Substring(2, expression.Length - 3)
                : expression.Substring(1, expression.Length - 2);

            var (name, colors) = ParseInner(expression.Trim());
            return (name, isNot, colors);
        }

        public static (string Name, PuzzleColor[] Colors) ParseInner(string expression)
            => Parse(expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        private static (string Name, PuzzleColor[] Colors) Parse(string[] parts)
            => (parts[0], ParseColors(parts.Skip(1)));

        private static PuzzleColor[] ParseColors(IEnumerable<string> parts)
            => (from color in parts
                select PuzzleColorParser.Parse(color))
                .ToArray();
    }
}