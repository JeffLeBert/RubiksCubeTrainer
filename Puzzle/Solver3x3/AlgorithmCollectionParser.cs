using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class AlgorithmCollectionParser
    {
        public static AlgorithmCollection Parse(XElement element, Solver solver)
        {
            var fromTemplateName = element.Attribute("FromTemplate")?.Value;
            if (fromTemplateName == null)
            {
                return new AlgorithmCollection(
                    element.Attribute(nameof(AlgorithmCollection.Name))?.Value,
                    element.Element(nameof(AlgorithmCollection.Description)).Value,
                    PuzzleStateParser.Parse(element.Element(nameof(AlgorithmCollection.InitialState)), solver),
                    ParseAlgorithms(element.Element(nameof(AlgorithmCollection.Algorithms)).Value));
            }
            else
            {
                var fromTemplate = solver.AlgorithmCollectionTemplates[fromTemplateName];
                var colors =
                    (from color in element.Attribute("Colors").Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                     select PuzzleColorParser.Parse(color.Trim()))
                    .ToArray();
                return fromTemplate.WithColors(colors);
            }
        }

        private static ImmutableArray<ImmutableArray<NotationMoveType>> ParseAlgorithms(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ImmutableArray.CreateRange(NotationParser.EnumerateMoves(algorithmText)));
    }
}