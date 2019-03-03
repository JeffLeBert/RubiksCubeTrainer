using System;
using System.Collections.Generic;
using System.Linq;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static partial class NotationParser
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
                    if (!move.IsEmpty)
                    {
                        yield return move;
                        move = NotationMoveType.None;
                    }

                    move = new NotationMoveType(ParseMoveName(moveChar));
                }
            }

            if (!move.IsEmpty)
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
    }
}