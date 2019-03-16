using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SolverFailureInformation
    {
        private SolverFailureInformation(
            ImmutableArray<ImmutableArray<NotationMoveType>> algorithms,
            bool solved,
            string failDescription)
        {
            this.Algorithms = algorithms;
            this.Solved = solved;
            this.FailDescription = failDescription;
        }

        public static SolverFailureInformation Empty { get; }
            = new SolverFailureInformation(ImmutableArray<ImmutableArray<NotationMoveType>>.Empty, false, null);

        public ImmutableArray<ImmutableArray<NotationMoveType>> Algorithms { get; }

        public bool AtEnd => this.Solved || (this.FailDescription != null);

        public bool Solved { get; }

        public string FailDescription { get; }

        public SolverFailureInformation WithMoves(ImmutableArray<NotationMoveType> moves)
            => new SolverFailureInformation(this.Algorithms.Add(moves), this.Solved, this.FailDescription);

        public SolverFailureInformation WithFailed(string failDescription)
            => new SolverFailureInformation(this.Algorithms, this.Solved, failDescription);

        public SolverFailureInformation WithSolved()
            => new SolverFailureInformation(
                this.Algorithms,
                true,
                this.FailDescription);
    }
}