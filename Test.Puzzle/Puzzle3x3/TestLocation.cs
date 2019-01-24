using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class TestLocation
    {
        [Theory]
        [InlineData(FaceName.Up, 0, 0, 1)]
        [InlineData(FaceName.Down, 0, 0, -1)]
        [InlineData(FaceName.Back, 0, 1, 0)]
        [InlineData(FaceName.Front, 0, -1, 0)]
        [InlineData(FaceName.Left, -1, 0, 0)]
        [InlineData(FaceName.Right, 1, 0, 0)]
        public void Can_determine_if_center_point(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.True(location.IsCenter);
        }

        [Theory]
        [InlineData(FaceName.Up, -1, -1, 1)]        // Corner
        [InlineData(FaceName.Up, -1, 0, 1)]         // Edge
        public void Can_determine_if_not_center_point(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.False(location.IsCenter);
        }

        [Theory]
        [InlineData(FaceName.Up, -1, 0, 1)]
        [InlineData(FaceName.Up, 1, 0, 1)]
        [InlineData(FaceName.Up, 0, -1, 1)]
        [InlineData(FaceName.Up, 0, 1, 1)]
        [InlineData(FaceName.Right, 1, 0, -1)]
        [InlineData(FaceName.Right, 1, 0, 1)]
        [InlineData(FaceName.Right, 1, -1, 0)]
        [InlineData(FaceName.Right, 1, 1, 0)]
        public void Can_determine_if_edge(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.True(location.IsEdge);
        }

        [Theory]
        [InlineData(FaceName.Up, -1, -1, 1)]
        [InlineData(FaceName.Up, -1, 1, 1)]
        [InlineData(FaceName.Up, 1, -1, 1)]
        [InlineData(FaceName.Up, 1, 1, 1)]
        [InlineData(FaceName.Right, 1, -1, -1)]
        [InlineData(FaceName.Right, 1, -1, 1)]
        [InlineData(FaceName.Right, 1, 1, -1)]
        [InlineData(FaceName.Right, 1, 1, 1)]
        public void Can_determine_if_not_edge(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.False(location.IsEdge);
        }

        [Theory]
        [InlineData(FaceName.Up, -1, -1, 1)]
        [InlineData(FaceName.Up, -1, 1, 1)]
        [InlineData(FaceName.Up, 1, -1, 1)]
        [InlineData(FaceName.Up, 1, 1, 1)]
        [InlineData(FaceName.Right, 1, -1, -1)]
        [InlineData(FaceName.Right, 1, -1, 1)]
        [InlineData(FaceName.Right, 1, 1, -1)]
        [InlineData(FaceName.Right, 1, 1, 1)]
        public void Can_determine_corner(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.True(location.IsCorner);
        }

        [Theory]
        [InlineData(FaceName.Up, -1, 0, 1)]
        [InlineData(FaceName.Up, 1, 0, 1)]
        [InlineData(FaceName.Up, 0, -1, 1)]
        [InlineData(FaceName.Up, 0, 1, 1)]
        [InlineData(FaceName.Right, 1, 0, -1)]
        [InlineData(FaceName.Right, 1, 0, 1)]
        [InlineData(FaceName.Right, 1, -1, 0)]
        [InlineData(FaceName.Right, 1, 1, 0)]
        public void Can_determine_is_not_corner(FaceName faceName, int x, int y, int z)
        {
            var location = new Location(faceName, x, y, z);

            Assert.False(location.IsCorner);
        }
    }
}