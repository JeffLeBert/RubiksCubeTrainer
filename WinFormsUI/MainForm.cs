using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using RubiksCubeTrainer.Puzzle3x3;
using RubiksCubeTrainer.Solver3x3;

namespace RubiksCubeTrainer.WinFormsUI
{
    public partial class MainForm : Form
    {
        private Puzzle puzzle = Puzzle.Solved;

        public MainForm()
        {
            this.InitializeComponent();
            this.ScramblePuzzle();
        }

        private void picPuzzle_Paint(object sender, PaintEventArgs e)
        {
            var renderer = new FlatPuzzleRenderer3x3(this.puzzle);
            renderer.Draw(e.Graphics, this.picPuzzle.Size);
        }

        private void txtScrambleMoves_TextChanged(object sender, EventArgs e)
        {
            this.txtSolutionMoves.Text = string.Empty;
            this.txtSolutionDescription.Text = string.Empty;

            this.UpdateWithMoves(this.txtScrambleMoves.Text);
        }

        private void txtSolutionMoves_TextChanged(object sender, EventArgs e)
        {
            this.UpdateWithMoves(this.txtScrambleMoves.Text + this.txtSolutionMoves.Text);
        }

        private void UpdateWithMoves(string moves)
        {
            this.puzzle = Puzzle.Solved;
            try
            {
                foreach (var move in NotationParser.EnumerateMoves(moves))
                {
                    this.puzzle = Rotator.ApplyMove(this.puzzle, move);
                }
            }
            catch (Exception ex)
            {
                this.BeginInvoke(
                    (Action)
                    (() => { MessageBox.Show("Something went wrong:\r\n" + ex.ToString()); }));

                // For now just don't say anything and blank out the cube display.
                this.picPuzzle.Visible = false;
                return;
            }

            this.picPuzzle.Invalidate();
            this.picPuzzle.Visible = true;

            this.Refresh();
        }

        private void cmdScramble_Click(object sender, EventArgs e)
        {
            this.ScramblePuzzle();
            this.txtSolutionDescription.Text = string.Empty;
        }

        private void ScramblePuzzle()
        {
            this.txtScrambleMoves.Text = Scrambler.Scamble();
            this.txtScrambleMoves.SelectionStart = int.MaxValue;
        }

        private void cmdSolve_Click(object sender, EventArgs e)
        {
            var solutionFinder = new ShortestSolutionFinder(WellKnownSolvers.Roux);
            var (foundSolution, description, states) = solutionFinder.FindSolution(
                Rotator.ApplyMoves(Puzzle.Solved, this.txtScrambleMoves.Text));

            UpdateSolutionUI(description, states);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
            => this.Close();

        private void FindFailureX10000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var stopwatch = Stopwatch.StartNew();
            const int SearchCount = 10000;
            for (int i = 0; i < SearchCount; i++)
            {
                var solutionFinder = new ShortestSolutionFinder(WellKnownSolvers.Roux);
                var (foundSolution, description, states) = solutionFinder.FindSolution(
                    Rotator.ApplyMoves(Puzzle.Solved, Scrambler.Scamble()));

                if (!foundSolution)
                {
                    this.UpdateSolutionUI(description, states);
                    return;
                }

            }
            stopwatch.Stop();

            this.txtScrambleMoves.Text = string.Empty;
            this.txtSolutionMoves.Text = string.Empty;
            this.txtSolutionDescription.Text = $"{SearchCount} solutions found in {stopwatch.ElapsedMilliseconds}ms.";
            this.Refresh();
        }

        private void UpdateSolutionUI(string description, IEnumerable<SolveWalkerState> states)
        {
            this.txtSolutionMoves.Text =
                string.Join(
                    " ",
                    from solveState in states
                    select "(" + NotationParser.FormatMoves(solveState.Moves) + ")");
            this.txtSolutionDescription.Text = description + Environment.NewLine + Environment.NewLine
                + string.Join(
                    Environment.NewLine + Environment.NewLine,
                    from solveState in states
                    select solveState.Algorithm.Name + Environment.NewLine + NotationParser.FormatMoves(solveState.Moves));
        }

        private void FindAlgorithmSolutionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtScrambleMoves.Text = string.Empty;
            this.txtSolutionMoves.Text = string.Empty;
            this.txtSolutionDescription.Text = string.Empty;
            this.Refresh();

            var solver = WellKnownSolvers.Roux;
            var algorithm = solver.Algorithms["LeftDown.RightUp White Blue"];
            var stopwatch1 = Stopwatch.StartNew();
            Puzzle foundPuzzle = null;
            for (int i = 0; i < 10000; i++)
            {
                var initialPuzzle = Rotator.ApplyMoves(Puzzle.Solved, Scrambler.Scamble());
                foundPuzzle = new AlgorithmUsageFinder(WellKnownSolvers.Roux, algorithm).FindUsage(initialPuzzle);
                if (foundPuzzle != null)
                {
                    break;
                }
            }
            stopwatch1.Stop();

            if (foundPuzzle == null)
            {
                this.txtSolutionDescription.Text = "No usage of this algorithm in this puzzle.";
                return;
            }

            var stopwatch2 = Stopwatch.StartNew();
            var solutions = new SolutionSearch(6, SolutionSearch.AllFaceMoves, algorithm.FinishedState)
                .Search(foundPuzzle);
            stopwatch2.Stop();

            var message = "Found puzzle for algorithm in " + stopwatch1.ElapsedMilliseconds.ToString() + "ms\r\n"
                + "Found solutions in " + stopwatch2.ElapsedMilliseconds.ToString() + "ms\r\n"
                + string.Join(
                    Environment.NewLine,
                    from solution in solutions
                    orderby solution.Count()
                    select NotationParser.FormatMoves(solution));

            this.txtSolutionDescription.Text = message;
        }
    }
}