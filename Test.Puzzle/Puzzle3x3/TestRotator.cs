using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class TestRotator
    {
        [Fact]
        public void Can_R()
        {
            var puzzle = Rotator.ApplyMove(
                Builder.TestPuzzle,
                new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Clockwise));

            // Parts the didn't change.
            AssertSameAsTestPuzzle(
                puzzle,
                FaceName.Left,
                new[] { -1 },
                new[] { -1, 0, 1 },
                new[] { -1, 0, 1 });
            AssertSameAsTestPuzzle(
                puzzle,
                FaceName.Back,
                new[] { -1, 0 },
                new[] { 1 },
                new[] { -1, 0, 1 });
            AssertSameAsTestPuzzle(
                puzzle,
                FaceName.Up,
                new[] { -1, 0 },
                new[] { -1, 0, 1 },
                new[] { 1 });
            AssertSameAsTestPuzzle(
                puzzle,
                FaceName.Front,
                new[] { -1, 0 },
                new[] { -1 },
                new[] { -1, 0, 1 });
            AssertSameAsTestPuzzle(
                puzzle,
                FaceName.Down,
                new[] { -1, 0 },
                new[] { -1, 0, 1 },
                new[] { -1 });

            // TODO: Verify the face and adjacent edges.
        }

        private static void AssertSameAsTestPuzzle(
            Puzzle puzzle,
            FaceName faceName,
            int[] xValues,
            int[] yValues,
            int[] zValues)
        {
            foreach (var x in xValues)
            {
                foreach (var y in yValues)
                {
                    foreach (var z in zValues)
                    {
                        var location = new Location(faceName, new Point3D(x, y, z));
                        Assert.Equal(Builder.TestPuzzle[location], puzzle[location]);
                    }
                }
            }
        }
    }
}