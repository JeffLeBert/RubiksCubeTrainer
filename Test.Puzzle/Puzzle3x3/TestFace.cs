using System.Collections.Immutable;
using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class When_rotating_a_face
    {
        [Fact]
        public void Indexer_works()
        {
            var face = CreateTestFace(0);

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_clockwise()
        {
            var face = CreateTestFace(0).RotateForward();

            Assert.Equal((PuzzleColor)6, face[-1, -1]);
            Assert.Equal((PuzzleColor)3, face[0, -1]);
            Assert.Equal((PuzzleColor)0, face[1, -1]);
            Assert.Equal((PuzzleColor)7, face[-1, 0]);
            Assert.Equal((PuzzleColor)4, face[0, 0]);
            Assert.Equal((PuzzleColor)1, face[1, 0]);
            Assert.Equal((PuzzleColor)8, face[-1, 1]);
            Assert.Equal((PuzzleColor)5, face[0, 1]);
            Assert.Equal((PuzzleColor)2, face[1, 1]);
        }

        [Fact]
        public void Four_CW_rotates_is_back_to_start()
        {
            var face = CreateTestFace(0).RotateForward().RotateForward().RotateForward().RotateForward();

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_counter_clockwise()
        {
            var face = CreateTestFace(0).RotateBackward();

            Assert.Equal((PuzzleColor)2, face[-1, -1]);
            Assert.Equal((PuzzleColor)5, face[0, -1]);
            Assert.Equal((PuzzleColor)8, face[1, -1]);
            Assert.Equal((PuzzleColor)1, face[-1, 0]);
            Assert.Equal((PuzzleColor)4, face[0, 0]);
            Assert.Equal((PuzzleColor)7, face[1, 0]);
            Assert.Equal((PuzzleColor)0, face[-1, 1]);
            Assert.Equal((PuzzleColor)3, face[0, 1]);
            Assert.Equal((PuzzleColor)6, face[1, 1]);
        }

        [Fact]
        public void Four_CCW_rotates_is_back_to_start()
        {
            var face = CreateTestFace(0).RotateBackward().RotateBackward().RotateBackward().RotateBackward();

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_twice()
        {
            var face = CreateTestFace(0).Rotate2();

            Assert.Equal((PuzzleColor)8, face[-1, -1]);
            Assert.Equal((PuzzleColor)7, face[0, -1]);
            Assert.Equal((PuzzleColor)6, face[1, -1]);
            Assert.Equal((PuzzleColor)5, face[-1, 0]);
            Assert.Equal((PuzzleColor)4, face[0, 0]);
            Assert.Equal((PuzzleColor)3, face[1, 0]);
            Assert.Equal((PuzzleColor)2, face[-1, 1]);
            Assert.Equal((PuzzleColor)1, face[0, 1]);
            Assert.Equal((PuzzleColor)0, face[1, 1]);
        }

        [Fact]
        public void Two_double_rotates_is_back_to_start()
        {
            var face = CreateTestFace(0).Rotate2().Rotate2();

            IsUnrotated(face);
        }

        private static Face CreateTestFace(int offset)
            => new Face(
                FaceName.Up,
                ImmutableArray.Create(
                    (PuzzleColor)offset,
                    (PuzzleColor)(offset + 1),
                    (PuzzleColor)(offset + 2),
                    (PuzzleColor)(offset + 3),
                    (PuzzleColor)(offset + 4),
                    (PuzzleColor)(offset + 5),
                    (PuzzleColor)(offset + 6),
                    (PuzzleColor)(offset + 7),
                    (PuzzleColor)(offset + 8)));

        private static void IsUnrotated(Face face)
        {
            Assert.Equal((PuzzleColor)0, face[-1, -1]);
            Assert.Equal((PuzzleColor)1, face[0, -1]);
            Assert.Equal((PuzzleColor)2, face[1, -1]);
            Assert.Equal((PuzzleColor)3, face[-1, 0]);
            Assert.Equal((PuzzleColor)4, face[0, 0]);
            Assert.Equal((PuzzleColor)5, face[1, 0]);
            Assert.Equal((PuzzleColor)6, face[-1, 1]);
            Assert.Equal((PuzzleColor)7, face[0, 1]);
            Assert.Equal((PuzzleColor)8, face[1, 1]);
        }
    }

    public class When_working_with_center_points
    {
        [Theory]
        [InlineData(FaceName.Back, 0, 1, 0)]
        [InlineData(FaceName.Down, 0, 0, -1)]
        [InlineData(FaceName.Front, 0, -1, 0)]
        [InlineData(FaceName.Left, -1, 0, 0)]
        [InlineData(FaceName.Right, 1, 0, 0)]
        [InlineData(FaceName.Up, 0, 0, 1)]
        public void Can_get_center_point_from_face(FaceName faceName, int x, int y, int z)
        {
            var mapper = CoordinateMapper.GetMapperForFace(faceName);
            var centerPoint = mapper.CenterPoint;

            Assert.Equal(x, centerPoint.X);
            Assert.Equal(y, centerPoint.Y);
            Assert.Equal(z, centerPoint.Z);
        }

        [Theory]
        [InlineData(0, 1, 0, FaceName.Back)]
        [InlineData(0, 0, -1, FaceName.Down)]
        [InlineData(0, -1, 0, FaceName.Front)]
        [InlineData(-1, 0, 0, FaceName.Left)]
        [InlineData(1, 0, 0, FaceName.Right)]
        [InlineData(0, 0, 1, FaceName.Up)]
        public void Can_get_face_from_center_point(int x, int y, int z, FaceName expectedFaceName)
        {
            var actualFaceName = CoordinateMapper.GetFaceFromCenterPoint(new Point3D(x, y, z));

            Assert.Equal(expectedFaceName, actualFaceName);
        }
    }
}