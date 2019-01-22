using System;
using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class When_getting_a_face
    {
        [Fact]
        public void Throws_if_invalid_face()
            => Assert.Throws<IndexOutOfRangeException>(() => CoordinateMapper.GetMapperForFace((FaceName)9999));
    }

    public class When_getting_2D_coordinates_from_3D
    {
        [Theory]
        [InlineData(FaceName.Left)]
        [InlineData(FaceName.Right)]
        public void Mapping_X_face_works(FaceName faceName)
        {
            var mapper = CoordinateMapper.GetMapperForFace(faceName);
            var point = mapper.Map(new Point3D(1, 2, 3));

            Assert.Equal(2, point.X);
            Assert.Equal(3, point.Y);
        }

        [Theory]
        [InlineData(FaceName.Front)]
        [InlineData(FaceName.Back)]
        public void Mapping_Y_face_works(FaceName faceName)
        {
            var mapper = CoordinateMapper.GetMapperForFace(faceName);
            var point = mapper.Map(new Point3D(1, 2, 3));

            Assert.Equal(1, point.X);
            Assert.Equal(3, point.Y);
        }

        [Theory]
        [InlineData(FaceName.Up)]
        [InlineData(FaceName.Down)]
        public void Mapping_Z_face_works(FaceName faceName)
        {
            var mapper = CoordinateMapper.GetMapperForFace(faceName);
            var point = mapper.Map(new Point3D(1, 2, 3));

            Assert.Equal(1, point.X);
            Assert.Equal(2, point.Y);
        }
    }

    public class When_getting_3D_coordinates_from_2D
    {
        [Fact]
        public void Mapping_left_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Left);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(-1, point.X);
            Assert.Equal(100, point.Y);
            Assert.Equal(101, point.Z);
        }

        [Fact]
        public void Mapping_right_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Right);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(1, point.X);
            Assert.Equal(100, point.Y);
            Assert.Equal(101, point.Z);
        }

        [Fact]
        public void Mapping_front_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Front);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(100, point.X);
            Assert.Equal(-1, point.Y);
            Assert.Equal(101, point.Z);
        }

        [Fact]
        public void Mapping_back_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Back);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(100, point.X);
            Assert.Equal(1, point.Y);
            Assert.Equal(101, point.Z);
        }

        [Fact]
        public void Mapping_up_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Up);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(100, point.X);
            Assert.Equal(101, point.Y);
            Assert.Equal(1, point.Z);
        }

        [Fact]
        public void Mapping_down_face_works()
        {
            var mapper = CoordinateMapper.GetMapperForFace(FaceName.Down);
            var point = mapper.Map(new Point2D(100, 101));

            Assert.Equal(100, point.X);
            Assert.Equal(101, point.Y);
            Assert.Equal(-1, point.Z);
        }
    }

    public class When_getting_relative_faces
    {
        [Theory]
        // "Up" faces.
        [InlineData(FaceName.Up, 0, -1, FaceName.Front)]
        [InlineData(FaceName.Down, 0, -1, FaceName.Front)]
        [InlineData(FaceName.Back, 0, -1, FaceName.Down)]
        [InlineData(FaceName.Front, 0, -1, FaceName.Down)]
        [InlineData(FaceName.Left, 0, -1, FaceName.Down)]
        [InlineData(FaceName.Right, 0, -1, FaceName.Down)]
        // "Down" faces.
        [InlineData(FaceName.Up, 0, 1, FaceName.Back)]
        [InlineData(FaceName.Down, 0, 1, FaceName.Back)]
        [InlineData(FaceName.Back, 0, 1, FaceName.Up)]
        [InlineData(FaceName.Front, 0, 1, FaceName.Up)]
        [InlineData(FaceName.Left, 0, 1, FaceName.Up)]
        [InlineData(FaceName.Right, 0, 1, FaceName.Up)]
        // "Right" faces.
        [InlineData(FaceName.Up, 1, 0, FaceName.Right)]
        [InlineData(FaceName.Down, 1, 0, FaceName.Right)]
        [InlineData(FaceName.Back, 1, 0, FaceName.Right)]
        [InlineData(FaceName.Front, 1, 0, FaceName.Right)]
        [InlineData(FaceName.Left, 1, 0, FaceName.Back)]
        [InlineData(FaceName.Right, 1, 0, FaceName.Back)]
        // "Left" faces.
        [InlineData(FaceName.Up, -1, 0, FaceName.Left)]
        [InlineData(FaceName.Down, -1, 0, FaceName.Left)]
        [InlineData(FaceName.Back, -1, 0, FaceName.Left)]
        [InlineData(FaceName.Front, -1, 0, FaceName.Left)]
        [InlineData(FaceName.Left, -1, 0, FaceName.Front)]
        [InlineData(FaceName.Right, -1, 0, FaceName.Front)]
        public void Can_get_adjacent_face(FaceName startFace, int x, int y, FaceName expectedUpFace)
        {
            var mapper = CoordinateMapper.GetMapperForFace(startFace);
            var actualUpFace = mapper.GetAdjacentFaceForEdge(new Point2D(x, y));

            Assert.Equal(expectedUpFace, actualUpFace);
        }
    }
}