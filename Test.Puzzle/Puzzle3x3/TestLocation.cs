using System.Collections.Generic;
using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class TestCoordinateMapper
    {
        [Theory]
        [MemberData(nameof(AllEdges))]
        public void The_adjcent_edge_of_an_adjacent_edge_is_the_original_edge(Location location)
        {
            var shouldBeFirstLocation = location.AdjacentEdge.AdjacentEdge;

            Assert.Equal(location, shouldBeFirstLocation);
        }

        public static IEnumerable<object[]> AllEdges()
            => new[]
            {
                new object[] { Location.UpBack },
                new object[] { Location.UpLeft },
                new object[] { Location.UpFront },
                new object[] { Location.UpRight },

                new object[] { Location.FrontUp },
                new object[] { Location.FrontLeft },
                new object[] { Location.FrontDown },
                new object[] { Location.FrontRight },

                new object[] { Location.DownBack },
                new object[] { Location.DownLeft },
                new object[] { Location.DownFront },
                new object[] { Location.DownRight },

                new object[] { Location.BackUp },
                new object[] { Location.BackLeft },
                new object[] { Location.BackDown },
                new object[] { Location.BackRight },

                new object[] { Location.LeftUp },
                new object[] { Location.LeftFront },
                new object[] { Location.LeftDown },
                new object[] { Location.LeftBack },

                new object[] { Location.RightUp },
                new object[] { Location.RightFront },
                new object[] { Location.RightDown },
                new object[] { Location.RightBack }
            };

        [Theory]
        [MemberData(nameof(AllCorners))]
        public void The_triple_adjacent_corner_is_the_starting_corner(Location location)
        {
            var shouldBeFirstLocation = location.AdjacentCorner.AdjacentCorner.AdjacentCorner;

            Assert.Equal(location, shouldBeFirstLocation);
        }

        public static IEnumerable<object[]> AllCorners()
        => new[]
        {
                new object[] { Location.UpLeftFront },
                new object[] { Location.UpRightFront },
                new object[] { Location.UpLeftBack },
                new object[] { Location.UpRightBack },
                new object[] { Location.FrontLeftDown },
                new object[] { Location.FrontRightDown },
                new object[] { Location.BackLeftDown },
                new object[] { Location.BackRightDown }
        };
    }
}