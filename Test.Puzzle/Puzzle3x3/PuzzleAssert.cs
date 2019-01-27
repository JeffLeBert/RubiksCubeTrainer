using System;
using Xunit;

namespace RubiksCubeTrainer.Puzzle3x3
{
    internal class PuzzleAssert
    {
        public static void AssertSame(Puzzle puzzle, params string[] expectedText)
        {
            var actualText = new TextRenderer(puzzle).Draw();

            Assert.Equal(string.Join(Environment.NewLine, expectedText), actualText);
        }
    }
}