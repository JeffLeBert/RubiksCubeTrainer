using System;
using System.Collections.Generic;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Scrambler
    {
        public static string Scamble(int depth)
            => string.Join(" ", EnumerateMoves(depth));

        private static IEnumerable<string> EnumerateMoves(int depth)
        {
            var randomGenerator = new Random();
            var lastRotation = NotationRotationNames.None;
            for (int i = 0; i < depth; i++)
            {
                var nextMove = new NotationMoveType(
                    GetRandomRotationName(randomGenerator, lastRotation),
                    GetRandomRotationType(randomGenerator));
                lastRotation = nextMove.Name;

                yield return NotationParser.FormatMove(nextMove);
            }
        }

        private static NotationRotationNames GetRandomRotationName(Random randomGenerator, NotationRotationNames lastRotation)
        {
            while (true)
            {
                var rotationName = randomGenerator.Next(12) + NotationRotationNames.Right;
                if (rotationName != lastRotation)
                {
                    return rotationName;
                }
            }
        }

        private static NotationRotationType GetRandomRotationType(Random randomGenerator)
        {
            switch (randomGenerator.Next(3))
            {
                case 0: return NotationRotationType.Clockwise;
                case 1: return NotationRotationType.CounterClockwise;
                case 2: return NotationRotationType.Double;
                default: throw new InvalidOperationException();
            }
        }
    }
}