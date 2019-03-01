using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class SolutionSearch
    {
        public static readonly ImmutableArray<NotationMoveType> AllFaceMoves = ImmutableArray.Create(
            NotationMoveType.FrontClockwise, NotationMoveType.FrontCounterClockwise, NotationMoveType.FrontDouble,
            NotationMoveType.RightClockwise, NotationMoveType.RightCounterClockwise, NotationMoveType.RightDouble,
            NotationMoveType.UpClockwise, NotationMoveType.UpCounterClockwise, NotationMoveType.UpDouble,
            NotationMoveType.LeftClockwise, NotationMoveType.LeftCounterClockwise, NotationMoveType.LeftDouble,
            NotationMoveType.DownClockwise, NotationMoveType.DownCounterClockwise, NotationMoveType.DownDouble,
            NotationMoveType.BackClockwise, NotationMoveType.BackCounterClockwise, NotationMoveType.BackDouble);

        private readonly ImmutableArray<NotationMoveType> availableMoves;
        private readonly Func<Puzzle, bool> isFinished;
        private int shortestSolutionLength;

        public SolutionSearch(
            int shortestSolutionLength,
            ImmutableArray<NotationMoveType> availableMoves,
            Func<Puzzle, bool> isFinished)
        {
            this.shortestSolutionLength = shortestSolutionLength;
            this.availableMoves = availableMoves;
            this.isFinished = isFinished;
        }

        public IEnumerable<IEnumerable<NotationMoveType>> Search(Puzzle scrambledPuzzle)
            => this.SearchThisLayer(new SearchState(scrambledPuzzle)).Result;

        private Task<IEnumerable<IEnumerable<NotationMoveType>>> SearchThisLayer(SearchState searchState)
        {
            //System.Diagnostics.Debug.WriteLine(NotationParser.FormatMoves(searchState.Moves));

            if (searchState.Moves.Length > this.shortestSolutionLength)
            {
                return Task.FromResult(Enumerable.Empty<IEnumerable<NotationMoveType>>());
            }

            if (isFinished(searchState.Puzzle))
            {
                UpdateShortestSolutionLength(searchState.Moves.Length);
                return Task.FromResult<IEnumerable<IEnumerable<NotationMoveType>>>(new[] { (IEnumerable<NotationMoveType>)searchState.Moves });
            }

            var lastMove = searchState.Moves.IsEmpty ? NotationMoveType.None : searchState.Moves.Last();
            var tasks = new List<Task<IEnumerable<IEnumerable<NotationMoveType>>>>();
            foreach (var availableMove in availableMoves)
            {
                if (lastMove.Name == availableMove.Name)
                {
                    continue;
                }

                var task = Task.Factory.StartNew(() => this.SearchThisLayer(searchState.WithMove(availableMove)).Result);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            var solutions = new List<IEnumerable<NotationMoveType>>();
            foreach (var task in tasks)
            {
                solutions.AddRange(task.Result);
            }

            return Task.FromResult<IEnumerable<IEnumerable<NotationMoveType>>>(solutions);
        }

        private void UpdateShortestSolutionLength(int length)
        {
            while (true)
            {
                var initialLength = this.shortestSolutionLength;
                if (initialLength <= length)
                {
                    break;
                }

                Interlocked.CompareExchange(ref this.shortestSolutionLength, length, initialLength);

                System.Diagnostics.Debug.WriteLine("Dropped length to: " + initialLength.ToString() + " from " + length.ToString());
            }
        }

        [System.Diagnostics.DebuggerDisplay("Moves = {Moves}, Depth = {Depth}")]
        private struct SearchState
        {
            public SearchState(Puzzle puzzle)
                : this(puzzle, ImmutableArray<NotationMoveType>.Empty)
            {
            }

            private SearchState(Puzzle puzzle, ImmutableArray<NotationMoveType> moves)
            {
                this.Puzzle = puzzle;
                this.Moves = moves;
            }

            public ImmutableArray<NotationMoveType> Moves { get; }

            public Puzzle Puzzle { get; }

            public SearchState WithMove(NotationMoveType move)
                => new SearchState(Rotator.ApplyMove(this.Puzzle, move), this.Moves.Add(move));
        }
    }
}