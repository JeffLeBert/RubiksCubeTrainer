using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Step : IStep
    {
        private readonly ImmutableArray<AlgorithmCollection> allAlgorithmCollections;

        public Step(XElement stepElement)
        {
            this.Name = stepElement.Attribute(nameof(Name)).Value;
            this.InitialState = new PuzzleState(stepElement.Element(nameof(InitialState)).Value);
            this.FinishedState = new PuzzleState(stepElement.Element(nameof(FinishedState)).Value);
            this.allAlgorithmCollections = ImmutableArray.CreateRange(ParseAlgorithmCollections(stepElement));
        }

        public PuzzleState FinishedState { get; }

        public PuzzleState InitialState { get; }

        public string Name { get; }

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmCollection in this.allAlgorithmCollections
               where algorithmCollection.InitialState.Matches(puzzle)
               select algorithmCollection;

        public bool ShouldUse(Puzzle puzzle)
            => this.InitialState.Matches(puzzle);

        private static IEnumerable<AlgorithmCollection> ParseAlgorithmCollections(XElement stepElement)
            => from element in stepElement.Elements(nameof(AlgorithmCollection))
               select new AlgorithmCollection(element);
    }
}