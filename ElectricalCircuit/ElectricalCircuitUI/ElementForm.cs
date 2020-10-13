using System;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;
using ElectricalCircuit.Elements;

namespace ElectricalCircuitUI
{
    public partial class ElementForm : Form
    {
        /// <summary>
        /// Элемент
        /// </summary>
        private IElement _element;

        /// <summary>
        /// Возвращает и задает элемент
        /// </summary>
        public IElement Element
        {
            get
            {
                return _element;
            }
            set
            {
                _element = value;

                NameTextBox.Text = _element.Name;
                ValueTextBox.Text = _element.Value.ToString();
                SelectElementType();
            }
        }

        public ElementForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод выбора типа элемента 
        /// </summary>
        private void SelectElementType()
        {
            if (Element is Resistor)
            {
                ResistorRadioButton.Checked = true;
            }
            else if (Element is Inductor)
            {
                InductorRadioButton.Checked = true;
            }
            else
            {
                CapacitorRadioButton.Checked = true;
            }
        }

        private void NameTextBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                Element.Name = NameTextBox.Text;
                NameTextBox.BackColor = Color.White;
            }
            catch (ArgumentException)
            {
                NameTextBox.BackColor = Color.LightCoral;
            }
        }

        private void ValueTextBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                Element.Value = double.Parse(ValueTextBox.Text);
                ValueTextBox.BackColor = Color.White;
            }
            catch (ArgumentException)
            {
                ValueTextBox.BackColor = Color.LightCoral;
            }
            catch (FormatException)
            {
                ValueTextBox.BackColor = Color.LightCoral;
            }
        }

        private void ElementTypeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton) sender;

            if (ResistorRadioButton.Checked)
            {
                Element = new Resistor(Element.Name, Element.Value);
            }
            else if (InductorRadioButton.Checked)
            {
                Element = new Inductor(Element.Name, Element.Value);
            }
            else
            {
                Element = new Capacitor(Element.Name, Element.Value);
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Invalid values entered",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            if (ValueTextBox.BackColor == Color.LightCoral)
            {
                MessageBox.Show("Invalid values entered",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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