using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RubiksCubeTrainer.Puzzle3x3;
using RubiksCubeTrainer.Solver3x3;
using RubiksCubeTrainer.Solver3x3.Roux;

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
            this.picPuzzle.Invalidate();

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
            var solver = Solver.Create(this.puzzle);
            var solutionMoves = string.Empty;
            var solutionDescription = string.Empty;
            while (solver.CurrentStep != null)
            {
                var firstStep = solver.CurrentStep.GetPossibleSteps().FirstOrDefault();
                if (firstStep == null)
                {
                    this.txtSolutionDescription.Text += "Unable to find solution.";
                    break;
                }

                var moves = NotationParser.FormatMoves(firstStep.Algorithm.Moves);
                solutionMoves += moves + " ";
                solutionDescription += firstStep.Algorithm.Description
                    + Environment.NewLine
                    + moves
                    + Environment.NewLine
                    + Environment.NewLine;

                solver = solver.NextSolver(firstStep);
            }

            this.txtSolutionMoves.Text = solutionMoves;
            this.txtSolutionDescription.Text = solutionDescription;

            this.Refresh();
        }

        private void cmdFindFailure_Click(object sender, EventArgs e)
        {
            var solver = Solver.Create(this.puzzle);
            solver = SolverFailureFinder.FindFailure(solver);

            this.txtScrambleMoves.Text = FormatSolution(solver);
        }

        private string FormatSolution(SolverBase endSolver)
        {
            var builder = new StringBuilder();
            foreach (var solver in endSolver.AncestorSolversAndSelf.Reverse())
            {
            }

            return string.Empty;
        }
    }
}