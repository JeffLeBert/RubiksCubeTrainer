namespace RubiksCubeTrainer.WinFormsUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtScrambleMoves = new System.Windows.Forms.TextBox();
            this.picPuzzle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtSolutionMoves = new System.Windows.Forms.TextBox();
            this.lblScramble = new System.Windows.Forms.Label();
            this.cmdScramble = new System.Windows.Forms.Button();
            this.txtSolutionDescription = new System.Windows.Forms.TextBox();
            this.cmdSolve = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findFailureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScrambleMoves
            // 
            this.txtScrambleMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScrambleMoves.Location = new System.Drawing.Point(63, 3);
            this.txtScrambleMoves.Name = "txtScrambleMoves";
            this.txtScrambleMoves.Size = new System.Drawing.Size(814, 20);
            this.txtScrambleMoves.TabIndex = 1;
            this.txtScrambleMoves.TextChanged += new System.EventHandler(this.txtScrambleMoves_TextChanged);
            // 
            // picPuzzle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.picPuzzle, 2);
            this.picPuzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPuzzle.ErrorImage = null;
            this.picPuzzle.InitialImage = null;
            this.picPuzzle.Location = new System.Drawing.Point(63, 61);
            this.picPuzzle.Name = "picPuzzle";
            this.picPuzzle.Size = new System.Drawing.Size(881, 576);
            this.picPuzzle.TabIndex = 1;
            this.picPuzzle.TabStop = false;
            this.picPuzzle.Paint += new System.Windows.Forms.PaintEventHandler(this.picPuzzle_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.txtSolutionMoves, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblScramble, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.picPuzzle, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cmdScramble, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSolutionDescription, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtScrambleMoves, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmdSolve, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 786);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // txtSolutionMoves
            // 
            this.txtSolutionMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSolutionMoves.Location = new System.Drawing.Point(63, 32);
            this.txtSolutionMoves.Name = "txtSolutionMoves";
            this.txtSolutionMoves.Size = new System.Drawing.Size(814, 20);
            this.txtSolutionMoves.TabIndex = 3;
            this.txtSolutionMoves.TextChanged += new System.EventHandler(this.txtSolutionMoves_TextChanged);
            // 
            // lblScramble
            // 
            this.lblScramble.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScramble.AutoSize = true;
            this.lblScramble.Location = new System.Drawing.Point(3, 0);
            this.lblScramble.Name = "lblScramble";
            this.lblScramble.Size = new System.Drawing.Size(54, 29);
            this.lblScramble.TabIndex = 0;
            this.lblScramble.Text = "Scramble:";
            this.lblScramble.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmdScramble
            // 
            this.cmdScramble.AutoSize = true;
            this.cmdScramble.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmdScramble.Location = new System.Drawing.Point(883, 3);
            this.cmdScramble.Name = "cmdScramble";
            this.cmdScramble.Size = new System.Drawing.Size(61, 23);
            this.cmdScramble.TabIndex = 2;
            this.cmdScramble.Text = "&Scramble";
            this.cmdScramble.UseVisualStyleBackColor = true;
            this.cmdScramble.Click += new System.EventHandler(this.cmdScramble_Click);
            // 
            // txtSolutionDescription
            // 
            this.txtSolutionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtSolutionDescription, 2);
            this.txtSolutionDescription.Location = new System.Drawing.Point(63, 643);
            this.txtSolutionDescription.Multiline = true;
            this.txtSolutionDescription.Name = "txtSolutionDescription";
            this.txtSolutionDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSolutionDescription.Size = new System.Drawing.Size(881, 140);
            this.txtSolutionDescription.TabIndex = 999;
            // 
            // cmdSolve
            // 
            this.cmdSolve.AutoSize = true;
            this.cmdSolve.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cmdSolve.Location = new System.Drawing.Point(883, 32);
            this.cmdSolve.Name = "cmdSolve";
            this.cmdSolve.Size = new System.Drawing.Size(44, 23);
            this.cmdSolve.TabIndex = 4;
            this.cmdSolve.Text = "Solve";
            this.cmdSolve.UseVisualStyleBackColor = true;
            this.cmdSolve.Click += new System.EventHandler(this.cmdSolve_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(947, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findFailureToolStripMenuItem,
            this.testToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // findFailureToolStripMenuItem
            // 
            this.findFailureToolStripMenuItem.Name = "findFailureToolStripMenuItem";
            this.findFailureToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.findFailureToolStripMenuItem.Text = "&Find Failure";
            this.findFailureToolStripMenuItem.Click += new System.EventHandler(this.findFailureToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.testToolStripMenuItem.Text = "&Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.TestToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 810);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Rubik\'s Cube Trainer";
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtScrambleMoves;
        private System.Windows.Forms.PictureBox picPuzzle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblScramble;
        private System.Windows.Forms.Button cmdScramble;
        private System.Windows.Forms.TextBox txtSolutionDescription;
        private System.Windows.Forms.Button cmdSolve;
        private System.Windows.Forms.TextBox txtSolutionMoves;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findFailureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
    }
}