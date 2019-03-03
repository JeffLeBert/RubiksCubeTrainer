using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class AlgorithmCollectionParser
    {
        public static AlgorithmCollection Parse(XElement element, Func<string, Step> getStep)
            => new AlgorithmCollection(
                element.Element(nameof(AlgorithmCollection.Description)).Value,
                PuzzleStateParser.Parse(element.Element(nameof(AlgorithmCollection.InitialState)), getStep),
                ParseAlgorithms(element.Element(nameof(AlgorithmCollection.Algorithms)).Value));

        [Obsolete("Remove usage except here.")]
        public static ImmutableArray<ImmutableArray<NotationMoveType>> ParseAlgorithms(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ImmutableArray.CreateRange(NotationParser.EnumerateMoves(algorithmText)));
    }
}