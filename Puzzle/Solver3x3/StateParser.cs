using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class StateParser
    {
        public static (string Name, IState State) Parse(string baseName, IState parentState, XElement element, Solver solver)
            => (BuildName(baseName, element.Attribute("Name")?.Value),
                EnumerateStates(baseName, element, solver).Aggregate(parentState, AndState.Combine));

        private static string BuildName(string baseName, string name)
            => baseName == null
            ? name
            : baseName + "." + name;

        private static IEnumerable<IState> EnumerateStates(string baseName, XElement element, Solver solver)
            => from node in element.Nodes()
               from state in Parse(baseName, node, solver)
               select state;

        private static IEnumerable<IState> Parse(string baseName, XNode node, Solver solver)
        {
            switch (node)
            {
                case XText textNode:
                    return ParseText(textNode.Value, solver);

                case XElement element:
                    return new[] { ParseChildElement(baseName, element, solver) };

                default:
                    return new IState[] { };
            }
        }

        private static IEnumerable<IState> ParseText(string text, Solver solver)
            => from state in text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               select ParseState(state.Trim(), solver);

        private static IState ParseChildElement(string baseName, XElement childElement, Solver solver)
        {
            switch (childElement.Name.LocalName)
            {
                case "State":
                    return Parse(baseName, null, childElement, solver).State;

                case "Or":
                    var orState = ParseOrElement(baseName, childElement, solver);
                    return ParseIsNot(childElement)
                        ? orState.Negate()
                        : orState;

                default:
                    throw new InvalidOperationException(
                        $"Must be either 'State' or 'Or' but was '{childElement.Name.LocalName}'.");
            }
        }

        private static OrState ParseOrElement(string baseName, XElement orElement, Solver solver)
            => new OrState(
                ImmutableArray.CreateRange(
                    from childElement in orElement.Elements()
                    select ParseChildElement(baseName, childElement, solver)));

        private static bool ParseIsNot(XElement element)
        {
            var isNotText = element.Attribute("IsNot")?.Value;
            return isNotText == null
                ? false
                : bool.Parse(isNotText);
        }

        private static IState ParseState(string match, Solver solver)
            => ExpressionParser.IsExpression(match, true)
                ? ParseStateFromExpression(match, solver)
                : ParseStateFromParts(match);

        private static IState ParseStateFromExpression(string match, Solver solver)
        {
            var (stateName, isNot, colors) = ExpressionParser.Parse(match);
            if (!solver.States.TryGetValue(stateName, out IState state))
            {
                throw new InvalidOperationException($"Unknown named state '{stateName}'");
            }

            state = state.WithColors(colors);

            return isNot
                ? state.Negate()
                : state;
        }

        private static IState ParseStateFromParts(string match)
        {
            var parts = match.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts.Length)
            {
                case 2:
                    return SingleColorState.Create(parts[0], parts[1]);

                case 3:
                    return EdgeState.Create(parts[0], parts[1], parts[2]);

                case 4:
                    return CornerState.Create(parts[0], parts[1], parts[2], parts[3]);

                default:
                    throw new InvalidOperationException($"Too many parts: '{match}'");
            }
        }
    }
}