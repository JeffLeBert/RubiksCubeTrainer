using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SolverFailureInformation
    {
        private SolverFailureInformation(IEnumerable<NotationMoveType> moves, bool noMoreSteps, bool noMoreAlgorithms)
        {
            this.Moves = moves;
            this.NoMoreSteps = noMoreSteps;
            this.NoMoreAlgorithms = noMoreAlgorithms;
        }

        public static SolverFailureInformation Empty { get; }
            = new SolverFailureInformation(new NotationMoveType[] { }, false, false);

        public bool AtEnd => this.NoMoreSteps || this.NoMoreAlgorithms;

        public IEnumerable<NotationMoveType> Moves { get; }

        public bool NoMoreSteps { get; }

        public bool NoMoreAlgorithms { get; }

        public SolverFailureInformation WithMoves(IEnumerable<NotationMoveType> moves)
            => new SolverFailureInformation(
                this.Moves.Concat(moves).ToArray(),
                this.NoMoreSteps,
                this.NoMoreAlgorithms);

        public SolverFailureInformation WithNoMoreAlgorithms()
            => new SolverFailureInformation(this.Moves, this.NoMoreSteps, true);

        public SolverFailureInformation WithNoMoreSteps()
            => new SolverFailureInformation(this.Moves, true, this.NoMoreAlgorithms);
    }
}