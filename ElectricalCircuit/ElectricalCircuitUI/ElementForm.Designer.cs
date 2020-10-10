namespace ElectricalCircuitUI
{
    partial class ElementForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CapacitorRadioButton = new System.Windows.Forms.RadioButton();
            this.InductorRadioButton = new System.Windows.Forms.RadioButton();
            this.ResistorRadioButton = new System.Windows.Forms.RadioButton();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.CancelButton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 135);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CapacitorRadioButton);
            this.panel2.Controls.Add(this.InductorRadioButton);
            this.panel2.Controls.Add(this.ResistorRadioButton);
            this.panel2.Location = new System.Drawing.Point(12, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(139, 28);
            this.panel2.TabIndex = 4;
            // 
            // CapacitorRadioButton
            // 
            this.CapacitorRadioButton.AutoSize = true;
            this.CapacitorRadioButton.Location = new System.Drawing.Point(98, 4);
            this.CapacitorRadioButton.Name = "CapacitorRadioButton";
            this.CapacitorRadioButton.Size = new System.Drawing.Size(38, 21);
            this.CapacitorRadioButton.TabIndex = 2;
            this.CapacitorRadioButton.TabStop = true;
            this.CapacitorRadioButton.Text = "C";
            this.CapacitorRadioButton.UseVisualStyleBackColor = true;
            this.CapacitorRadioButton.CheckedChanged += new System.EventHandler(this.ElementTypeRadioButton_CheckedChanged);
            // 
            // InductorRadioButton
            // 
            this.InductorRadioButton.AutoSize = true;
            this.InductorRadioButton.Location = new System.Drawing.Point(50, 4);
            this.InductorRadioButton.Name = "InductorRadioButton";
            this.InductorRadioButton.Size = new System.Drawing.Size(37, 21);
            this.InductorRadioButton.TabIndex = 1;
            this.InductorRadioButton.TabStop = true;
            this.InductorRadioButton.Text = "L";
            this.InductorRadioButton.UseVisualStyleBackColor = true;
            this.InductorRadioButton.CheckedChanged += new System.EventHandler(this.ElementTypeRadioButton_CheckedChanged);
            // 
            // ResistorRadioButton
            // 
            this.ResistorRadioButton.AutoSize = true;
            this.ResistorRadioButton.Location = new System.Drawing.Point(3, 4);
            this.ResistorRadioButton.Name = "ResistorRadioButton";
            this.ResistorRadioButton.Size = new System.Drawing.Size(39, 21);
            this.ResistorRadioButton.TabIndex = 0;
            this.ResistorRadioButton.TabStop = true;
            this.ResistorRadioButton.Text = "R";
            this.ResistorRadioButton.UseVisualStyleBackColor = true;
            this.ResistorRadioButton.CheckedChanged += new System.EventHandler(this.ElementTypeRadioButton_CheckedChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(248, 96);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(84, 30);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(157, 96);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(85, 30);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.NameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.ValueTextBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(323, 79);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(54, 15);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NameTextBox.Location = new System.Drawing.Point(63, 3);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(257, 22);
            this.NameTextBox.TabIndex = 0;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(3, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(54, 15);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "Value:";
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ValueTextBox.Location = new System.Drawing.Point(63, 42);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(257, 22);
            this.ValueTextBox.TabIndex = 3;
            this.ValueTextBox.TextChanged += new System.EventHandler(this.ValueTextBox_TextChanged);
            // 
            // ElementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 135);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(365, 182);
            this.MinimumSize = new System.Drawing.Size(365, 182);
            this.Name = "ElementForm";
            this.ShowIcon = false;
            this.Text = "Add/Edit Element";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton CapacitorRadioButton;
        private System.Windows.Forms.RadioButton InductorRadioButton;
        private System.Windows.Forms.RadioButton ResistorRadioButton;
    }
}