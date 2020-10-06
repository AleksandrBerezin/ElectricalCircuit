using System;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    public partial class CircuitForm : Form
    {
        /// <summary>
        /// Цепь
        /// </summary>
        private Circuit _circuit;

        /// <summary>
        /// Возвращент и задает цепь
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

        private void NameTextBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                Circuit.Name = NameTextBox.Text;
                NameTextBox.BackColor = Color.White;
            }
            catch (ArgumentException)
            {
                NameTextBox.BackColor = Color.LightCoral;
            }
        }

        private void OKButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}