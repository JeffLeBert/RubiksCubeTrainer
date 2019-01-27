using System;
using System.Collections.Generic;
using System.Linq;

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

        public static string FormatMoves(IEnumerable<NotationMoveType> moves)
            => string.Join(
                " ",
                from move in moves
                select FormatMove(move));

        public static string FormatMove(NotationMoveType move)
            => FormatMoveName(move.Name) + FormatRotationType(move.Type);

        private static string FormatMoveName(NotationRotationNames rotationName)
        {
            switch (rotationName)
            {
                case NotationRotationNames.Right: return "R";
                case NotationRotationNames.Left: return "L";
                case NotationRotationNames.Up: return "U";
                case NotationRotationNames.Down: return "D";
                case NotationRotationNames.Front: return "F";
                case NotationRotationNames.Back: return "B";
                case NotationRotationNames.WideRight: return "r";
                case NotationRotationNames.WideLeft: return "l";
                case NotationRotationNames.WideUp: return "u";
                case NotationRotationNames.WideDown: return "d";
                case NotationRotationNames.WideFront: return "f";
                case NotationRotationNames.WideBack: return "b";
                case NotationRotationNames.MiddleE: return "E";
                case NotationRotationNames.MiddleM: return "M";
                case NotationRotationNames.MiddleS: return "S";
                case NotationRotationNames.AllUp: return "x";
                case NotationRotationNames.AllClockwise: return "y";
                case NotationRotationNames.AllRight: return "z";

                default: throw new InvalidOperationException();
            }
        }

        private static string FormatRotationType(NotationRotationType rotationType)
        {
            switch (rotationType)
            {
                case NotationRotationType.Clockwise: return string.Empty;
                case NotationRotationType.CounterClockwise: return "'";
                case NotationRotationType.Double: return "2";

                default: throw new InvalidOperationException();
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
                case 'y': return NotationRotationNames.AllClockwise;
                case 'z': return NotationRotationNames.AllRight;

                default: throw new InvalidOperationException();
            }
        }
    }
}