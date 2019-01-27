using System;
using System.Windows.Forms;
using RubiksCubeTrainer.Puzzle3x3;

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

        private void txtScramble_TextChanged(object sender, EventArgs e)
        {
            this.puzzle = Puzzle.Solved;
            try
            {
                foreach (var move in NotationParser.EnumerateMoves(this.txtScramble.Text))
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
        }

        private void cmdScramble_Click(object sender, EventArgs e)
        {
            this.ScramblePuzzle();
        }

        private void ScramblePuzzle()
        {
            this.txtScramble.Text = Scrambler.Scamble(10);
            this.txtScramble.SelectionStart = int.MaxValue;
        }
    }
}