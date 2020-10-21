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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SchemaPictureBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.ImpedancesGroupBox = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NewFrequencyTextBox = new System.Windows.Forms.TextBox();
            this.RemoveFrequencyButton = new System.Windows.Forms.Button();
            this.CalculateImpedanceButton = new System.Windows.Forms.Button();
            this.ImpedancesTable = new System.Windows.Forms.DataGridView();
            this.FrequenciesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImpedancesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElementGroupBox = new System.Windows.Forms.GroupBox();
            this.RemoveElementButton = new System.Windows.Forms.Button();
            this.EditElementButton = new System.Windows.Forms.Button();
            this.AddSerialButton = new System.Windows.Forms.Button();
            this.AddParallelButton = new System.Windows.Forms.Button();
            this.TypeTextBox = new System.Windows.Forms.TextBox();
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CircuitGroupBox = new System.Windows.Forms.GroupBox();
            this.EditCircuitButton = new System.Windows.Forms.Button();
            this.RemoveCircuitButton = new System.Windows.Forms.Button();
            this.AddCircuitButton = new System.Windows.Forms.Button();
            this.CircuitTreeView = new System.Windows.Forms.TreeView();
            this.CircuitsComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SchemaPictureBox)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.ImpedancesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImpedancesTable)).BeginInit();
            this.ElementGroupBox.SuspendLayout();
            this.CircuitGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1262, 673);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.CircuitGroupBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1256, 640);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(303, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(950, 634);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.SchemaPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 465);
            this.panel1.TabIndex = 0;
            // 
            // SchemaPictureBox
            // 
            this.SchemaPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SchemaPictureBox.Location = new System.Drawing.Point(0, 0);
            this.SchemaPictureBox.MinimumSize = new System.Drawing.Size(711, 385);
            this.SchemaPictureBox.Name = "SchemaPictureBox";
            this.SchemaPictureBox.Size = new System.Drawing.Size(944, 465);
            this.SchemaPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SchemaPictureBox.TabIndex = 1;
            this.SchemaPictureBox.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 373F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.ImpedancesGroupBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.ElementGroupBox, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 474);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(950, 160);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // ImpedancesGroupBox
            // 
            this.ImpedancesGroupBox.Controls.Add(this.label5);
            this.ImpedancesGroupBox.Controls.Add(this.label4);
            this.ImpedancesGroupBox.Controls.Add(this.NewFrequencyTextBox);
            this.ImpedancesGroupBox.Controls.Add(this.RemoveFrequencyButton);
            this.ImpedancesGroupBox.Controls.Add(this.CalculateImpedanceButton);
            this.ImpedancesGroupBox.Controls.Add(this.ImpedancesTable);
            this.ImpedancesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImpedancesGroupBox.Location = new System.Drawing.Point(376, 3);
            this.ImpedancesGroupBox.Name = "ImpedancesGroupBox";
            this.ImpedancesGroupBox.Size = new System.Drawing.Size(571, 154);
            this.ImpedancesGroupBox.TabIndex = 7;
            this.ImpedancesGroupBox.TabStop = false;
            this.ImpedancesGroupBox.Text = "Impedances Calculation";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Гц";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Frequency:";
            // 
            // NewFrequencyTextBox
            // 
            this.NewFrequencyTextBox.Location = new System.Drawing.Point(91, 45);
            this.NewFrequencyTextBox.Name = "NewFrequencyTextBox";
            this.NewFrequencyTextBox.Size = new System.Drawing.Size(85, 22);
            this.NewFrequencyTextBox.TabIndex = 9;
            // 
            // RemoveFrequencyButton
            // 
            this.RemoveFrequencyButton.Location = new System.Drawing.Point(6, 116);
            this.RemoveFrequencyButton.Name = "RemoveFrequencyButton";
            this.RemoveFrequencyButton.Size = new System.Drawing.Size(200, 32);
            this.RemoveFrequencyButton.TabIndex = 8;
            this.RemoveFrequencyButton.Text = "Remove selected frequency";
            this.RemoveFrequencyButton.UseVisualStyleBackColor = true;
            this.RemoveFrequencyButton.Click += new System.EventHandler(this.RemoveFrequencyButton_Click);
            // 
            // CalculateImpedanceButton
            // 
            this.CalculateImpedanceButton.Location = new System.Drawing.Point(6, 78);
            this.CalculateImpedanceButton.Name = "CalculateImpedanceButton";
            this.CalculateImpedanceButton.Size = new System.Drawing.Size(200, 32);
            this.CalculateImpedanceButton.TabIndex = 7;
            this.CalculateImpedanceButton.Text = "Calculate impedance";
            this.CalculateImpedanceButton.UseVisualStyleBackColor = true;
            this.CalculateImpedanceButton.Click += new System.EventHandler(this.CalculateImpedanceButton_Click);
            // 
            // ImpedancesTable
            // 
            this.ImpedancesTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ImpedancesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ImpedancesTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FrequenciesColumn,
            this.ImpedancesColumn});
            this.ImpedancesTable.Location = new System.Drawing.Point(212, 21);
            this.ImpedancesTable.Name = "ImpedancesTable";
            this.ImpedancesTable.RowHeadersWidth = 25;
            this.ImpedancesTable.RowTemplate.Height = 24;
            this.ImpedancesTable.Size = new System.Drawing.Size(353, 127);
            this.ImpedancesTable.TabIndex = 0;
            // 
            // FrequenciesColumn
            // 
            this.FrequenciesColumn.HeaderText = "Frequencies";
            this.FrequenciesColumn.MinimumWidth = 6;
            this.FrequenciesColumn.Name = "FrequenciesColumn";
            this.FrequenciesColumn.ReadOnly = true;
            this.FrequenciesColumn.Width = 80;
            // 
            // ImpedancesColumn
            // 
            this.ImpedancesColumn.HeaderText = "Impedances";
            this.ImpedancesColumn.MinimumWidth = 6;
            this.ImpedancesColumn.Name = "ImpedancesColumn";
            this.ImpedancesColumn.ReadOnly = true;
            this.ImpedancesColumn.Width = 140;
            // 
            // ElementGroupBox
            // 
            this.ElementGroupBox.Controls.Add(this.RemoveElementButton);
            this.ElementGroupBox.Controls.Add(this.EditElementButton);
            this.ElementGroupBox.Controls.Add(this.AddSerialButton);
            this.ElementGroupBox.Controls.Add(this.AddParallelButton);
            this.ElementGroupBox.Controls.Add(this.TypeTextBox);
            this.ElementGroupBox.Controls.Add(this.ValueTextBox);
            this.ElementGroupBox.Controls.Add(this.NameTextBox);
            this.ElementGroupBox.Controls.Add(this.label3);
            this.ElementGroupBox.Controls.Add(this.label2);
            this.ElementGroupBox.Controls.Add(this.label1);
            this.ElementGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementGroupBox.Location = new System.Drawing.Point(3, 3);
            this.ElementGroupBox.Name = "ElementGroupBox";
            this.ElementGroupBox.Size = new System.Drawing.Size(367, 154);
            this.ElementGroupBox.TabIndex = 6;
            this.ElementGroupBox.TabStop = false;
            this.ElementGroupBox.Text = "Current Element:";
            // 
            // RemoveElementButton
            // 
            this.RemoveElementButton.Location = new System.Drawing.Point(279, 116);
            this.RemoveElementButton.Name = "RemoveElementButton";
            this.RemoveElementButton.Size = new System.Drawing.Size(82, 32);
            this.RemoveElementButton.TabIndex = 23;
            this.RemoveElementButton.Text = "Remove";
            this.RemoveElementButton.UseVisualStyleBackColor = true;
            this.RemoveElementButton.Click += new System.EventHandler(this.RemoveElementButton_Click);
            // 
            // EditElementButton
            // 
            this.EditElementButton.Location = new System.Drawing.Point(191, 116);
            this.EditElementButton.Name = "EditElementButton";
            this.EditElementButton.Size = new System.Drawing.Size(82, 32);
            this.EditElementButton.TabIndex = 22;
            this.EditElementButton.Text = "Edit";
            this.EditElementButton.UseVisualStyleBackColor = true;
            this.EditElementButton.Click += new System.EventHandler(this.EditElementButton_Click);
            // 
            // AddSerialButton
            // 
            this.AddSerialButton.Location = new System.Drawing.Point(191, 40);
            this.AddSerialButton.Name = "AddSerialButton";
            this.AddSerialButton.Size = new System.Drawing.Size(170, 32);
            this.AddSerialButton.TabIndex = 20;
            this.AddSerialButton.Text = "Add serial element";
            this.AddSerialButton.UseVisualStyleBackColor = true;
            this.AddSerialButton.Click += new System.EventHandler(this.AddSerialButton_Click);
            // 
            // AddParallelButton
            // 
            this.AddParallelButton.Location = new System.Drawing.Point(191, 78);
            this.AddParallelButton.Name = "AddParallelButton";
            this.AddParallelButton.Size = new System.Drawing.Size(170, 32);
            this.AddParallelButton.TabIndex = 19;
            this.AddParallelButton.Text = "Add parallel element";
            this.AddParallelButton.UseVisualStyleBackColor = true;
            this.AddParallelButton.Click += new System.EventHandler(this.AddParallelButton_Click);
            // 
            // TypeTextBox
            // 
            this.TypeTextBox.Location = new System.Drawing.Point(55, 121);
            this.TypeTextBox.Name = "TypeTextBox";
            this.TypeTextBox.ReadOnly = true;
            this.TypeTextBox.Size = new System.Drawing.Size(130, 22);
            this.TypeTextBox.TabIndex = 18;
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(55, 83);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.ReadOnly = true;
            this.ValueTextBox.Size = new System.Drawing.Size(130, 22);
            this.ValueTextBox.TabIndex = 17;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(55, 45);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(130, 22);
            this.NameTextBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // CircuitGroupBox
            // 
            this.CircuitGroupBox.Controls.Add(this.EditCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.RemoveCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.AddCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.CircuitTreeView);
            this.CircuitGroupBox.Controls.Add(this.CircuitsComboBox);
            this.CircuitGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircuitGroupBox.Location = new System.Drawing.Point(3, 3);
            this.CircuitGroupBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.CircuitGroupBox.Name = "CircuitGroupBox";
            this.CircuitGroupBox.Size = new System.Drawing.Size(294, 631);
            this.CircuitGroupBox.TabIndex = 2;
            this.CircuitGroupBox.TabStop = false;
            this.CircuitGroupBox.Text = "Current Circuit:";
            // 
            // EditCircuitButton
            // 
            this.EditCircuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditCircuitButton.BackgroundImage = global::ElectricalCircuitUI.Properties.Resources.EditImage;
            this.EditCircuitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.EditCircuitButton.FlatAppearance.BorderSize = 0;
            this.EditCircuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EditCircuitButton.Location = new System.Drawing.Point(36, 601);
            this.EditCircuitButton.Name = "EditCircuitButton";
            this.EditCircuitButton.Size = new System.Drawing.Size(24, 24);
            this.EditCircuitButton.TabIndex = 9;
            this.EditCircuitButton.UseVisualStyleBackColor = true;
            this.EditCircuitButton.Click += new System.EventHandler(this.EditCircuitButton_Click);
            // 
            // RemoveCircuitButton
            // 
            this.RemoveCircuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveCircuitButton.BackgroundImage = global::ElectricalCircuitUI.Properties.Resources.RemoveImage;
            this.RemoveCircuitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RemoveCircuitButton.FlatAppearance.BorderSize = 0;
            this.RemoveCircuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveCircuitButton.Location = new System.Drawing.Point(66, 601);
            this.RemoveCircuitButton.Name = "RemoveCircuitButton";
            this.RemoveCircuitButton.Size = new System.Drawing.Size(24, 24);
            this.RemoveCircuitButton.TabIndex = 8;
            this.RemoveCircuitButton.UseVisualStyleBackColor = true;
            this.RemoveCircuitButton.Click += new System.EventHandler(this.RemoveCircuitButton_Click);
            // 
            // AddCircuitButton
            // 
            this.AddCircuitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddCircuitButton.BackgroundImage = global::ElectricalCircuitUI.Properties.Resources.AddImage;
            this.AddCircuitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.AddCircuitButton.FlatAppearance.BorderSize = 0;
            this.AddCircuitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCircuitButton.Location = new System.Drawing.Point(6, 601);
            this.AddCircuitButton.Name = "AddCircuitButton";
            this.AddCircuitButton.Size = new System.Drawing.Size(24, 24);
            this.AddCircuitButton.TabIndex = 7;
            this.AddCircuitButton.UseVisualStyleBackColor = true;
            this.AddCircuitButton.Click += new System.EventHandler(this.AddCircuitButton_Click);
            // 
            // CircuitTreeView
            // 
            this.CircuitTreeView.AllowDrop = true;
            this.CircuitTreeView.Location = new System.Drawing.Point(6, 51);
            this.CircuitTreeView.Name = "CircuitTreeView";
            this.CircuitTreeView.Size = new System.Drawing.Size(282, 544);
            this.CircuitTreeView.TabIndex = 4;
            this.CircuitTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.CircuitTreeView_ItemDrag);
            this.CircuitTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CircuitTreeView_AfterSelect);
            this.CircuitTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.CircuitTreeView_DragDrop);
            this.CircuitTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.CircuitTreeView_DragEnter);
            this.CircuitTreeView.DragOver += new System.Windows.Forms.DragEventHandler(this.CircuitTreeView_DragOver);
            // 
            // CircuitsComboBox
            // 
            this.CircuitsComboBox.FormattingEnabled = true;
            this.CircuitsComboBox.Location = new System.Drawing.Point(6, 21);
            this.CircuitsComboBox.Name = "CircuitsComboBox";
            this.CircuitsComboBox.Size = new System.Drawing.Size(282, 24);
            this.CircuitsComboBox.TabIndex = 3;
            this.CircuitsComboBox.SelectedIndexChanged += new System.EventHandler(this.CircuitsComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ElectricalCircuit";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SchemaPictureBox)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ImpedancesGroupBox.ResumeLayout(false);
            this.ImpedancesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImpedancesTable)).EndInit();
            this.ElementGroupBox.ResumeLayout(false);
            this.ElementGroupBox.PerformLayout();
            this.CircuitGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox SchemaPictureBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox ElementGroupBox;
        private System.Windows.Forms.Button RemoveElementButton;
        private System.Windows.Forms.Button EditElementButton;
        private System.Windows.Forms.Button AddSerialButton;
        private System.Windows.Forms.Button AddParallelButton;
        private System.Windows.Forms.TextBox TypeTextBox;
        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox ImpedancesGroupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NewFrequencyTextBox;
        private System.Windows.Forms.Button RemoveFrequencyButton;
        private System.Windows.Forms.Button CalculateImpedanceButton;
        private System.Windows.Forms.DataGridView ImpedancesTable;
        private System.Windows.Forms.GroupBox CircuitGroupBox;
        private System.Windows.Forms.ComboBox CircuitsComboBox;
        private System.Windows.Forms.TreeView CircuitTreeView;
        private System.Windows.Forms.Button EditCircuitButton;
        private System.Windows.Forms.Button RemoveCircuitButton;
        private System.Windows.Forms.Button AddCircuitButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrequenciesColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImpedancesColumn;
    }
}

