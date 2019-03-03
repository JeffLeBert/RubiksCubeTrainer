using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SolverFailureInformation
    {
        private SolverFailureInformation(
            ImmutableArray<ImmutableArray<NotationMoveType>> algorithms,
            bool noMoreSteps,
            bool noMoreAlgorithms,
            bool foundCycle)
        {
            this.Algorithms = algorithms;
            this.NoMoreSteps = noMoreSteps;
            this.NoMoreAlgorithms = noMoreAlgorithms;
            this.FoundCycle = foundCycle;
        }

        public static SolverFailureInformation Empty { get; }
            = new SolverFailureInformation(ImmutableArray<ImmutableArray<NotationMoveType>>.Empty, false, false, false);

        public ImmutableArray<ImmutableArray<NotationMoveType>> Algorithms { get; }

        public bool AtEnd => this.NoMoreSteps || this.NoMoreAlgorithms || this.FoundCycle;

        public bool Failed => this.NoMoreAlgorithms || this.FoundCycle;

        public bool FoundCycle { get; }

        public bool NoMoreSteps { get; }

        public bool NoMoreAlgorithms { get; }

        public SolverFailureInformation WithAlgorithm(ImmutableArray<NotationMoveType> algorithm)
            => new SolverFailureInformation(
                this.Algorithms.Add(algorithm),
                this.NoMoreSteps,
                this.NoMoreAlgorithms,
                this.FoundCycle);

        public SolverFailureInformation WithNoMoreAlgorithms()
            => new SolverFailureInformation(this.Algorithms, this.NoMoreSteps, true, this.FoundCycle);

        public SolverFailureInformation WithNoMoreSteps()
            => new SolverFailureInformation(this.Algorithms, true, this.NoMoreAlgorithms, this.FoundCycle);

        public SolverFailureInformation WithFoundCycle()
            => new SolverFailureInformation(this.Algorithms, this.NoMoreSteps, this.NoMoreAlgorithms, true);
    }
}