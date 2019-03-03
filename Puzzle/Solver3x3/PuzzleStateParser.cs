using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class PuzzleStateParser
    {
        public static Func<Puzzle, bool> Parse(XElement stateElement, Func<string, Step> getStep)
        {
            var previousState = GetPreviousStateIfAny(stateElement, getStep);

            var state = stateElement.HasElements
                ? ParseStateElements(stateElement)
                : ParseStateValue(stateElement.Value);

            return previousState == null
                ? state
                : puzzle => previousState(puzzle) && state(puzzle);
        }

        private static Func<Puzzle, bool> GetPreviousStateIfAny(XElement stateElement, Func<string, Step> getStep)
        {
            // The previous state is the finished state of the previous step.
            var previousStepName = stateElement.Attribute("PreviousStep")?.Value;
            return previousStepName == null
                ? null
                : getStep(previousStepName).FinishedState;
        }

        private static Func<Puzzle, bool> ParseStateElements(XElement element)
            => AndMatchers(
                from childElement in element.Elements()
                select ParseChildElement(childElement));

        private static Func<Puzzle, bool> ParseChildElement(XElement childElement)
        {
            switch (childElement.Name.LocalName)
            {
                case "Checks":
                    return ParseStateValue(childElement.Value);

                case "Or":
                    return ParseOrElement(childElement);

                default:
                    throw new InvalidOperationException(
                        $"Must be either 'Checks' or 'Or' but was '{childElement.Name.LocalName}'.");
            }
        }

        private static Func<Puzzle, bool> ParseOrElement(XElement orElement)
            => OrMatchers(
                from childElement in orElement.Elements()
                select ParseChildElement(childElement));

        private static Func<Puzzle, bool> AndMatchers(IEnumerable<Func<Puzzle, bool>> matchers)
            => CombineMatchers(matchers, (matcher1, matcher2) => puzzle => matcher1(puzzle) && matcher2(puzzle));

        private static Func<Puzzle, bool> OrMatchers(IEnumerable<Func<Puzzle, bool>> matchers)
            => CombineMatchers(matchers, (matcher1, matcher2) => puzzle => matcher1(puzzle) || matcher2(puzzle));

        private static Func<Puzzle, bool> CombineMatchers(
            IEnumerable<Func<Puzzle, bool>> matchers,
            Func<Func<Puzzle, bool>, Func<Puzzle, bool>, Func<Puzzle, bool>> combiner)
        {
            Func<Puzzle, bool> currentMatches = null;
            foreach (var matcher in matchers)
            {
                var currentMatch = matcher;
                if (currentMatches == null)
                {
                    currentMatches = currentMatch;
                }
                else
                {
                    var thisCurrentMatches = currentMatches;
                    currentMatches = combiner(thisCurrentMatches, currentMatch);
                }
            }

            return currentMatches;
        }

        [Obsolete("Use the other parser.")]
        public static Func<Puzzle, bool> ParseStateValue(string state)
        {
            var currentMatches = AndMatchers(
                from matcher in state.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ParseMatch(matcher));
            return currentMatches ?? ((Puzzle puzzle) => true);
        }

        private static Func<Puzzle, bool> ParseMatch(string match)
        {
            var parts = match.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (parts.Length)
            {
                case 2:
                    return ParseCenter(parts);

                case 3:
                    return ParseEdge(parts);

                case 4:
                    return ParseCorner(parts);

                default:
                    throw new InvalidOperationException($"Too many parts: '{match}'");
            }
        }

        private static Func<Puzzle, bool> ParseCenter(string[] parts)
        {
            var info = GetLocationInformation(parts[0].Trim());
            var color = PuzzleColorParser.Parse(parts[1]);

            if (info.IsNot)
            {
                return puzzle => !Checker.SingleColor(puzzle, info.Location, color);
            }
            else
            {
                return puzzle => Checker.SingleColor(puzzle, info.Location, color);
            }
        }

        private static Func<Puzzle, bool> ParseEdge(string[] parts)
        {
            var info = GetLocationInformation(parts[0].Trim());
            var location2 = info.Location.AdjacentEdge;
            var color1 = PuzzleColorParser.Parse(parts[1]);
            var color2 = PuzzleColorParser.Parse(parts[2]);

            if (info.IsNot)
            {
                if (info.IsAll)
                {
                    return puzzle => !Checker.EdgeOrFlipped(puzzle, info.Location, location2, color1, color2);
                }
                else
                {
                    return puzzle => !Checker.Edge(puzzle, info.Location, location2, color1, color2);
                }
            }
            else
            {
                if (info.IsAll)
                {
                    return puzzle => Checker.EdgeOrFlipped(puzzle, info.Location, location2, color1, color2);
                }
                else
                {
                    return puzzle => Checker.Edge(puzzle, info.Location, location2, color1, color2);
                }
            }
        }

        private static Func<Puzzle, bool> ParseCorner(string[] parts)
        {
            var info = GetLocationInformation(parts[0].Trim());
            var location2 = info.Location.AdjacentCorner;
            var location3 = location2.AdjacentCorner;
            var color1 = PuzzleColorParser.Parse(parts[1]);
            var color2 = PuzzleColorParser.Parse(parts[2]);
            var color3 = PuzzleColorParser.Parse(parts[3]);

            if (info.IsNot)
            {
                if (info.IsAll)
                {
                    return puzzle => !Checker.CornerOrRotated(puzzle, info.Location, location2, location3, color1, color2, color3);
                }
                else
                {
                    return puzzle => !Checker.Corner(puzzle, info.Location, location2, location3, color1, color2, color3);
                }
            }
            else
            {
                if (info.IsAll)
                {
                    return puzzle => Checker.CornerOrRotated(puzzle, info.Location, location2, location3, color1, color2, color3);
                }
                else
                {
                    return puzzle => Checker.Corner(puzzle, info.Location, location2, location3, color1, color2, color3);
                }
            }
        }

        private static (bool IsAll, bool IsNot, Location Location) GetLocationInformation(string locationPart)
        {
            var isNot = locationPart.StartsWith("!");
            if (isNot)
            {
                locationPart = locationPart.Substring(1);
            }

            var isAll = locationPart.EndsWith("*");
            if (isAll)
            {
                locationPart = locationPart.Substring(0, locationPart.Length - 1);
            }

            return (isAll, isNot, LocationParser.Parse(locationPart));
        }
    }
}