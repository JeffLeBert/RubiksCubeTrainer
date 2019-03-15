using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class StepParser
    {
        public static Step Parse(XElement stepElement, Solver solver)
            => new Step(
                stepElement.Attribute(nameof(Step.Name)).Value,
                PuzzleStateParser.Parse(stepElement.Element(nameof(Step.InitialState)), solver).Checker,
                PuzzleStateParser.Parse(stepElement.Element(nameof(Step.FinishedState)), solver).Checker,
                ImmutableArray.CreateRange(ParseAlgorithmCollections(stepElement, solver)));

        private static IEnumerable<AlgorithmCollection> ParseAlgorithmCollections(XElement stepElement, Solver solver)
            => from element in stepElement.Elements(nameof(AlgorithmCollection))
               select AlgorithmCollectionParser.Parse(element, solver);
    }
}