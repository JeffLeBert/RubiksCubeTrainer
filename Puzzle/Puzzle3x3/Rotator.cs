using System;
using System.Collections.Generic;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Rotator
    {
        public static Puzzle ApplyMoves(Puzzle puzzle, string moves)
            => ApplyMoves(puzzle, NotationParser.EnumerateMoves(moves));

        public static Puzzle ApplyMoves(Puzzle puzzle, IEnumerable<NotationMoveType> moves)
        {
            foreach (var move in moves)
            {
                puzzle = ApplyMove(puzzle, move);
            }

            return puzzle;
        }

        public static Puzzle ApplyMove(Puzzle puzzle, NotationMoveType move)
        {
            switch (move.Name)
            {
                // Edge rotations.
                case NotationRotationNames.Back:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.Down:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.Front:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.Left:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.Right:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.Up:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(oldPuzzle, newPuzzle, move));

                // Middle rotations.
                case NotationRotationNames.MiddleE:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.MiddleM:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(oldPuzzle, newPuzzle, move));
                case NotationRotationNames.MiddleS:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(oldPuzzle, newPuzzle, move));

                // Wide rotations.
                case NotationRotationNames.WideUp:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Up)),
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleE).WithSwapDirection()));
                case NotationRotationNames.WideDown:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleE)),
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Down)));
                case NotationRotationNames.WideLeft:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Left)),
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleM)));
                case NotationRotationNames.WideRight:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Right)),
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleM).WithSwapDirection()));
                case NotationRotationNames.WideBack:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Back)),
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleS).WithSwapDirection()));
                case NotationRotationNames.WideFront:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Front)),
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleS)));

                // Full cube rotations.
                case NotationRotationNames.AllClockwise:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Up)),
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleE).WithSwapDirection()),
                        (oldPuzzle, newPuzzle) => RotateZSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Down).WithSwapDirection()));
                case NotationRotationNames.AllRight:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Front)),
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleS)),
                        (oldPuzzle, newPuzzle) => RotateYSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Back).WithSwapDirection()));
                case NotationRotationNames.AllUp:
                    return DoWithNewPuzzle(
                        puzzle,
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Right)),
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.MiddleM).WithSwapDirection()),
                        (oldPuzzle, newPuzzle) => RotateXSlice(puzzle, newPuzzle, move.With(NotationRotationNames.Left).WithSwapDirection()));

                default:
                    throw new InvalidOperationException();
            }
        }

        private static Puzzle DoWithNewPuzzle(Puzzle oldPuzzle, params Action<Puzzle, Puzzle>[] actions)
        {
            var newPuzzle = oldPuzzle.Clone();
            foreach (var action in actions)
            {
                action(oldPuzzle, newPuzzle);
            }

            return newPuzzle;
        }

        #region X Slice

        private static void RotateXSlice(Puzzle oldPuzzle, Puzzle newPuzzle, NotationMoveType move)
        {
            var rotations = GetRotationForXSlice(move);
            var zLayer = GetLayerForXSlice(move);
            foreach (var (fromLocation, toLocation) in GetXSliceLocations(zLayer, rotations))
            {
                newPuzzle[toLocation] = oldPuzzle[fromLocation];

                switch (zLayer)
                {
                    case 1:
                        var rightFromLocation = new Location(FaceName.Right, fromLocation.Point3D);
                        var rightToLocation = new Location(FaceName.Right, toLocation.Point3D);
                        newPuzzle[rightToLocation] = oldPuzzle[rightFromLocation];
                        break;

                    case -1:
                        var leftFromLocation = new Location(FaceName.Left, fromLocation.Point3D);
                        var leftToLocation = new Location(FaceName.Left, toLocation.Point3D);
                        newPuzzle[leftToLocation] = oldPuzzle[leftFromLocation];
                        break;
                }
            }
        }

        private static int GetRotationForXSlice(NotationMoveType move)
        {
            switch (move.Type)
            {
                case NotationRotationType.Clockwise: return move.Name != NotationRotationNames.Right ? 1 : -1;
                case NotationRotationType.CounterClockwise: return move.Name != NotationRotationNames.Right ? -1 : 1;
                case NotationRotationType.Double: return 2;

                default: throw new InvalidOperationException();
            }
        }

        private static int GetLayerForXSlice(NotationMoveType move)
        {
            switch (move.Name)
            {
                case NotationRotationNames.Right: return 1;
                case NotationRotationNames.MiddleM: return 0;
                case NotationRotationNames.Left: return -1;

                default: throw new InvalidOperationException();
            }
        }

        private static IEnumerable<(Location, Location)> GetXSliceLocations(int xLayer, int rotations)
        {
            var up = new[]
            {
                new Location(FaceName.Up, xLayer, -1, 1),
                new Location(FaceName.Up, xLayer, 0, 1),
                new Location(FaceName.Up, xLayer, 1, 1)
            };
            var back = new[]
            {
                new Location(FaceName.Back, xLayer, 1, 1),
                new Location(FaceName.Back, xLayer, 1, 0),
                new Location(FaceName.Back, xLayer, 1, -1)
            };
            var down = new[]
            {
                new Location(FaceName.Down, xLayer, 1, -1),
                new Location(FaceName.Down, xLayer, 0, -1),
                new Location(FaceName.Down, xLayer, -1, -1)
            };
            var front = new[]
            {
                new Location(FaceName.Front, xLayer, -1, -1),
                new Location(FaceName.Front, xLayer, -1, 0),
                new Location(FaceName.Front, xLayer, -1, 1)
            };

            switch (rotations)
            {
                case -1:
                    yield return (up[0], back[0]);
                    yield return (up[1], back[1]);
                    yield return (up[2], back[2]);

                    yield return (back[0], down[0]);
                    yield return (back[1], down[1]);
                    yield return (back[2], down[2]);

                    yield return (down[0], front[0]);
                    yield return (down[1], front[1]);
                    yield return (down[2], front[2]);

                    yield return (front[0], up[0]);
                    yield return (front[1], up[1]);
                    yield return (front[2], up[2]);
                    break;

                case 1:
                    yield return (up[0], front[0]);
                    yield return (up[1], front[1]);
                    yield return (up[2], front[2]);

                    yield return (front[0], down[0]);
                    yield return (front[1], down[1]);
                    yield return (front[2], down[2]);

                    yield return (down[0], back[0]);
                    yield return (down[1], back[1]);
                    yield return (down[2], back[2]);

                    yield return (back[0], up[0]);
                    yield return (back[1], up[1]);
                    yield return (back[2], up[2]);
                    break;

                case 2:
                    yield return (up[0], down[0]);
                    yield return (up[1], down[1]);
                    yield return (up[2], down[2]);

                    yield return (back[0], front[0]);
                    yield return (back[1], front[1]);
                    yield return (back[2], front[2]);

                    yield return (down[0], up[0]);
                    yield return (down[1], up[1]);
                    yield return (down[2], up[2]);

                    yield return (front[0], back[0]);
                    yield return (front[1], back[1]);
                    yield return (front[2], back[2]);
                    break;
            }
        }

        #endregion X Slice

        #region Y Slice

        private static void RotateYSlice(Puzzle oldPuzzle, Puzzle newPuzzle, NotationMoveType move)
        {
            var rotations = GetRotationForYSlice(move);
            var zLayer = GetLayerForYSlice(move);
            foreach (var (fromLocation, toLocation) in GetYSliceLocations(zLayer, rotations))
            {
                newPuzzle[toLocation] = oldPuzzle[fromLocation];

                switch (zLayer)
                {
                    case 1:
                        var backFromLocation = new Location(FaceName.Back, fromLocation.Point3D);
                        var backToLocation = new Location(FaceName.Back, toLocation.Point3D);
                        newPuzzle[backToLocation] = oldPuzzle[backFromLocation];
                        break;

                    case -1:
                        var frontFromLocation = new Location(FaceName.Front, fromLocation.Point3D);
                        var frontToLocation = new Location(FaceName.Front, toLocation.Point3D);
                        newPuzzle[frontToLocation] = oldPuzzle[frontFromLocation];
                        break;
                }
            }
        }

        private static int GetRotationForYSlice(NotationMoveType move)
        {
            switch (move.Type)
            {
                case NotationRotationType.Clockwise: return move.Name != NotationRotationNames.Back ? -1 : 1;
                case NotationRotationType.CounterClockwise: return move.Name != NotationRotationNames.Back ? 1 : -1;
                case NotationRotationType.Double: return 2;

                default: throw new InvalidOperationException();
            }
        }

        private static int GetLayerForYSlice(NotationMoveType move)
        {
            switch (move.Name)
            {
                case NotationRotationNames.Front: return -1;
                case NotationRotationNames.MiddleS: return 0;
                case NotationRotationNames.Back: return 1;

                default: throw new InvalidOperationException();
            }
        }

        private static IEnumerable<(Location, Location)> GetYSliceLocations(int yLayer, int rotations)
        {
            var up = new[]
            {
                new Location(FaceName.Up, -1, yLayer, 1),
                new Location(FaceName.Up, 0, yLayer, 1),
                new Location(FaceName.Up, 1, yLayer, 1)
            };
            var right = new[]
            {
                new Location(FaceName.Right, 1, yLayer, 1),
                new Location(FaceName.Right, 1, yLayer, 0),
                new Location(FaceName.Right, 1, yLayer, -1)
            };
            var down = new[]
            {
                new Location(FaceName.Down, 1, yLayer, -1),
                new Location(FaceName.Down, 0, yLayer, -1),
                new Location(FaceName.Down, -1, yLayer, -1)
            };
            var left = new[]
            {
                new Location(FaceName.Left, -1, yLayer, -1),
                new Location(FaceName.Left, -1, yLayer, 0),
                new Location(FaceName.Left, -1, yLayer, 1)
            };

            switch (rotations)
            {
                case -1:
                    yield return (up[0], right[0]);
                    yield return (up[1], right[1]);
                    yield return (up[2], right[2]);

                    yield return (right[0], down[0]);
                    yield return (right[1], down[1]);
                    yield return (right[2], down[2]);

                    yield return (down[0], left[0]);
                    yield return (down[1], left[1]);
                    yield return (down[2], left[2]);

                    yield return (left[0], up[0]);
                    yield return (left[1], up[1]);
                    yield return (left[2], up[2]);
                    break;

                case 1:
                    yield return (up[0], left[0]);
                    yield return (up[1], left[1]);
                    yield return (up[2], left[2]);

                    yield return (left[0], down[0]);
                    yield return (left[1], down[1]);
                    yield return (left[2], down[2]);

                    yield return (down[0], right[0]);
                    yield return (down[1], right[1]);
                    yield return (down[2], right[2]);

                    yield return (right[0], up[0]);
                    yield return (right[1], up[1]);
                    yield return (right[2], up[2]);
                    break;

                case 2:
                    yield return (up[0], down[0]);
                    yield return (up[1], down[1]);
                    yield return (up[2], down[2]);

                    yield return (right[0], left[0]);
                    yield return (right[1], left[1]);
                    yield return (right[2], left[2]);

                    yield return (down[0], up[0]);
                    yield return (down[1], up[1]);
                    yield return (down[2], up[2]);

                    yield return (left[0], right[0]);
                    yield return (left[1], right[1]);
                    yield return (left[2], right[2]);
                    break;
            }
        }

        #endregion Y Slice

        #region Z Slice

        private static void RotateZSlice(Puzzle oldPuzzle, Puzzle newPuzzle, NotationMoveType move)
        {
            var rotations = GetRotationForZSlice(move);
            var zLayer = GetLayerForZSlice(move);
            foreach (var (fromLocation, toLocation) in GetZSliceLocations(zLayer, rotations))
            {
                newPuzzle[toLocation] = oldPuzzle[fromLocation];

                switch (zLayer)
                {
                    case 1:
                        var upFromLocation = new Location(FaceName.Up, fromLocation.Point3D);
                        var upToLocation = new Location(FaceName.Up, toLocation.Point3D);
                        newPuzzle[upToLocation] = oldPuzzle[upFromLocation];
                        break;

                    case -1:
                        var downFromLocation = new Location(FaceName.Down, fromLocation.Point3D);
                        var downToLocation = new Location(FaceName.Down, toLocation.Point3D);
                        newPuzzle[downToLocation] = oldPuzzle[downFromLocation];
                        break;
                }
            }
        }

        private static int GetRotationForZSlice(NotationMoveType move)
        {
            switch (move.Type)
            {
                case NotationRotationType.Clockwise: return move.Name != NotationRotationNames.Up ? 1 : -1;
                case NotationRotationType.CounterClockwise: return move.Name != NotationRotationNames.Up ? -1 : 1;
                case NotationRotationType.Double: return 2;

                default: throw new InvalidOperationException();
            }
        }

        private static int GetLayerForZSlice(NotationMoveType move)
        {
            switch (move.Name)
            {
                case NotationRotationNames.Up: return 1;
                case NotationRotationNames.MiddleE: return 0;
                case NotationRotationNames.Down: return -1;

                default: throw new InvalidOperationException();
            }
        }

        private static IEnumerable<(Location, Location)> GetZSliceLocations(int zLayer, int rotations)
        {
            var front = new[]
            {
                new Location(FaceName.Front, -1, -1, zLayer),
                new Location(FaceName.Front, 0, -1, zLayer),
                new Location(FaceName.Front, 1, -1, zLayer)
            };
            var right = new[]
            {
                new Location(FaceName.Right, 1, -1, zLayer),
                new Location(FaceName.Right, 1, 0, zLayer),
                new Location(FaceName.Right, 1, 1, zLayer)
            };
            var back = new[]
            {
                new Location(FaceName.Back, 1, 1, zLayer),
                new Location(FaceName.Back, 0, 1, zLayer),
                new Location(FaceName.Back, -1, 1, zLayer)
            };
            var left = new[]
            {
                new Location(FaceName.Left, -1, 1, zLayer),
                new Location(FaceName.Left, -1, 0, zLayer),
                new Location(FaceName.Left, -1, -1, zLayer)
            };

            switch (rotations)
            {
                case -1:
                    yield return (front[0], left[0]);
                    yield return (front[1], left[1]);
                    yield return (front[2], left[2]);

                    yield return (left[0], back[0]);
                    yield return (left[1], back[1]);
                    yield return (left[2], back[2]);

                    yield return (back[0], right[0]);
                    yield return (back[1], right[1]);
                    yield return (back[2], right[2]);

                    yield return (right[0], front[0]);
                    yield return (right[1], front[1]);
                    yield return (right[2], front[2]);
                    break;

                case 1:
                    yield return (front[0], right[0]);
                    yield return (front[1], right[1]);
                    yield return (front[2], right[2]);

                    yield return (right[0], back[0]);
                    yield return (right[1], back[1]);
                    yield return (right[2], back[2]);

                    yield return (back[0], left[0]);
                    yield return (back[1], left[1]);
                    yield return (back[2], left[2]);

                    yield return (left[0], front[0]);
                    yield return (left[1], front[1]);
                    yield return (left[2], front[2]);
                    break;

                case 2:
                    yield return (front[0], back[0]);
                    yield return (front[1], back[1]);
                    yield return (front[2], back[2]);

                    yield return (right[0], left[0]);
                    yield return (right[1], left[1]);
                    yield return (right[2], left[2]);

                    yield return (back[0], front[0]);
                    yield return (back[1], front[1]);
                    yield return (back[2], front[2]);

                    yield return (left[0], right[0]);
                    yield return (left[1], right[1]);
                    yield return (left[2], right[2]);
                    break;
            }
        }

        #endregion Z Slice
    }
}