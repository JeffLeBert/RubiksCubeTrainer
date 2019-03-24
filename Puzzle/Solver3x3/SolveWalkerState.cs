using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SolveWalkerState
    {
        public SolveWalkerState(Puzzle puzzle, Algorithm algorithm, ImmutableArray<NotationMoveType> moves)
        {
            this.Puzzle = puzzle;
            this.Algorithm = algorithm;
            this.Moves = moves;
        }

        public Algorithm Algorithm { get; }

        public ImmutableArray<NotationMoveType> Moves { get; }

        public Puzzle Puzzle { get; }
    }
}