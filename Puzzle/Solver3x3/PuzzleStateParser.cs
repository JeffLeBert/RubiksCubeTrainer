using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class PuzzleStateParser
    {
        public static IChecker Parse(XElement stateElement, Solver solver)
        {
            var previousState = GetPreviousStateIfAny(stateElement, solver);

            var state = stateElement.HasElements
                ? ParseStateElements(stateElement)
                : ParseStateValue(stateElement.Value);

            return AndChecker.Combine(previousState, state);
        }

        private static IChecker GetPreviousStateIfAny(XElement stateElement, Solver solver)
        {
            if (stateElement == null)
            {
                System.Diagnostics.Debugger.Break();
            }

            // The previous state is the finished state of the previous step.
            var previousStepName = stateElement.Attribute("PreviousStep")?.Value;
            if (previousStepName == null)
            {
                return null;
            }

            return solver.Steps.TryGetValue(previousStepName, out Step previousStep)
                ? previousStep.FinishedState
                : throw new InvalidOperationException($"Unknown step name {previousStepName}");
        }

        private static IChecker ParseStateElements(XElement element)
            => (from childElement in element.Elements() select ParseChildElement(childElement))
            .Aggregate(AndChecker.Combine);

        private static IChecker ParseChildElement(XElement childElement)
        {
            switch (childElement.Name.LocalName)
            {
                case "Checks":
                    return ParseStateValue(childElement.Value);

                case "Or":
                    var orChecker = ParseOrElement(childElement);
                    return ParseIsNot(childElement)
                        ? orChecker.WithNot()
                        : orChecker;

                default:
                    throw new InvalidOperationException(
                        $"Must be either 'Checks' or 'Or' but was '{childElement.Name.LocalName}'.");
            }
        }

        private static OrChecker ParseOrElement(XElement orElement)
        {
            var checkers = ImmutableArray.CreateRange(
                from childElement in orElement.Elements()
                select ParseChildElement(childElement));
            return new OrChecker(checkers, false);
        }

        private static bool ParseIsNot(XElement element)
        {
            var isNotText = element.Attribute("IsNot")?.Value;
            return isNotText == null
                ? false
                : bool.Parse(isNotText);
        }

        private static IChecker ParseStateValue(string state)
        {
            var checkers = ImmutableArray.CreateRange(
                from matcher in state.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ParseMatch(matcher));
            return checkers.Length == 1
                ? checkers[0]
                : new AndChecker(checkers);
        }

        private static IChecker ParseMatch(string match)
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