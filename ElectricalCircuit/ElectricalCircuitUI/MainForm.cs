using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Drawing.DrawableSegments;
using ElectricalCircuit.Elements;
using ElectricalCircuit.Segments;

namespace ElectricalCircuitUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// List of frequencies
        /// </summary>
        private readonly List<double> _frequencies;

        public MainForm()
        {
            InitializeComponent();

            _frequencies = new List<double>
            {
                100,
                200,
                300
            };

            CircuitInfo.SelectedCircuitChanged += CircuitInfo_SelectedCircuitChanged;
            CircuitInfo.SelectedSegmentChanged += CircuitInfo_SelectedSegmentChanged;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            foreach (var circuit in CircuitInfo.project.Circuits)
            {
                circuit.SegmentChanged += CalculationImpedances;
            }

            FillImpedancesTable();
        }

        /// <summary>
        /// Impedance table filling method
        /// </summary>
        private void FillImpedancesTable()
        {
            FillFrequenciesColumn();
            FillImpedancesColumn();
        }

        /// <summary>
        /// Frequency column filling method
        /// </summary>
        private void FillFrequenciesColumn()
        {
            _frequencies.Sort();
            ImpedancesTable.Rows.Clear();

            foreach (var frequency in _frequencies)
            {
                ImpedancesTable.Rows.Add(frequency);
            }
        }

        /// <summary>
        /// Impedance column filling method
        /// </summary>
        private void FillImpedancesColumn()
        {
            var selectedItem = CircuitInfo.SelectedCircuit;
            if (selectedItem == null || selectedItem.SubSegments.Count == 0)
            {
                return;
            }

            var impedances = selectedItem.CalculateZ(_frequencies);
            for (var i = 0; i < _frequencies.Count; i++)
            {
                var impedance = impedances[i];
                string imaginary = string.Format($"{impedance.Imaginary:F4}");
                if (impedance.Imaginary < 0)
                {
                    imaginary = imaginary.Substring(1);
                    imaginary = "- " + imaginary;
                }
                else
                {
                    imaginary = "+ " + imaginary;
                }

                ImpedancesTable[1, i].Value =
                    string.Format($"{impedance.Real:F4} {imaginary}*j Ом");
            }
        }

        private void CalculateImpedanceButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NewFrequencyTextBox.Text))
            {
                return;
            }

            var frequency = Convert.ToDouble(NewFrequencyTextBox.Text);
            if (_frequencies.Contains(frequency))
            {
                NewFrequencyTextBox.BackColor = Color.LightCoral;
                return;
            }

            _frequencies.Add(frequency);
            NewFrequencyTextBox.Clear();
            NewFrequencyTextBox.BackColor = Color.White;
            FillImpedancesTable();
        }

        private void RemoveFrequencyButton_Click(object sender, EventArgs e)
        {
            if (ImpedancesTable.SelectedCells.Count > 1
                || ImpedancesTable.SelectedCells[0].RowIndex >= _frequencies.Count
                || ImpedancesTable.SelectedCells[0].ColumnIndex != 0)
            {
                MessageBox.Show(
                    $@"Please choose 1 frequency",
                    @"Remove frequency",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_frequencies.Count == 0)
            {
                return;
            }

            var selectedFrequency = int.Parse(ImpedancesTable.SelectedCells[0].Value.ToString());

            _frequencies.Remove(selectedFrequency);
            FillImpedancesTable();

            NewFrequencyTextBox.Clear();
            NewFrequencyTextBox.BackColor = Color.White;
        }

        /// <summary>
        /// Method for clearing element info fields
        /// </summary>
        private void ClearElementInfoFields()
        {
            NameTextBox.Text = "";
            ValueTextBox.Text = "";
            TypeTextBox.Text = "";
        }

        /// <summary>
        /// Gets element from ElementForm
        /// </summary>
        /// <param name="originalElement"></param>
        /// <returns></returns>
        private IElement GetElementFromElementForm(IElement originalElement)
        {
            var inner = new ElementForm();
            if (originalElement == null)
            {
                inner.Element = new Resistor();
            }
            else
            {
                inner.Element = originalElement;
            }

            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return null;
            }

            return inner.Element;
        }

        private void AddParallelButton_Click(object sender, EventArgs e)
        {
            AddElement(typeof(ParallelSegment));
        }

        private void AddSerialButton_Click(object sender, EventArgs e)
        {
            AddElement(typeof(SerialSegment));
        }

        private void AddElement(Type segmentType)
        {
            var selectedNode = CircuitInfo.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var newElement = GetElementFromElementForm(null);
            if (newElement == null)
            {
                return;
            }

            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;

            // Circuit selected
            if (parentNode == null)
            {
                selectedSegment.SubSegments.Add(newElement);
                CircuitInfo.FillCircuitTreeView();
                CircuitInfo.SelectNodeInTreeView(newElement);
                return;
            }

            var indexInParent = selectedNode.Index;
            var parentSegment = ((DrawableSegmentNodeBase)parentNode).Segment;

            if (parentSegment.GetType() == segmentType)
            {
                parentSegment.SubSegments.Add(newElement);
            }
            else
            {
                var parallelSegment = (ISegment)Activator.CreateInstance(segmentType);
                parallelSegment.SubSegments.Add(selectedSegment);
                parallelSegment.SubSegments.Add(newElement);

                parentSegment.SubSegments[indexInParent] = parallelSegment;
            }

            ClearElementInfoFields();
            CircuitInfo.FillCircuitTreeView();
            CircuitInfo.SelectNodeInTreeView(newElement);
        }

        private void EditElementButton_Click(object sender, EventArgs e)
        {
            var selectedNode = CircuitInfo.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var parentNode = (DrawableSegmentNodeBase)selectedNode.Parent;

            // Circuit selected
            if (parentNode == null)
            {
                CircuitInfo.EditCircuitButton_Click(sender, e);
                return;
            }

            var selectedSegment = selectedNode.Segment;
            var parentSegment = parentNode.Segment;

            // Selected segment - replace with opposite segment
            if (selectedSegment is IElement == false)
            {
                ISegment replacingSegment;
                var segmentType = selectedSegment.GetType();

                var changeSegment = MessageBox.Show(
                    $@"Do you really want to replace this segment with the opposite?",
                    @"Replace Segment",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (changeSegment != DialogResult.OK)
                {
                    return;
                }

                if (segmentType.ToString().Contains("SerialSegment"))
                {
                    replacingSegment = new ParallelSegment();
                }
                else
                {
                    replacingSegment = new SerialSegment();
                }

                foreach (var segment in selectedSegment.SubSegments)
                {
                    replacingSegment.SubSegments.Add(segment);
                }

                var index = selectedNode.Index;
                parentSegment.SubSegments[index] = replacingSegment;

                ClearElementInfoFields();
                CircuitInfo.FillCircuitTreeView();
                CircuitInfo.SelectNodeInTreeView(replacingSegment);

                return;
            }

            // Element selected
            var updatedElement = GetElementFromElementForm((IElement)selectedSegment);
            if (updatedElement == null)
            {
                return;
            }

            var realIndexInSegment = selectedNode.Index;
            parentSegment.SubSegments[realIndexInSegment] = updatedElement;

            ClearElementInfoFields();
            CircuitInfo.FillCircuitTreeView();
            CircuitInfo.SelectNodeInTreeView(updatedElement);
        }

        private void RemoveElementButton_Click(object sender, EventArgs e)
        {
            var selectedNode = CircuitInfo.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var parentNode = (DrawableSegmentNodeBase)selectedNode.Parent;

            // Circuit selected
            if (parentNode == null)
            {
                CircuitInfo.RemoveCircuitButton_Click(sender, e);
                return;
            }

            var result = MessageBox.Show(
                $@"Do you really want to remove this segment: {NameTextBox.Text}",
                @"Remove Segment",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                var selectedSegment = selectedNode.Segment;
                parentNode.Segment.SubSegments.Remove(selectedSegment);

                if (parentNode.Nodes.Count == 1)
                {
                    ((DrawableSegmentNodeBase)parentNode.Parent)?.Segment.SubSegments.RemoveAt
                        (parentNode.Index);
                }

                ClearElementInfoFields();
                CircuitInfo.FillCircuitTreeView();
            }
        }

        /// <summary>
        /// Method of impedance calculation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationImpedances(object sender, EventArgs e)
        {
            FillImpedancesColumn();
        }

        private void CircuitInfo_SelectedCircuitChanged(object sender, EventArgs e)
        {
            FillImpedancesColumn();
            ClearElementInfoFields();
        }

        private void CircuitInfo_SelectedSegmentChanged(object sender, EventArgs e)
        {
            ClearElementInfoFields();

            var selectedNode = CircuitInfo.SelectedNode;
            NameTextBox.Text = selectedNode.Text;

            var selectedSegment = ((DrawableSegmentNodeBase)selectedNode).Segment;

            if (selectedSegment is IElement)
            {
                var element = (IElement)selectedSegment;
                ValueTextBox.Text = element.Value.ToString();
                TypeTextBox.Text = element.Type.ToString();
            }
        }
    }
}