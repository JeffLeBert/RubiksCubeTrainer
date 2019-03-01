using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    /// <summary>
    /// A collection of algorithms that will all produce the same result.
    /// </summary>
    /// <remarks>
    /// There are many cases where there are different algorithms that will all supply
    /// the same result. In some scrambles, one might be better than the other so we
    /// keep a list of all the ones that will work.
    /// </remarks>
    public class AlgorithmCollection
    {
        public AlgorithmCollection(
            string description,
            PuzzleState initialState,
            params NotationMoveType[] moves)
        {
            this.Description = description;
            this.InitialState = initialState;
            this.Algorithms = ImmutableArray.Create<IEnumerable<NotationMoveType>>(moves);
        }

        public AlgorithmCollection(XElement element)
        {
            this.Description = element.Element(nameof(Description)).Value;
            this.InitialState = new PuzzleState(element.Element(nameof(InitialState)).Value);
            this.Algorithms = ParseAlgorithms(element.Element(nameof(this.Algorithms)).Value);
        }

        public ImmutableArray<IEnumerable<NotationMoveType>> Algorithms { get; }

        public string Description { get; }

        public PuzzleState InitialState { get; }

        private static ImmutableArray<IEnumerable<NotationMoveType>> ParseAlgorithms(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select NotationParser.EnumerateMoves(algorithmText));
    }
}