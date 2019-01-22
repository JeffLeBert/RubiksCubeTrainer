using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Rotator
    {
        private static readonly Point2D[] upEdgePoints = new[] { new Point2D(-1, -1), new Point2D(0, -1), new Point2D(1, -1) };
        private static readonly Point2D[] rightEdgePoints = new[] { new Point2D(-1, 1), new Point2D(-1, 0), new Point2D(-1, -1) };
        private static readonly Point2D[] downEdgePoints = new[] { new Point2D(1, 1), new Point2D(0, 1), new Point2D(-1, 1) };
        private static readonly Point2D[] leftEdgePoints = new[] { new Point2D(1, -1), new Point2D(1, 0), new Point2D(1, 1) };

        public static Puzzle ApplyMove(Puzzle puzzle, NotationMoveType move)
        {
            switch (move.Name)
            {
                case NotationRotationNames.AllClockwise:
                case NotationRotationNames.AllRight:
                case NotationRotationNames.AllUp:
                case NotationRotationNames.WideBack:
                case NotationRotationNames.WideDown:
                case NotationRotationNames.WideFront:
                case NotationRotationNames.WideLeft:
                case NotationRotationNames.WideRight:
                case NotationRotationNames.WideUp:
                    throw new NotImplementedException();

                case NotationRotationNames.MiddleE:
                case NotationRotationNames.MiddleM:
                case NotationRotationNames.MiddleS:
                    throw new NotImplementedException();

                case NotationRotationNames.Back: return RotateFace(puzzle, FaceName.Back, move.Type, true);
                case NotationRotationNames.Down: return RotateFace(puzzle, FaceName.Down, move.Type, true);
                case NotationRotationNames.Front: return RotateFace(puzzle, FaceName.Front, move.Type, false);
                case NotationRotationNames.Left: return RotateFace(puzzle, FaceName.Left, move.Type, true);
                case NotationRotationNames.Right: return RotateFace(puzzle, FaceName.Right, move.Type, false);
                case NotationRotationNames.Up: return RotateFace(puzzle, FaceName.Up, move.Type, false);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static Puzzle RotateFace(Puzzle puzzle, FaceName face, NotationRotationType type, bool switchDirection)
        {
            switch (type)
            {
                case NotationRotationType.Clockwise:
                    return switchDirection ? RotateBackwardFace(puzzle, face) : RotateForwardFace(puzzle, face);

                case NotationRotationType.CounterClockwise:
                    return switchDirection ? RotateForwardFace(puzzle, face) : RotateBackwardFace(puzzle, face);

                case NotationRotationType.Double:
                    return DoubleRotateFace(puzzle, face);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static Puzzle RotateForwardFace(Puzzle puzzle, FaceName faceName)
        {
            var oldFace = puzzle[faceName];
            return puzzle
                .With(oldFace.RotateForward())
                .With(RotateEdge(puzzle, oldFace, upEdgePoints, rightEdgePoints))
                .With(RotateEdge(puzzle, oldFace, rightEdgePoints, downEdgePoints))
                .With(RotateEdge(puzzle, oldFace, downEdgePoints, leftEdgePoints))
                .With(RotateEdge(puzzle, oldFace, leftEdgePoints, upEdgePoints));
        }

        private static Puzzle RotateBackwardFace(Puzzle puzzle, FaceName faceName)
        {
            var oldFace = puzzle[faceName];
            return puzzle
                .With(oldFace.RotateBackward())
                .With(RotateEdge(puzzle, oldFace, upEdgePoints, leftEdgePoints))
                .With(RotateEdge(puzzle, oldFace, leftEdgePoints, downEdgePoints))
                .With(RotateEdge(puzzle, oldFace, downEdgePoints, rightEdgePoints))
                .With(RotateEdge(puzzle, oldFace, rightEdgePoints, upEdgePoints));
        }

        private static Puzzle DoubleRotateFace(Puzzle puzzle, FaceName faceName)
        {
            var oldFace = puzzle[faceName];
            return puzzle
                .With(oldFace.Rotate2())
                .With(RotateEdge(puzzle, oldFace, upEdgePoints, downEdgePoints))
                .With(RotateEdge(puzzle, oldFace, leftEdgePoints, rightEdgePoints))
                .With(RotateEdge(puzzle, oldFace, downEdgePoints, upEdgePoints))
                .With(RotateEdge(puzzle, oldFace, rightEdgePoints, leftEdgePoints));
        }

        private static Face RotateEdge(
            Puzzle puzzle,
            Face frontFace,
            Point2D[] frontFromEdgePoints,
            Point2D[] frontToEdgePoints)
        {
            var fromFace = puzzle[frontFace.Mapper.GetAdjacentFaceForEdge(frontFromEdgePoints[1])];
            var toFace = puzzle[frontFace.Mapper.GetAdjacentFaceForEdge(frontToEdgePoints[1])];

            for (int i = 0; i < Face.Size; i++)
            {
                var fromCubie = frontFace.Mapper.Map(frontFromEdgePoints[i]);
                var toCubie = frontFace.Mapper.Map(frontToEdgePoints[i]);
                toFace = toFace.With(toCubie, fromFace[fromCubie]);
            }

            return toFace;
        }
    }
}