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
    [System.Diagnostics.DebuggerDisplay("{Name}")]
    public class Algorithm
    {
        private Algorithm(
            string name,
            string description,
            IState initialState,
            IState finalState,
            ImmutableArray<ImmutableArray<NotationMoveType>> moves)
        {
            this.Name = name;
            this.Description = description;
            this.InitialState = initialState;
            this.FinishedState = finalState;
            this.Moves = moves;
        }

        public static Algorithm Empty { get; } = new Algorithm(
            null,
            null,
            null,
            null,
            ImmutableArray<ImmutableArray<NotationMoveType>>.Empty);

        public ImmutableArray<ImmutableArray<NotationMoveType>> Moves { get; }

        public string Description { get; }

        public IState FinishedState { get; }

        public IState InitialState { get; }

        public string Name { get; }

        public Algorithm WithColors(PuzzleColor[] colors)
            => (colors == null) || (colors.Length == 0)
            ? this
            : new Algorithm(
                this.Name,
                this.Description,
                this.InitialState.WithColors(colors),
                this.FinishedState.WithColors(colors),
                this.Moves);

        public Algorithm WithDescription(string description)
            => description == this.Description
            ? this
            : new Algorithm(
                this.Name,
                description,
                this.InitialState,
                this.FinishedState,
                this.Moves);

        public Algorithm WithFinishedState(IState state)
            => state == this.FinishedState
            ? this
            : new Algorithm(
                this.Name,
                this.Description,
                this.InitialState,
                AndState.Combine(this.FinishedState, state),
                this.Moves);

        public Algorithm WithInitialState(IState state)
            => state == this.InitialState
            ? this
            : new Algorithm(
                this.Name,
                this.Description,
                AndState.Combine(this.InitialState, state),
                this.FinishedState,
                this.Moves);

        public Algorithm WithName(string name)
            => name == this.Name
            ? this
            : new Algorithm(
                name,
                this.Description,
                this.InitialState,
                this.FinishedState,
                this.Moves);

        public Algorithm WithMoves(ImmutableArray<ImmutableArray<NotationMoveType>> moves)
            => moves == this.Moves
            ? this
            : new Algorithm(
                this.Name,
                this.Description,
                this.InitialState,
                this.FinishedState,
                moves);
    }
}