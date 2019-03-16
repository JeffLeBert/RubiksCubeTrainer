using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class PuzzleStateParser
    {
        public static (string Name, IChecker Checker) Parse(XElement element, Solver solver)
            => (element.Attribute("Name")?.Value,
                EnumerateCheckers(element, solver).Aggregate((IChecker)null, AndChecker.Combine));

        private static IEnumerable<IChecker> EnumerateCheckers(XElement element, Solver solver)
            => from node in element.Nodes()
               from checker in Parse(node, solver)
               select checker;

        private static IEnumerable<IChecker> Parse(XNode node, Solver solver)
        {
            switch (node)
            {
                case XText textNode:
                    return ParseText(textNode.Value, solver);

                case XElement element:
                    return new[] { ParseChildElement(element, solver) };

                default:
                    return new IChecker[] { };
            }
        }

        private static IEnumerable<IChecker> ParseText(string text, Solver solver)
            => from checker in text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               select ParseMatch(checker.Trim(), solver);

        private static IChecker ParseChildElement(XElement childElement, Solver solver)
        {
            switch (childElement.Name.LocalName)
            {
                case "Checks":
                    return Parse(childElement, solver).Checker;

                case "Or":
                    var orChecker = ParseOrElement(childElement, solver);
                    return ParseIsNot(childElement)
                        ? orChecker.Negate()
                        : orChecker;

                default:
                    throw new InvalidOperationException(
                        $"Must be either 'Checks' or 'Or' but was '{childElement.Name.LocalName}'.");
            }
        }

        private static OrChecker ParseOrElement(XElement orElement, Solver solver)
            => new OrChecker(
                ImmutableArray.CreateRange(
                    from childElement in orElement.Elements()
                    select ParseChildElement(childElement, solver)));

        private static bool ParseIsNot(XElement element)
        {
            var isNotText = element.Attribute("IsNot")?.Value;
            return isNotText == null
                ? false
                : bool.Parse(isNotText);
        }

        private static IChecker ParseMatch(string match, Solver solver)
            => ExpressionParser.IsExpression(match, true)
                ? ParseMatchFromExpression(match, solver)
                : ParseMatchFromParts(match);

        private static IChecker ParseMatchFromExpression(string match, Solver solver)
        {
            var (stateName, isNot, colors) = ExpressionParser.Parse(match);
            if (!solver.States.TryGetValue(stateName, out IChecker state))
            {
                throw new InvalidOperationException($"Unknown named state '{stateName}'");
            }

            state = state.WithColors(colors);

            return isNot
                ? state.Negate()
                : state;
        }

        private static IChecker ParseMatchFromParts(string match)
        {
            var parts = match.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts.Length)
            {
                case 2:
                    return SingleColorChecker.Create(parts[0], parts[1]);

                case 3:
                    return EdgeChecker.Create(parts[0], parts[1], parts[2]);

                case 4:
                    return CornerChecker.Create(parts[0], parts[1], parts[2], parts[3]);

                default:
                    throw new InvalidOperationException($"Too many parts: '{match}'");
            }
        }
    }
}