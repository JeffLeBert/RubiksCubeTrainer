using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
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