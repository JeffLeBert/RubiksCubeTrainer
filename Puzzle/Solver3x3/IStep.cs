using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    /// <summary>
    /// Used to find a set of <see cref="Algorithm"/>s that can improve the puzzle.
    /// </summary>
    public interface IStep
    {
        /// <summary>
        /// Determines if this step should be used.
        /// </summary>
        /// <param name="puzzle">The current puzzle.</param>
        /// <returns><c>true</c> if this step should be used on the specified puzzle.</returns>
        /// <remarks>
        /// It is assumed that all previous steps have been done so that we don't have to recheck
        /// the complete state of the puzzle.
        /// </remarks>
        bool ShouldUse(Puzzle puzzle);

        /// <summary>
        /// Gets all the <see cref="Algorithm"/>s that can improve the puzzle.
        /// </summary>
        /// <param name="puzzle">The current puzzle.</param>
        /// <returns>The <see cref="Algorithm"/>s that can improve the puzzle.</returns>
        IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle);
    }
}