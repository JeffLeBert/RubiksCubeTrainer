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
            this.txtScramble = new System.Windows.Forms.TextBox();
            this.picPuzzle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblScramble = new System.Windows.Forms.Label();
            this.cmdScramble = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScramble
            // 
            this.txtScramble.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScramble.Location = new System.Drawing.Point(63, 3);
            this.txtScramble.Name = "txtScramble";
            this.txtScramble.Size = new System.Drawing.Size(814, 20);
            this.txtScramble.TabIndex = 0;
            this.txtScramble.TextChanged += new System.EventHandler(this.txtScramble_TextChanged);
            // 
            // picPuzzle
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.picPuzzle, 2);
            this.picPuzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPuzzle.ErrorImage = null;
            this.picPuzzle.InitialImage = null;
            this.picPuzzle.Location = new System.Drawing.Point(63, 32);
            this.picPuzzle.Name = "picPuzzle";
            this.picPuzzle.Size = new System.Drawing.Size(881, 585);
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
            this.tableLayoutPanel1.Controls.Add(this.lblScramble, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtScramble, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.picPuzzle, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmdScramble, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 620);
            this.tableLayoutPanel1.TabIndex = 2;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 620);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Rubik\'s Cube Trainer";
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtScramble;
        private System.Windows.Forms.PictureBox picPuzzle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblScramble;
        private System.Windows.Forms.Button cmdScramble;
    }
}