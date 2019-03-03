using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class StepParser
    {
        public static Step Parse(XElement stepElement, Func<string, Step> getStep)
            => new Step(
                stepElement.Attribute(nameof(Step.Name)).Value,
                PuzzleStateParser.Parse(stepElement.Element(nameof(Step.InitialState)), getStep),
                PuzzleStateParser.Parse(stepElement.Element(nameof(Step.FinishedState)), getStep),
                ImmutableArray.CreateRange(ParseAlgorithmCollections(stepElement, getStep)));

        private static IEnumerable<AlgorithmCollection> ParseAlgorithmCollections(
            XElement stepElement,
            Func<string, Step> getStep)
            => from element in stepElement.Elements(nameof(AlgorithmCollection))
               select AlgorithmCollectionParser.Parse(element, getStep);
    }
}