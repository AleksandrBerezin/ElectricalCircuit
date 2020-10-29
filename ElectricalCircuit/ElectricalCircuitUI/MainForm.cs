using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Drawing;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    //TODO: форма - 900 строк кода. Слишком много, надо делить на несколько классов или контролов
    public partial class MainForm : Form
    {
        /// <summary>
        /// List of frequencies
        /// </summary>
        private readonly List<double> _frequencies;

        public MainForm()
        {
            //TODO: логика в конструкторе формы до метода InitializeComponent() чревато исключениями и потерей всей верстки из-за криво открывающегося дизайнера форм
            InitializeComponent();

            CircuitControl.Project = new Project();
            foreach (var circuit in CircuitControl.Project.Circuits)
            {
                circuit.SegmentChanged += CalculationImpedances;
            }

            _frequencies = new List<double>
            {
                100,
                200,
                300
            };

            CircuitControl.SelectedCircuitChanged += CircuitControl_SelectedCircuitChanged;
            CircuitControl.SelectedSegmentChanged += CircuitControl_SelectedSegmentChanged;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            FillImpedancesTable();
        }

        /// <summary>
        /// Метод, заполняющий таблицу импедансов
        /// </summary>
        private void FillImpedancesTable()
        {
            FillFrequenciesColumn();
            FillImpedancesColumn();
        }

        /// <summary>
        /// Метод, заполняющий колонку частот
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
        /// Метод, заполняющий колонку импедансов
        /// </summary>
        private void FillImpedancesColumn()
        {
            var selectedItem = CircuitControl.SelectedCircuit;
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
                //TODO: что за кидание исключения самому себе?
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
                    $"Please choose 1 frequency",
                    "Remove frequency",
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
        /// Метод очистки полей с информацией об элементе
        /// </summary>
        private void ClearElementInfoFields()
        {
            NameTextBox.Text = "";
            ValueTextBox.Text = "";
            TypeTextBox.Text = "";
        }

        /// <summary>
        /// Получение элемента из ElementForm
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
            //TODO: Еще кусок дублирования
            AddElement(typeof(SerialSegment));
        }

        private void AddElement(Type segmentType)
        {
            var selectedNode = CircuitControl.SelectedNode;
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

            // Выбрана цепь
            if (parentNode == null)
            {
                selectedSegment.SubSegments.Add(newElement);
                CircuitControl.FillCircuitTreeView();
                CircuitControl.SelectNodeInTreeView(newElement);
                return;
            }

            var indexInParent = selectedNode.Index;
            var parentSegment = ((DrawingBaseNode)parentNode).Segment;

            //TODO: куча дублирования, нет? Так и не понял, чем принципиально отличаются, имхо можно упростить
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
            CircuitControl.FillCircuitTreeView();
            CircuitControl.SelectNodeInTreeView(newElement);
        }

        private void EditElementButton_Click(object sender, EventArgs e)
        {
            var selectedNode = CircuitControl.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var parentNode = (DrawingBaseNode)selectedNode.Parent;

            // Выбрана цепь
            if (parentNode == null)
            {
                CircuitControl.EditCircuitButton_Click(sender, e);
                return;
            }

            var selectedSegment = selectedNode.Segment;
            var parentSegment = parentNode.Segment;

            // Выбран сегмент - замена на противоположный сегмент
            if (selectedSegment is IElement == false)
            {
                ISegment replacingSegment;
                var segmentType = selectedSegment.GetType();

                var changeSegment = MessageBox.Show(
                    $@"Do you really want to replace this segment with the opposite?",
                    "Replace Segment",
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
                CircuitControl.FillCircuitTreeView();
                CircuitControl.SelectNodeInTreeView(replacingSegment);

                return;
            }

            // Выбран элемент
            var updatedElement = GetElementFromElementForm((IElement)selectedSegment);
            if (updatedElement == null)
            {
                return;
            }

            var realIndexInSegment = selectedNode.Index;
            parentSegment.SubSegments[realIndexInSegment] = updatedElement;

            ClearElementInfoFields();
            CircuitControl.FillCircuitTreeView();
            CircuitControl.SelectNodeInTreeView(updatedElement);
        }

        private void RemoveElementButton_Click(object sender, EventArgs e)
        {
            var selectedNode = CircuitControl.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }

            var parentNode = (DrawingBaseNode)selectedNode.Parent;

            // Выбрана цепь
            if (parentNode == null)
            {
                CircuitControl.RemoveCircuitButton_Click(sender, e);
                return;
            }

            var result = MessageBox.Show(
                $@"Do you really want to remove this segment: {NameTextBox.Text}",
                "Remove Segment",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                var selectedSegment = selectedNode.Segment;
                parentNode.Segment.SubSegments.Remove(selectedSegment);

                if (parentNode.Nodes.Count == 1)
                {
                    ((DrawingBaseNode)parentNode.Parent)?.Segment.SubSegments.RemoveAt
                        (parentNode.Index);
                }

                ClearElementInfoFields();
                CircuitControl.FillCircuitTreeView();
            }
        }

        /// <summary>
        /// Метод расчета импедансов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculationImpedances(object sender, EventArgs e)
        {
            FillImpedancesColumn();
        }

        private void CircuitControl_SelectedCircuitChanged(object sender, EventArgs e)
        {
            FillImpedancesColumn();
            ClearElementInfoFields();
        }

        private void CircuitControl_SelectedSegmentChanged(object sender, EventArgs e)
        {
            ClearElementInfoFields();

            var selectedNode = CircuitControl.SelectedNode;
            NameTextBox.Text = selectedNode.Text;

            var selectedSegment = ((DrawingBaseNode)selectedNode).Segment;

            if (selectedSegment is IElement)
            {
                var element = (IElement)selectedSegment;
                ValueTextBox.Text = element.Value.ToString();
                TypeTextBox.Text = element.Type.ToString();
            }
        }
    }
}