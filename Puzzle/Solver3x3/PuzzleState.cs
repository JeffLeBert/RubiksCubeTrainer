using System;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class PuzzleState
    {
        public PuzzleState(string state)
        {
            this.Matches = Parse(state);
        }

        public Func<Puzzle, bool> Matches { get; }

        private static Func<Puzzle, bool> Parse(string state)
        {
            Func<Puzzle, bool> currentMatches = null;
            foreach (var match in state.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var currentMatch = ParseMatch(match);
                if (currentMatches == null)
                {
                    currentMatches = currentMatch;
                }
                else
                {
                    var thisCurrentMatches = currentMatches;
                    currentMatches = puzzle => thisCurrentMatches(puzzle) && currentMatch(puzzle);
                }
            }

            if (currentMatches == null)
            {
                return puzzle => true;
            }
            else
            {
                return currentMatches;
            }
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
                    throw new InvalidOperationException("Too many parts.");
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