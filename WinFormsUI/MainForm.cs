using System;
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
            this.txtScrambleMoves.Text = Scrambler.Scamble(10);
            this.txtScrambleMoves.SelectionStart = int.MaxValue;
        }

        private void cmdSolve_Click(object sender, EventArgs e)
        {
            var solutionMoves = string.Empty;
            var solutionDescription = string.Empty;
            var currentPuzzle = this.puzzle;
            while (true)
            {
                var nextStep = WellKnownSolvers.Roux.NextSteps(currentPuzzle).FirstOrDefault();
                if (nextStep == null)
                {
                    solutionDescription += "No steps found.";
                    break;
                }

                var firstAlgorithmInfo = nextStep.GetPossibleAlgorithms(currentPuzzle).FirstOrDefault();
                if (firstAlgorithmInfo == null)
                {
                    solutionDescription += "No algorithms found.";
                    break;
                }

                currentPuzzle = Rotator.ApplyMoves(currentPuzzle, firstAlgorithmInfo.Algorithms[0]);

                var moves = NotationParser.FormatMoves(firstAlgorithmInfo.Algorithms[0]);
                solutionMoves += moves + " ";
                solutionDescription += firstAlgorithmInfo.Description
                    + Environment.NewLine
                    + moves
                    + Environment.NewLine
                    + Environment.NewLine;
            }

            this.txtSolutionMoves.Text = solutionMoves;
            this.txtSolutionDescription.Text = solutionDescription;

            this.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void findFailureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var currentPuzzle = Rotator.ApplyMoves(
                Puzzle.Solved,
                NotationParser.EnumerateMoves(this.txtScrambleMoves.Text));

            var failureInfo = SolverFailureFinder.FindFailure(WellKnownSolvers.Roux, currentPuzzle);

            var movesText = NotationParser.FormatMoves(failureInfo.Moves);
            if (failureInfo.NoMoreSteps)
            {
                this.txtSolutionDescription.Text = "Solution found.";
            }
            else if (failureInfo.NoMoreAlgorithms)
            {
                this.txtSolutionDescription.Text = "No more algorithms found.";
            }
            else
            {
                throw new InvalidOperationException();
            }

            this.txtSolutionMoves.Text = movesText;

            this.Refresh();
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.txtSolutionMoves.Text = string.Empty;
            this.txtSolutionDescription.Text = string.Empty;

            var stopwatch = Stopwatch.StartNew();
            var solutions = new SolutionSearch(6, SolutionSearch.AllFaceMoves, Check)
                .Search(this.puzzle);
            stopwatch.Stop();

            this.txtSolutionDescription.Text = "Results. Finished in " + stopwatch.ElapsedMilliseconds.ToString() + "ms\r\n" + string.Join(
                Environment.NewLine,
                from solution in solutions
                orderby solution.Count()
                select NotationParser.FormatMoves(solution));

            this.Refresh();

            bool Check(Puzzle puzzle)
                => Checker.SingleColor(puzzle, Puzzle3x3.Location.Left, PuzzleColor.Blue)
                && Checker.Edge(puzzle, Puzzle3x3.Location.LeftDown, PuzzleColor.Blue, PuzzleColor.White);
        }
    }
}