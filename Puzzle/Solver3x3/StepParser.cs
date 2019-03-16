using System;
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
                ImmutableArray.CreateRange(ParseAlgorithms(stepElement, solver)));

        private static IEnumerable<Algorithm> ParseAlgorithms(XElement stepElement, Solver solver)
            => from node in stepElement.Nodes()
               from algorithm in Parse(node, solver)
               select algorithm;

        private static IEnumerable<Algorithm> Parse(XNode node, Solver solver)
        {
            switch (node)
            {
                case XText textNode:
                    return ParseText(textNode.Value, solver);

                case XElement element:
                    return ParseChildElement(element, solver);

                default:
                    return Enumerable.Empty<Algorithm>();
            }
        }

        private static IEnumerable<Algorithm> ParseText(string value, Solver solver)
            => from expression in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               select ParseNamedAlgorithm(expression.Trim(), solver);

        private static Algorithm ParseNamedAlgorithm(string expression, Solver solver)
        {
            var (name, _, colors) = ExpressionParser.Parse(expression);
            if (!solver.Algorithms.TryGetValue(name, out Algorithm algorithm))
            {
                throw new InvalidOperationException($"Unknown named algorithm '{name}'");
            }

            return algorithm.WithColors(colors);
        }

        private static IEnumerable<Algorithm> ParseChildElement(XElement element, Solver solver)
            => element.Name.LocalName == "Algorithm"
            ? new[] { AlgorithmParser.Parse(element, solver) }
            : Enumerable.Empty<Algorithm>();
    }
}