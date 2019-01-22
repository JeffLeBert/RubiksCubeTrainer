using System;
using System.Collections.Generic;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class NotationParser
    {
        public static IEnumerable<NotationMoveType> EnumerateMoves(string moves)
        {
            var move = NotationMoveType.None;
            foreach (var moveChar in moves)
            {
                if (char.IsWhiteSpace(moveChar))
                {
                    // Ignore whitespace.
                }
                else if (moveChar == '\'')
                {
                    if (move.Type != NotationRotationType.Clockwise)
                    {
                        throw new Exception("Invalid move notation.");
                    }

                    move = move.With(NotationRotationType.CounterClockwise);
                }
                else if (moveChar == '2')
                {
                    if (move.Type != NotationRotationType.Clockwise)
                    {
                        throw new Exception("Invalid move notation.");
                    }

                    move = move.With(NotationRotationType.Double);
                }
                else if (char.IsLetter(moveChar))
                {
                    if (!move.Empty)
                    {
                        yield return move;
                        move = NotationMoveType.None;
                    }

                    move = new NotationMoveType(ParseMoveName(moveChar));
                }
            }

            if (!move.Empty)
            {
                yield return move;
            }
        }

        private static NotationRotationNames ParseMoveName(char moveChar)
        {
            switch (moveChar)
            {
                case 'R': return NotationRotationNames.Right;
                case 'L': return NotationRotationNames.Left;
                case 'U': return NotationRotationNames.Up;
                case 'D': return NotationRotationNames.Down;
                case 'F': return NotationRotationNames.Front;
                case 'B': return NotationRotationNames.Back;
                case 'r': return NotationRotationNames.WideRight;
                case 'l': return NotationRotationNames.WideLeft;
                case 'u': return NotationRotationNames.WideUp;
                case 'd': return NotationRotationNames.WideDown;
                case 'f': return NotationRotationNames.WideFront;
                case 'b': return NotationRotationNames.WideBack;
                case 'E': return NotationRotationNames.MiddleE;
                case 'M': return NotationRotationNames.MiddleM;
                case 'S': return NotationRotationNames.MiddleS;
                case 'x': return NotationRotationNames.AllUp;
                case 'y': return NotationRotationNames.AllRight;
                case 'z': return NotationRotationNames.AllClockwise;

                default: throw new InvalidOperationException();
            }
        }
    }
}