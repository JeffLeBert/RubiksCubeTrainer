using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        public Solver(ImmutableDictionary<string, Step> steps)
        {
            this.Steps = steps;
        }

        public ImmutableDictionary<string, Step> Steps { get; }

        public IEnumerable<Step> NextSteps(Puzzle puzzle)
            => from step in this.Steps.Values
               where step.InitialState.Matches(puzzle)
               select step;
    }
}