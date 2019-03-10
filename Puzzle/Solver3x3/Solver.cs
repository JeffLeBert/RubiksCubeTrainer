using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private Solver(
            ImmutableDictionary<string, Step> steps,
            ImmutableDictionary<string, AlgorithmCollection> algorithmCollectionTemplates)
        {
            this.Steps = steps;
            this.AlgorithmCollectionTemplates = algorithmCollectionTemplates;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary<string, Step>.Empty,
            ImmutableDictionary<string, AlgorithmCollection>.Empty);

        public ImmutableDictionary<string, AlgorithmCollection> AlgorithmCollectionTemplates { get; }

        public ImmutableDictionary<string, Step> Steps { get; }

        public IEnumerable<Step> NextSteps(Puzzle puzzle)
            => from step in this.Steps.Values
               where step.InitialState.Matches(puzzle)
               select step;

        public Solver With(AlgorithmCollection algorithmCollection)
            => new Solver(this.Steps, this.AlgorithmCollectionTemplates.Add(algorithmCollection.Name, algorithmCollection));

        public Solver With(Step step)
            => new Solver(this.Steps.Add(step.Name, step), this.AlgorithmCollectionTemplates);
    }
}