using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class StepParser
    {
        public static Step Parse(Solver solver, XElement stepElement)
            => new Step(
                stepElement.Attribute(nameof(Step.Name)).Value,
                PuzzleStateParser.Parse(solver, stepElement.Element(nameof(Step.InitialState))),
                PuzzleStateParser.Parse(solver, stepElement.Element(nameof(Step.FinishedState))),
                ImmutableArray.CreateRange(ParseAlgorithmCollections(solver, stepElement)));

        private static IEnumerable<AlgorithmCollection> ParseAlgorithmCollections(Solver solver, XElement stepElement)
            => from element in stepElement.Elements(nameof(AlgorithmCollection))
               select AlgorithmCollectionParser.Parse(solver, element);
    }
}