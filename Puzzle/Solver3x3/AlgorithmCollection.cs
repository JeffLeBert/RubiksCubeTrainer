using System;
using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    /// <summary>
    /// A collection of algorithms that will all produce the same result.
    /// </summary>
    /// <remarks>
    /// There are many cases where there are different algorithms that will all supply
    /// the same result. In some scrambles, one might be better than the other so we
    /// keep a list of all the ones that will work.
    /// </remarks>
    public class AlgorithmCollection
    {
        public AlgorithmCollection(
            string description,
            Func<Puzzle, bool> initialState,
            ImmutableArray<ImmutableArray<NotationMoveType>> algorithms)
        {
            this.Description = description;
            this.InitialState = initialState;
            this.Algorithms = algorithms;
        }

        public ImmutableArray<ImmutableArray<NotationMoveType>> Algorithms { get; }

        public string Description { get; }

        public Func<Puzzle, bool> InitialState { get; }
    }
}