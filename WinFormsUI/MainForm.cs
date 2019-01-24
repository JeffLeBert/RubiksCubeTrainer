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
        }

        private void picPuzzle_Paint(object sender, PaintEventArgs e)
        {
            this.picPuzzle.Invalidate();

            FlatPuzzleRenderer3x3.Draw(e.Graphics, this.puzzle, this.picPuzzle.Size);
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
    }
}