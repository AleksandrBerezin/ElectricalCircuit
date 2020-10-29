using System;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    public partial class CircuitForm : Form
    {
        /// <summary>
        /// Current circuit
        /// </summary>
        private Circuit _circuit;

        /// <summary>
        /// Returns true if the entered data is correct
        /// </summary>
        private bool _isCorrectData = true;

        /// <summary>
        /// Gets and sets circuit
        /// </summary>
        public Circuit Circuit
        {
            get
            {
                return _circuit;
            }
            set
            {
                _circuit = value;
                NameTextBox.Text = _circuit.Name;
            }
        }

        public CircuitForm()
        {
            InitializeComponent();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Circuit.Name = NameTextBox.Text;
                NameTextBox.BackColor = Color.White;
                _isCorrectData = true;
            }
            catch (ArgumentException)
            {
                NameTextBox.BackColor = Color.LightCoral;
                _isCorrectData = false;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (!_isCorrectData)
            {
                MessageBox.Show(@"Invalid values entered",
                    @"Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}