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
            string name,
            string description,
            IChecker initialState,
            ImmutableArray<ImmutableArray<NotationMoveType>> algorithms)
        {
            this.Name = name;
            this.Description = description;
            this.InitialState = initialState;
            this.Algorithms = algorithms;
        }

        public ImmutableArray<ImmutableArray<NotationMoveType>> Algorithms { get; }

        public string Description { get; }

        public IChecker InitialState { get; }

        public string Name { get; }

        public AlgorithmCollection WithColors(PuzzleColor[] colors)
        {
            return new AlgorithmCollection(
                this.Name,
                this.Description,
                this.InitialState.WithColors(colors),
                this.Algorithms);
        }
    }
}