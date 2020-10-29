namespace ElectricalCircuitUI
{
    partial class CircuitControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CircuitGroupBox = new System.Windows.Forms.GroupBox();
            this.EditCircuitButton = new System.Windows.Forms.Button();
            this.RemoveCircuitButton = new System.Windows.Forms.Button();
            this.AddCircuitButton = new System.Windows.Forms.Button();
            this.CircuitTreeView = new System.Windows.Forms.TreeView();
            this.CircuitsComboBox = new System.Windows.Forms.ComboBox();
            this.CircuitGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CircuitGroupBox
            // 
            this.CircuitGroupBox.Controls.Add(this.EditCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.RemoveCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.AddCircuitButton);
            this.CircuitGroupBox.Controls.Add(this.CircuitTreeView);
            this.CircuitGroupBox.Controls.Add(this.CircuitsComboBox);
            this.CircuitGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CircuitGroupBox.Location = new System.Drawing.Point(0, 0);
            this.CircuitGroupBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 6);
            this.CircuitGroupBox.Name = "CircuitGroupBox";
            this.CircuitGroupBox.Size = new System.Drawing.Size(294, 664);
            this.CircuitGroupBox.TabIndex = 3;
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
            this.EditCircuitButton.Location = new System.Drawing.Point(36, 634);
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
            this.RemoveCircuitButton.Location = new System.Drawing.Point(66, 634);
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
            this.AddCircuitButton.Location = new System.Drawing.Point(6, 634);
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
            this.CircuitTreeView.Size = new System.Drawing.Size(282, 571);
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
            // CircuitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CircuitGroupBox);
            this.MinimumSize = new System.Drawing.Size(294, 664);
            this.Name = "CircuitControl";
            this.Size = new System.Drawing.Size(294, 664);
            this.CircuitGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CircuitGroupBox;
        private System.Windows.Forms.Button EditCircuitButton;
        private System.Windows.Forms.Button RemoveCircuitButton;
        private System.Windows.Forms.Button AddCircuitButton;
        private System.Windows.Forms.TreeView CircuitTreeView;
        private System.Windows.Forms.ComboBox CircuitsComboBox;
    }
}
