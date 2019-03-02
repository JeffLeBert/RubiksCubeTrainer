using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class AlgorithmCollectionParser
    {
        public static AlgorithmCollection Parse(Solver solver, XElement element)
            => new AlgorithmCollection(
                element.Element(nameof(AlgorithmCollection.Description)).Value,
                PuzzleStateParser.Parse(solver, element.Element(nameof(AlgorithmCollection.InitialState))),
                ParseAlgorithms(element.Element(nameof(AlgorithmCollection.Algorithms)).Value));

        [Obsolete("Don't use this.")]
        public static ImmutableArray<IEnumerable<NotationMoveType>> ParseAlgorithms(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select NotationParser.EnumerateMoves(algorithmText));
    }
}