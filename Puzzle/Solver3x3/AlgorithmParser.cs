using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class AlgorithmParser
    {
        public static Algorithm Parse(XElement element, Solver solver)
        {
            var fromTemplateName = element.Attribute("FromTemplate")?.Value;
            if (fromTemplateName == null)
            {
                var name = element.Attribute(nameof(Algorithm.Name))?.Value;
                var description = element.Element(nameof(Algorithm.Description)).Value;
                return new Algorithm(
                    name,
                    description,
                    PuzzleStateParser.Parse(element.Element(nameof(Algorithm.InitialState)), solver).Checker,
                    ParseMoves(element.Element(nameof(Algorithm.Moves)).Value));
            }
            else
            {
                var fromTemplate = solver.Algorithms[fromTemplateName];
                var colors =
                    (from color in element.Attribute("Colors").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                     select PuzzleColorParser.Parse(color.Trim()))
                    .ToArray();
                return fromTemplate.WithColors(colors);
            }
        }

        private static ImmutableArray<ImmutableArray<NotationMoveType>> ParseMoves(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ImmutableArray.CreateRange(NotationParser.EnumerateMoves(algorithmText)));
    }
}