using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class When_building_a_face
    {
        [Fact]
        public void Can_build_solid_color_face()
        {
            var color = (PuzzleColor)9999;
            var face = new Face(FaceName.Up, color);

            Assert.Equal(color, face[-1, -1]);
            Assert.Equal(color, face[0, -1]);
            Assert.Equal(color, face[1, -1]);
            Assert.Equal(color, face[-1, 0]);
            Assert.Equal(color, face[0, 0]);
            Assert.Equal(color, face[1, 0]);
            Assert.Equal(color, face[-1, 1]);
            Assert.Equal(color, face[0, 1]);
            Assert.Equal(color, face[1, 1]);
        }

        [Fact]
        public void Can_build_random_color_face()
        {
            var face = new Face(
                FaceName.Up,
                (PuzzleColor)1,
                (PuzzleColor)2,
                (PuzzleColor)3,
                (PuzzleColor)4,
                (PuzzleColor)5,
                (PuzzleColor)6,
                (PuzzleColor)7,
                (PuzzleColor)8,
                (PuzzleColor)9);

            Assert.Equal((PuzzleColor)1, face[-1, -1]);
            Assert.Equal((PuzzleColor)2, face[0, -1]);
            Assert.Equal((PuzzleColor)3, face[1, -1]);
            Assert.Equal((PuzzleColor)4, face[-1, 0]);
            Assert.Equal((PuzzleColor)5, face[0, 0]);
            Assert.Equal((PuzzleColor)6, face[1, 0]);
            Assert.Equal((PuzzleColor)7, face[-1, 1]);
            Assert.Equal((PuzzleColor)8, face[0, 1]);
            Assert.Equal((PuzzleColor)9, face[1, 1]);
        }
    }

    public class When_rotating_a_face
    {
        [Fact]
        public void Indexer_works()
        {
            var face = Builder.BuildTestFace(FaceName.Up, 0);

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_clockwise()
        {
            var face = Builder.BuildTestFace(FaceName.Up, 0).RotateForward();

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
            var face = Builder.BuildTestFace(FaceName.Up, 0)
                .RotateForward()
                .RotateForward()
                .RotateForward()
                .RotateForward();

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_counter_clockwise()
        {
            var face = Builder.BuildTestFace(FaceName.Up, 0).RotateBackward();

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
            var face = Builder.BuildTestFace(FaceName.Up, 0)
                .RotateBackward()
                .RotateBackward()
                .RotateBackward()
                .RotateBackward();

            IsUnrotated(face);
        }

        [Fact]
        public void Can_rotate_twice()
        {
            var face = Builder.BuildTestFace(FaceName.Up, 0).Rotate2();

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
            var face = Builder.BuildTestFace(FaceName.Up, 0).Rotate2().Rotate2();

            IsUnrotated(face);
        }

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
}