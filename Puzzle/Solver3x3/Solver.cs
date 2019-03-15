using System;
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
            ImmutableDictionary<string, IChecker> states,
            ImmutableDictionary<string, AlgorithmCollection> algorithmCollections)
        {
            this.Steps = steps;
            this.States = states;
            this.AlgorithmCollections = algorithmCollections;
        }

        public static Solver Empty { get; } = new Solver(
            ImmutableDictionary.Create<string, Step>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, IChecker>(StringComparer.OrdinalIgnoreCase),
            ImmutableDictionary.Create<string, AlgorithmCollection>(StringComparer.OrdinalIgnoreCase));

        public ImmutableDictionary<string, AlgorithmCollection> AlgorithmCollections { get; }

        public ImmutableDictionary<string, IChecker> States { get; }

        public ImmutableDictionary<string, Step> Steps { get; }

        public IEnumerable<Step> NextSteps(Puzzle puzzle)
            => from step in this.Steps.Values
               where step.InitialState.Matches(puzzle)
               select step;

        public Solver With(AlgorithmCollection algorithmCollection)
            => new Solver(
                this.Steps,
                this.States,
                this.AlgorithmCollections.Add(algorithmCollection.Name, algorithmCollection));

        public Solver With(string name, IChecker state)
            => new Solver(
                this.Steps,
                this.States.Add(name, state),
                this.AlgorithmCollections);

        public Solver With(Step step)
            => new Solver(
                this.Steps.Add(step.Name, step),
                this.States,
                this.AlgorithmCollections);
    }
}