using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Rotator
    {
        private static readonly ImmutableDictionary<NotationMoveType, IEnumerable<(Location, Location)>> allMoveLocations;

        static Rotator()
        {
            var builder = ImmutableDictionary.CreateBuilder<NotationMoveType, IEnumerable<(Location, Location)>>();

            // Faces...
            BuildRotations(builder, NotationRotationNames.Back, RotateYSlice);
            BuildRotations(builder, NotationRotationNames.Front, RotateYSlice);
            BuildRotations(builder, NotationRotationNames.Left, RotateXSlice);
            BuildRotations(builder, NotationRotationNames.Right, RotateXSlice);
            BuildRotations(builder, NotationRotationNames.Down, RotateZSlice);
            BuildRotations(builder, NotationRotationNames.Up, RotateZSlice);

            // Middle rotations.
            BuildRotations(builder, NotationRotationNames.MiddleE, RotateZSlice);
            BuildRotations(builder, NotationRotationNames.MiddleM, RotateXSlice);
            BuildRotations(builder, NotationRotationNames.MiddleS, RotateYSlice);

            // Wide rotations.
            BuildRotations(
                builder,
                NotationRotationNames.WideUp,
                move => RotateZSlice(move.With(NotationRotationNames.Up))
                    .Concat(RotateZSlice(move.With(NotationRotationNames.MiddleE).WithSwapDirection())));
            BuildRotations(
                builder,
                NotationRotationNames.WideDown,
                move => RotateZSlice(move.With(NotationRotationNames.MiddleE))
                    .Concat(RotateZSlice(move.With(NotationRotationNames.Down))));
            BuildRotations(
                builder,
                NotationRotationNames.WideLeft,
                move => RotateXSlice(move.With(NotationRotationNames.Left))
                    .Concat(RotateXSlice(move.With(NotationRotationNames.MiddleM))));
            BuildRotations(
                builder,
                NotationRotationNames.WideRight,
                move => RotateXSlice(move.With(NotationRotationNames.Right))
                    .Concat(RotateXSlice(move.With(NotationRotationNames.MiddleM).WithSwapDirection())));
            BuildRotations(
                builder,
                NotationRotationNames.WideBack,
                move => RotateYSlice(move.With(NotationRotationNames.Back))
                    .Concat(RotateYSlice(move.With(NotationRotationNames.MiddleS).WithSwapDirection())));
            BuildRotations(
                builder,
                NotationRotationNames.WideFront,
                move => RotateYSlice(move.With(NotationRotationNames.Front))
                    .Concat(RotateYSlice(move.With(NotationRotationNames.MiddleS))));

            // Full cube rotations.
            BuildRotations(
                builder,
                NotationRotationNames.AllY,
                move => RotateZSlice(move.With(NotationRotationNames.Up))
                    .Concat(RotateZSlice(move.With(NotationRotationNames.MiddleE).WithSwapDirection()))
                    .Concat(RotateZSlice(move.With(NotationRotationNames.Down).WithSwapDirection())));
            BuildRotations(
                builder,
                NotationRotationNames.AllZ,
                move => RotateYSlice(move.With(NotationRotationNames.Front))
                    .Concat(RotateYSlice(move.With(NotationRotationNames.MiddleS)))
                    .Concat(RotateYSlice(move.With(NotationRotationNames.Back).WithSwapDirection())));
            BuildRotations(
                builder,
                NotationRotationNames.AllX,
                move => RotateXSlice(move.With(NotationRotationNames.Right))
                    .Concat(RotateXSlice(move.With(NotationRotationNames.MiddleM).WithSwapDirection()))
                    .Concat(RotateXSlice(move.With(NotationRotationNames.Left).WithSwapDirection())));

            allMoveLocations = builder.ToImmutable();
        }

        public static Puzzle ApplyMoves(Puzzle puzzle, string moves)
            => ApplyMoves(puzzle, NotationParser.EnumerateMoves(moves));

        public static Puzzle ApplyMoves(Puzzle puzzle, ImmutableArray<ImmutableArray<NotationMoveType>> moveCollections)
        {
            foreach (var moveCollection in moveCollections)
            {
                puzzle = ApplyMoves(puzzle, moveCollection);
            }

            return puzzle;
        }

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
            var moveLocations = allMoveLocations[move];
            var newPuzzle = puzzle.Clone();
            foreach (var (fromLocation, toLocation) in moveLocations)
            {
                newPuzzle[toLocation] = puzzle[fromLocation];
            }

            return newPuzzle;
        }

        private static void BuildRotations(
            ImmutableDictionary<NotationMoveType, IEnumerable<(Location, Location)>>.Builder builder,
            NotationRotationNames rotationName,
            Func<NotationMoveType, IEnumerable<(Location, Location)>> rotator)
        {
            var clockwiseMoveType = new NotationMoveType(rotationName, NotationRotationType.Clockwise);
            builder.Add(clockwiseMoveType, rotator(clockwiseMoveType).ToImmutableArray());

            var counterClockwiseMoveType = new NotationMoveType(rotationName, NotationRotationType.CounterClockwise);
            builder.Add(counterClockwiseMoveType, rotator(counterClockwiseMoveType).ToImmutableArray());

            var doubleMoveType = new NotationMoveType(rotationName, NotationRotationType.Double);
            builder.Add(doubleMoveType, rotator(doubleMoveType).ToImmutableArray());
        }

        #region X Slice

        private static IEnumerable<(Location, Location)> RotateXSlice(NotationMoveType move)
        {
            var rotations = GetRotationForXSlice(move);
            var zLayer = GetLayerForXSlice(move);
            foreach (var (fromLocation, toLocation) in GetXSliceLocations(zLayer, rotations))
            {
                yield return (fromLocation, toLocation);

                switch (zLayer)
                {
                    case 1:
                        var rightFromLocation = new Location(FaceName.Right, fromLocation.Point3D);
                        var rightToLocation = new Location(FaceName.Right, toLocation.Point3D);
                        yield return (rightFromLocation, rightToLocation);
                        break;

                    case -1:
                        var leftFromLocation = new Location(FaceName.Left, fromLocation.Point3D);
                        var leftToLocation = new Location(FaceName.Left, toLocation.Point3D);
                        yield return (leftFromLocation, leftToLocation);
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

        private static IEnumerable<(Location, Location)> RotateYSlice(NotationMoveType move)
        {
            var rotations = GetRotationForYSlice(move);
            var zLayer = GetLayerForYSlice(move);
            foreach (var (fromLocation, toLocation) in GetYSliceLocations(zLayer, rotations))
            {
                yield return (fromLocation, toLocation);

                switch (zLayer)
                {
                    case 1:
                        var backFromLocation = new Location(FaceName.Back, fromLocation.Point3D);
                        var backToLocation = new Location(FaceName.Back, toLocation.Point3D);
                        yield return (backFromLocation, backToLocation);
                        break;

                    case -1:
                        var frontFromLocation = new Location(FaceName.Front, fromLocation.Point3D);
                        var frontToLocation = new Location(FaceName.Front, toLocation.Point3D);
                        yield return (frontFromLocation, frontToLocation);
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

        private static IEnumerable<(Location, Location)> RotateZSlice(NotationMoveType move)
        {
            var rotations = GetRotationForZSlice(move);
            var zLayer = GetLayerForZSlice(move);
            foreach (var (fromLocation, toLocation) in GetZSliceLocations(zLayer, rotations))
            {
                yield return (fromLocation, toLocation);

                switch (zLayer)
                {
                    case 1:
                        var upFromLocation = new Location(FaceName.Up, fromLocation.Point3D);
                        var upToLocation = new Location(FaceName.Up, toLocation.Point3D);
                        yield return (upFromLocation, upToLocation);
                        break;

                    case -1:
                        var downFromLocation = new Location(FaceName.Down, fromLocation.Point3D);
                        var downToLocation = new Location(FaceName.Down, toLocation.Point3D);
                        yield return (downFromLocation, downToLocation);
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