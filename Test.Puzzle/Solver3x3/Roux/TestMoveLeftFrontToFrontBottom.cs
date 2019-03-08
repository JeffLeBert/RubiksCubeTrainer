using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;
using Xunit;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class TestMoveLeftFrontToFrontBottom
    {
        private static readonly Step step = WellKnownSolvers.Roux.Steps["MoveLeftFrontToFrontBottom"];

        [Theory]
        [MemberData(nameof(GoodInitialStates))]
        public void True_if_InitialState_is_met(Location location, PuzzleColor color)
        {
            var puzzle = Puzzle.Solved.Clone();
            puzzle[location] = color;

            Assert.True(step.InitialState.Matches(puzzle));
        }

        public static IEnumerable<object[]> GoodInitialStates()
            => new[]
            {
                new object[] { Location.LeftFront , PuzzleColor.Green },
                new object[] { Location.FrontLeft, PuzzleColor.Green }
            };

        [Theory]
        [MemberData(nameof(BadInitialStates))]
        public void False_if_InitialState_is_not_met(Location location, PuzzleColor color)
        {
            var puzzle = Puzzle.Solved.Clone();
            puzzle[location] = color;

            Assert.False(step.InitialState.Matches(puzzle));
        }

        public static IEnumerable<object[]> BadInitialStates()
            => new[]
            {
                new object[] { Location.Left, PuzzleColor.Green },
                new object[] { Location.LeftDown, PuzzleColor.Green },
                new object[] { Location.DownLeft, PuzzleColor.Green },
            };

        [Theory]
        [InlineData("F'")]
        [InlineData("F M")]
        public void No_algorithm_if_already_in_place(string moves)
        {
            var puzzle = Rotator.ApplyMoves(Puzzle.Solved, moves);
            var algorithms = step.GetPossibleAlgorithms(puzzle);

            Assert.Empty(algorithms);
        }

        [Theory]
        [InlineData("F")]                       // Front Up.
        [InlineData("F2 U' F2")]                // Front Left.
        [InlineData("F2")]                      // Front Right.
        [InlineData("F U2 B")]                  // Back Left.
        [InlineData("F U2 B'")]                 // Back Right.
        [InlineData("F M2")]                    // Back Down.
        [InlineData("F U'")]                    // Right Up.
        [InlineData("F U R2")]                  // Right Down.
        [InlineData("F U")]                     // Left Up.
        public void Can_solve_front_left(string scrambleMoves)
        {
            var scrambledPuzzle = Rotator.ApplyMoves(Puzzle.Solved, NotationParser.EnumerateMoves(scrambleMoves));

            var algorithmInfos = step.GetPossibleAlgorithms(scrambledPuzzle);

            foreach (var algorithmInfo in algorithmInfos)
            {
                foreach (var algorithm in algorithmInfo.Algorithms)
                {
                    var solvedPuzzle = Rotator.ApplyMoves(scrambledPuzzle, algorithm);
                    Assert.True(
                        step.FinishedState.Matches(solvedPuzzle),
                        $"Failed for scramble [{scrambleMoves}] with solution [{NotationParser.FormatMoves(algorithm)}].");
                }
            }
        }
    }
}