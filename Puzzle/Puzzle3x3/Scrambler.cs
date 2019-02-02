using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Scrambler
    {
        public static string Scamble(int depth)
            => string.Join(" ", EnumerateMoveNames().Take(depth));

        private static IEnumerable<string> EnumerateMoveNames()
            => from move in EnumerateMoves()
               select NotationParser.FormatMove(move);

        internal static IEnumerable<NotationMoveType> EnumerateMoves()
        {
            var randomGenerator = new Random();
            var lastRotation = NotationRotationNames.None;
            while (true)
            {
                var nextMove = new NotationMoveType(
                    GetRandomRotationName(randomGenerator, lastRotation),
                    GetRandomRotationType(randomGenerator));
                lastRotation = nextMove.Name;

                yield return nextMove;
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