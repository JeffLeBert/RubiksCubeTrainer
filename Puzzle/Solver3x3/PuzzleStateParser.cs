using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class PuzzleStateParser
    {
        public static (string Name, IChecker Checker) Parse(XElement stateElement, Solver solver)
        {
            var name = stateElement.Attribute("Name")?.Value;
            var state = stateElement.HasElements
                ? ParseStateElements(stateElement, solver)
                : ParseStateValue(stateElement.Value, solver);

            return (name, state);
        }

        private static IChecker ParseStateElements(XElement element, Solver solver)
            => (from childElement in element.Elements() select ParseChildElement(childElement, solver))
            .Aggregate(AndChecker.Combine);

        private static IChecker ParseChildElement(XElement childElement, Solver solver)
        {
            switch (childElement.Name.LocalName)
            {
                case "Checks":
                    return ParseStateValue(childElement.Value, solver);

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

        private static IChecker ParseStateValue(string state, Solver solver)
        {
            var checkers = ImmutableArray.CreateRange(
                from matcher in state.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ParseMatch(matcher, solver));
            return checkers.Length == 1
                ? checkers[0]
                : new AndChecker(checkers);
        }

        private static IChecker ParseMatch(string match, Solver solver)
        {
            match = match.Trim();
            return (match.StartsWith("{") || match.StartsWith("!{"))
                ? ParseMatchFromNamedState(match, solver)
                : ParseMatchFromParts(match);
        }

        private static IChecker ParseMatchFromNamedState(string match, Solver solver)
        {
            if (!match.EndsWith("}"))
            {
                throw new InvalidOperationException($"Named state must end with '}}': '{match}'");
            }

            var isNot = match.StartsWith("!");
            if (isNot)
            {
                match = match.Substring(1);
            }

            var stateInfo = match.Substring(1, match.Length - 2).Trim();

            var (stateName, colors) = ExpressionParser.Parse(stateInfo);
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