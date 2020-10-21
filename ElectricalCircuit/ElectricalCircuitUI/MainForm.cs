using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    //TODO: форма - 900 строк кода. Слишком много, надо делить на несколько классов или контролов
    public partial class MainForm : Form
    {
        /// <summary>
        /// Проект
        /// </summary>
        private readonly Project _project;

        /// <summary>
        /// Список частот
        /// </summary>
        private readonly List<double> _frequencies;

        /// <summary>
        /// Класс для работы с деревом
        /// </summary>
        private readonly CircuitTreeManager _circuitTreeManager;

        /// <summary>
        /// Класс для отрисовки цепи
        /// </summary>
        private readonly DrawingManager _drawingManager;

        public MainForm()
        {
            //TODO: логика в конструкторе формы до метода InitializeComponent() чревато исключениями и потерей всей верстки из-за криво открывающегося дизайнера форм
            InitializeComponent();

            _project = new Project();
            foreach (var circuit in _project.Circuits)
            {
                circuit.SegmentChanged += CalculationImpedances;
            }

            _frequencies = new List<double>
            {
                100,
                200,
                300
            };

            _circuitTreeManager = new CircuitTreeManager();
            _drawingManager = new DrawingManager();

            _circuitTreeManager.CircuitTree = CircuitTreeView;
            _drawingManager.Picture = SchemaPictureBox;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            FillCircuitsComboBox();
            FillImpedancesTable();
        }

        /// <summary>
        /// Метод, заполняющий выпадающий список цепей
        /// </summary>
        private void FillCircuitsComboBox()
        {
            //TODO: зачем null? А если сразу присвоить нужную коллекцию?
            CircuitsComboBox.DataSource = null;
            CircuitsComboBox.DataSource = _project.Circuits;
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
            var selectedItem = (Circuit)CircuitsComboBox.SelectedItem;
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

        /// <summary>
        /// Метод, заполняющий дерево элементов цепи
        /// </summary>
        private void FillCircuitTreeView()
        {
            CircuitTreeView.Nodes.Clear();

            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var circuit = (Circuit)CircuitsComboBox.SelectedItem;
            _circuitTreeManager.WriteCircuitInTree(circuit);

            CircuitTreeView.ExpandAll();

            //TODO
            _circuitTreeManager.CalculateSegmentsCount((SegmentTreeNode)CircuitTreeView.Nodes[0]);
            _drawingManager.DrawCircuit((SegmentTreeNode)CircuitTreeView.Nodes[0]);
        }

        private void CircuitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCircuitTreeView();
            FillImpedancesColumn();
            ClearElementInfoFields();
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
            //TODO
            if (ImpedancesTable.SelectedCells.Count > 1
                || !ImpedancesTable.SelectedCells.Contains(ImpedancesColumn.CellTemplate))
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

        private void AddCircuitButton_Click(object sender, EventArgs e)
        {
            var inner = new CircuitForm
            {
                Circuit = new Circuit()
            };
            var result = inner.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            var newCircuit = inner.Circuit;
            _project.Circuits.Add(newCircuit);
            FillCircuitsComboBox();
            CircuitsComboBox.SelectedItem = newCircuit;
        }

        private void EditCircuitButton_Click(object sender, EventArgs e)
        {
            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var selectedCircuit = (Circuit)CircuitsComboBox.SelectedItem;
            var realIndexInProject = _project.Circuits.IndexOf(selectedCircuit);

            var inner = new CircuitForm
            {
                Circuit = (Circuit)selectedCircuit.Clone()
            };
            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var updatedCircuit = inner.Circuit;
            _project.Circuits.RemoveAt(realIndexInProject);
            _project.Circuits.Insert(realIndexInProject, updatedCircuit);

            FillCircuitsComboBox();
            CircuitsComboBox.SelectedItem = updatedCircuit;
        }

        private void RemoveCircuitButton_Click(object sender, EventArgs e)
        {
            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var result = MessageBox.Show(
                $"Do you really want to remove this circuit: {CircuitsComboBox.SelectedItem}",
                "Remove Circuit",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                _project.Circuits.RemoveAt(CircuitsComboBox.SelectedIndex);
                FillCircuitsComboBox();

                if (CircuitsComboBox.SelectedItem == null && CircuitsComboBox.Items.Count != 0)
                {
                    CircuitsComboBox.SelectedItem = CircuitsComboBox.Items[0];
                }
            }
        }

        private void CircuitTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearElementInfoFields();

            var selectedNode = CircuitTreeView.SelectedNode;
            NameTextBox.Text = selectedNode.Text;

            var selectedSegment = ((SegmentTreeNode)selectedNode).Segment;

            if (selectedSegment is IElement)
            {
                var element = (IElement)selectedSegment;
                ValueTextBox.Text = element.Value.ToString();
                TypeTextBox.Text = element.Type.ToString();
            }
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
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            var newElement = GetElementFromElementForm(null);
            if (newElement == null)
            {
                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;

            // Выбрана цепь
            if (parentNode == null)
            {
                selectedSegment.SubSegments.Add(newElement);
                FillCircuitTreeView();
                _circuitTreeManager.SelectNodeInTreeView(newElement);
                return;
            }

            var indexInParent = selectedNode.Index;
            var parentSegment = ((SegmentTreeNode)parentNode).Segment;

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
            FillCircuitTreeView();
            _circuitTreeManager.SelectNodeInTreeView(newElement);
        }

        private void EditElementButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var parentNode = (SegmentTreeNode)selectedNode.Parent;

            // Выбрана цепь
            if (parentNode == null)
            {
                EditCircuitButton_Click(sender, e);
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
                    $"Do you really want to replace this segment with the opposite?",
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
                FillCircuitTreeView();
                _circuitTreeManager.SelectNodeInTreeView(replacingSegment);

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
            FillCircuitTreeView();
            _circuitTreeManager.SelectNodeInTreeView(updatedElement);
        }

        private void RemoveElementButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var parentNode = (SegmentTreeNode)selectedNode.Parent;

            // Выбрана цепь
            if (parentNode == null)
            {
                RemoveCircuitButton_Click(sender, e);
                return;
            }

            var result = MessageBox.Show(
                $"Do you really want to remove this segment: {NameTextBox.Text}",
                "Remove Segment",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                var selectedSegment = selectedNode.Segment;
                parentNode.Segment.SubSegments.Remove(selectedSegment);

                ClearElementInfoFields();
                FillCircuitTreeView();
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
    }
}