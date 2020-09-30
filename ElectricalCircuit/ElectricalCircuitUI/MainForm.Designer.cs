namespace ElectricalCircuitUI
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.FrequenciesListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.CircuitsListBox = new System.Windows.Forms.ListBox();
            this.ImpedancesListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ElementsListBox = new System.Windows.Forms.ListBox();
            this.CircuitPictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.CurrentValueTextBox = new System.Windows.Forms.TextBox();
            this.NewValueTexBox = new System.Windows.Forms.TextBox();
            this.SaveValueButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CircuitPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.CircuitPictureBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1082, 573);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1082, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ImpedancesListBox, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 326);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1076, 244);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.FrequenciesListBox, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(541, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(263, 238);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // FrequenciesListBox
            // 
            this.FrequenciesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FrequenciesListBox.FormattingEnabled = true;
            this.FrequenciesListBox.ItemHeight = 16;
            this.FrequenciesListBox.Location = new System.Drawing.Point(3, 3);
            this.FrequenciesListBox.Name = "FrequenciesListBox";
            this.FrequenciesListBox.Size = new System.Drawing.Size(257, 113);
            this.FrequenciesListBox.TabIndex = 0;
            this.FrequenciesListBox.SelectedIndexChanged += new System.EventHandler(this.FrequenciesListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.CircuitsListBox, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(263, 238);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // CircuitsListBox
            // 
            this.CircuitsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircuitsListBox.FormattingEnabled = true;
            this.CircuitsListBox.ItemHeight = 16;
            this.CircuitsListBox.Location = new System.Drawing.Point(3, 3);
            this.CircuitsListBox.Name = "CircuitsListBox";
            this.CircuitsListBox.Size = new System.Drawing.Size(257, 113);
            this.CircuitsListBox.TabIndex = 0;
            this.CircuitsListBox.SelectedIndexChanged += new System.EventHandler(this.CircuitsListBox_SelectedIndexChanged);
            // 
            // ImpedancesListBox
            // 
            this.ImpedancesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImpedancesListBox.FormattingEnabled = true;
            this.ImpedancesListBox.ItemHeight = 16;
            this.ImpedancesListBox.Location = new System.Drawing.Point(810, 3);
            this.ImpedancesListBox.Name = "ImpedancesListBox";
            this.ImpedancesListBox.Size = new System.Drawing.Size(263, 238);
            this.ImpedancesListBox.TabIndex = 4;
            this.ImpedancesListBox.SelectedIndexChanged += new System.EventHandler(this.ImpedancesListBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.ElementsListBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(272, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(263, 238);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // ElementsListBox
            // 
            this.ElementsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementsListBox.FormattingEnabled = true;
            this.ElementsListBox.ItemHeight = 16;
            this.ElementsListBox.Location = new System.Drawing.Point(3, 3);
            this.ElementsListBox.Name = "ElementsListBox";
            this.ElementsListBox.Size = new System.Drawing.Size(257, 113);
            this.ElementsListBox.Sorted = true;
            this.ElementsListBox.TabIndex = 0;
            this.ElementsListBox.SelectedIndexChanged += new System.EventHandler(this.ElementsListBox_SelectedIndexChanged);
            // 
            // CircuitPictureBox
            // 
            this.CircuitPictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.CircuitPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircuitPictureBox.Location = new System.Drawing.Point(3, 30);
            this.CircuitPictureBox.Name = "CircuitPictureBox";
            this.CircuitPictureBox.Size = new System.Drawing.Size(1076, 290);
            this.CircuitPictureBox.TabIndex = 2;
            this.CircuitPictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SaveValueButton);
            this.panel1.Controls.Add(this.NewValueTexBox);
            this.panel1.Controls.Add(this.CurrentValueTextBox);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 119);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 119);
            this.panel1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 15);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Current value:";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(3, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 15);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "New value:";
            // 
            // CurrentValueTextBox
            // 
            this.CurrentValueTextBox.Location = new System.Drawing.Point(109, 3);
            this.CurrentValueTextBox.Name = "CurrentValueTextBox";
            this.CurrentValueTextBox.ReadOnly = true;
            this.CurrentValueTextBox.Size = new System.Drawing.Size(151, 22);
            this.CurrentValueTextBox.TabIndex = 2;
            // 
            // NewValueTexBox
            // 
            this.NewValueTexBox.Location = new System.Drawing.Point(109, 37);
            this.NewValueTexBox.Name = "NewValueTexBox";
            this.NewValueTexBox.Size = new System.Drawing.Size(151, 22);
            this.NewValueTexBox.TabIndex = 3;
            // 
            // SaveValueButton
            // 
            this.SaveValueButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveValueButton.Location = new System.Drawing.Point(88, 77);
            this.SaveValueButton.Name = "SaveValueButton";
            this.SaveValueButton.Size = new System.Drawing.Size(75, 36);
            this.SaveValueButton.TabIndex = 4;
            this.SaveValueButton.Text = "Save";
            this.SaveValueButton.UseVisualStyleBackColor = true;
            this.SaveValueButton.Click += new System.EventHandler(this.SaveValueButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 573);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CircuitPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ListBox FrequenciesListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ListBox CircuitsListBox;
        private System.Windows.Forms.ListBox ImpedancesListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.ListBox ElementsListBox;
        private System.Windows.Forms.PictureBox CircuitPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SaveValueButton;
        private System.Windows.Forms.TextBox NewValueTexBox;
        private System.Windows.Forms.TextBox CurrentValueTextBox;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}

