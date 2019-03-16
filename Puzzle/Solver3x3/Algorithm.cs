using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    /// <summary>
    /// A collection of moves that will all produce the same result.
    /// </summary>
    /// <remarks>
    /// There are many cases where there are different moves that will all supply
    /// the same result. In some scrambles, one might be better than the other so we
    /// keep a list of all the ones that will work.
    /// </remarks>
    public class Algorithm
    {
        public Algorithm(
            string name,
            string description,
            IChecker initialState,
            ImmutableArray<ImmutableArray<NotationMoveType>> moves)
        {
            this.Name = name;
            this.Description = description;
            this.InitialState = initialState;
            this.Moves = moves;
        }

        public ImmutableArray<ImmutableArray<NotationMoveType>> Moves { get; }

        public string Description { get; }

        public IChecker InitialState { get; }

        public string Name { get; }

        public Algorithm WithColors(PuzzleColor[] colors)
            => new Algorithm(
                this.Name,
                this.Description,
                this.InitialState.WithColors(colors),
                this.Moves);
    }
}