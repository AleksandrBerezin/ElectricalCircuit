using System;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit.Elements;

namespace ElectricalCircuitUI
{
    public partial class ElementForm : Form
    {
        /// <summary>
        /// Current Element
        /// </summary>
        private IElement _element;

        /// <summary>
        /// Returns true if the entered data is correct
        /// </summary>
        private bool _isCorrectData = true;

        /// <summary>
        /// Gets and sets element
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
                TypeComboBox.SelectedItem = _element.Type;
            }
        }

        public ElementForm()
        {
            InitializeComponent();
            TypeComboBox.DataSource = Enum.GetValues(typeof(ElementType));
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Element.Name = NameTextBox.Text;
                NameTextBox.BackColor = Color.White;
                _isCorrectData = true;
            }
            catch (ArgumentException)
            {
                NameTextBox.BackColor = Color.LightCoral;
                _isCorrectData = false;
            }
        }

        private void ValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Element.Value = double.Parse(ValueTextBox.Text);
                ValueTextBox.BackColor = Color.White;
                _isCorrectData = true;
            }
            catch (ArgumentException)
            {
                ValueTextBox.BackColor = Color.LightCoral;
                _isCorrectData = false;
            }
            catch (FormatException)
            {
                ValueTextBox.BackColor = Color.LightCoral;
                _isCorrectData = false;
            }
        }
        
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Element == null)
            {
                return;
            }

            var type = (ElementType)TypeComboBox.SelectedItem;
            if (type == ElementType.Resistor)
            {
                Element = new Resistor(Element.Name, Element.Value);
            }
            else if (type == ElementType.Inductor)
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