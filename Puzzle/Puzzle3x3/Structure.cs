using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public static class Structure
    {
        // Face indexes in puzzle, see LayerName enum.
        //   0
        // 4 1 5 3
        //   2

        // Indexes of cells in each face.
        //        0 1 2
        //        3 4 5
        //        6 7 8
        //
        // 0 1 2  0 1 2  0 1 2  0 1 2
        // 3 4 5  3 4 5  3 4 5  3 4 5
        // 6 7 8  6 7 8  6 7 8  6 7 8
        //
        //        0 1 2
        //        3 4 5
        //        6 7 8

        public static ImmutableDictionary<LayerName, ImmutableDictionary<SquareLocation, SquareLocation>> ClockwiseRotates { get; }

        static Structure()
        {
            ClockwiseRotates = ImmutableDictionary<LayerName, ImmutableDictionary<SquareLocation, SquareLocation>>.Empty.AddRange(
                new[]
                {
                    BuildLayerRotation(LayerName.Up, ClockwiseRotateFaceUp()),
                    BuildLayerRotation(LayerName.Front, ClockwiseRotateFaceFront()),
                    BuildLayerRotation(LayerName.Down, ClockwiseRotateFaceDown()),
                    BuildLayerRotation(LayerName.Back, ClockwiseRotateFaceBack()),
                    BuildLayerRotation(LayerName.Left, ClockwiseRotateFaceLeft()),
                    BuildLayerRotation(LayerName.Right, ClockwiseRotateFaceRight()),
                    BuildLayerRotation(LayerName.MiddleE, ClockwiseRotateLayerE()),
                    BuildLayerRotation(LayerName.MiddleM, ClockwiseRotateLayerM()),
                    BuildLayerRotation(LayerName.MiddleS, ClockwiseRotateLayerS())
                });
        }

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceUp()
            => BuildDictionary(
                BuildStandardFaceRotation(FaceName.Up),
                BuildEdgeRotation(FaceName.Back, new[] { 6, 7, 8 }, FaceName.Right, new[] { 0, 3, 6 }),
                BuildEdgeRotation(FaceName.Right, new[] { 0, 3, 6 }, FaceName.Front, new[] { 0, 1, 2 }),
                BuildEdgeRotation(FaceName.Front, new[] { 0, 1, 2 }, FaceName.Left, new[] { 0, 1, 2 }),
                BuildEdgeRotation(FaceName.Left, new[] { 0, 1, 2 }, FaceName.Back, new[] { 6, 7, 8 }));

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceFront()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceDown()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceBack()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceLeft()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateFaceRight()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateLayerE()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateLayerM()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static ImmutableDictionary<SquareLocation, SquareLocation> ClockwiseRotateLayerS()
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty;

        private static IEnumerable<KeyValuePair<SquareLocation, SquareLocation>> BuildStandardFaceRotation(FaceName face)
        {
            yield return BuildLocation(face, 0, 2);
            yield return BuildLocation(face, 1, 5);
            yield return BuildLocation(face, 2, 8);
            yield return BuildLocation(face, 3, 1);
            yield return BuildLocation(face, 5, 7);
            yield return BuildLocation(face, 6, 0);
            yield return BuildLocation(face, 7, 3);
            yield return BuildLocation(face, 8, 6);
        }

        private static IEnumerable<KeyValuePair<SquareLocation, SquareLocation>> BuildEdgeRotation(
            FaceName face1,
            int[] indexes1,
            FaceName face2,
            int[] indexes2)
        {
            for (int i = 0; i < Face.Size; i++)
            {
                yield return BuildLocation(face1, indexes1[i], face2, indexes2[i]);
            }
        }

        private static KeyValuePair<LayerName, ImmutableDictionary<SquareLocation, SquareLocation>> BuildLayerRotation(
            LayerName layer,
            ImmutableDictionary<SquareLocation, SquareLocation> map)
            => new KeyValuePair<LayerName, ImmutableDictionary<SquareLocation, SquareLocation>>(layer, map);

        private static KeyValuePair<SquareLocation, SquareLocation> BuildLocation(FaceName face, int index1, int index2)
            => new KeyValuePair<SquareLocation, SquareLocation>(
                new SquareLocation(face, index1),
                new SquareLocation(face, index2));

        private static KeyValuePair<SquareLocation, SquareLocation> BuildLocation(
            FaceName face1,
            int index1,
            FaceName face2,
            int index2)
            => new KeyValuePair<SquareLocation, SquareLocation>(
                new SquareLocation(face1, index1),
                new SquareLocation(face2, index2));

        private static ImmutableDictionary<SquareLocation, SquareLocation> BuildDictionary(
            params IEnumerable<KeyValuePair<SquareLocation, SquareLocation>>[] enumerators)
            => ImmutableDictionary<SquareLocation, SquareLocation>.Empty
                .AddRange(
                    from enumerator in enumerators
                    from keyValue in enumerator
                    select keyValue);
    }
}