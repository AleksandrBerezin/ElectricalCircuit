using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ElectricalCircuit;
using ElectricalCircuit.Elements;

namespace ElectricalCircuitUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Проект
        /// </summary>
        private Project _project;

        /// <summary>
        /// Список частот
        /// </summary>
        private List<double> _frequencies;
        
        private int _x = 50;
        private int _y = 150;

        public MainForm()
        {
            _project = new Project();

            foreach (var circuit in _project.Circuits)
            {
                circuit.CircuitChanged += CalculationImpedances;
            }

            _frequencies = new List<double>
            {
                100,
                200,
                300
            };

            InitializeComponent();

            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            FillCircuitsComboBox();
            FillFrequenciesListBox();
        }

        /// <summary>
        /// Метод, заполняющий выпадающий список цепей
        /// </summary>
        private void FillCircuitsComboBox()
        {
            CircuitsComboBox.DataSource = null;
            CircuitsComboBox.DataSource = _project.Circuits;
        }

        /// <summary>
        /// Метод, заполняющий список импедансов
        /// </summary>
        private void FillImpedancesListBox()
        {
            var selectedItem = (Circuit)CircuitsComboBox.SelectedItem;

            if (selectedItem == null || selectedItem.Segments.Count == 0)
            {
                ImpedancesListBox.DataSource = null;
                return;
            }

            ImpedancesListBox.DataSource =
                selectedItem.CalculateZ(_frequencies);
        }

        /// <summary>
        /// Метод, заполняющий список частот
        /// </summary>
        private void FillFrequenciesListBox()
        {
            _frequencies.Sort();
            FrequenciesListBox.DataSource = null;
            FrequenciesListBox.DataSource = _frequencies;
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

            var newNode = CircuitTreeView.Nodes.Add(CircuitsComboBox.SelectedItem.ToString());
            var circuit = (Circuit)CircuitsComboBox.SelectedItem;

            foreach (var segment in circuit.Segments)
            {
                WriteAllAllSegmentsInTree(segment, newNode);
            }

            CircuitTreeView.ExpandAll();
        }

        /// <summary>
        /// Метод поиска всех сегментов в цепи, и добавление в TreeView
        /// </summary>
        /// <param name="segment"></param>
        private void WriteAllAllSegmentsInTree(ISegment segment, TreeNode node)
        {
            var newNode = new SegmentTreeNode(segment);
            node.Nodes.Add(newNode);
            if (segment.SubSegments == null)
            {
                return;
            }

            foreach (var subSegment in segment.SubSegments)
            {
                WriteAllAllSegmentsInTree(subSegment, newNode);
            }
        }

        private void FrequenciesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ImpedancesListBox.SelectedIndex = FrequenciesListBox.SelectedIndex;
        }

        private void ImpedancesListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FrequenciesListBox.SelectedIndex = ImpedancesListBox.SelectedIndex;
        }

        /// <summary>
        /// Метод для отрисовки элемента
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name=""></param>
        private void DrawElement(Graphics graphics, string name)
        {

        }

        /// <summary>
        /// Метод для отрисовки резистора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="name"></param>
        private void DrawResistor(Graphics graphics, string name)
        {
            var pen = new Pen(Color.Black, 2);
            var contour = new Rectangle(_x, _y, _x + 40, _y + 40);
            var brush = new SolidBrush(Color.Black);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };
            
            graphics.DrawString(name, Font, brush, contour, format);

            var rectangle = new Rectangle(_x, _y + 10, 40, 20);
            graphics.DrawRectangle(pen, rectangle);
            _x += 40;
        }

        /// <summary>
        /// Метод для отрисовки катушки индуктивности
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="name"></param>
        private void DrawInductor(Graphics graphics, string name)
        {
            var pen = new Pen(Color.Black, 2);
            var contour = new Rectangle(_x, _y, _x + 40, _y + 40);
            var brush = new SolidBrush(Color.Black);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };
            
            graphics.DrawString(name, Font, brush, contour, format);

            for (int i = 0; i < 4; i++)
            {
                graphics.DrawArc(pen, _x, _y + 12, 10, 15, 0, -180);
                _x += 10;
            }
        }

        /// <summary>
        /// Метод для отрисовки конденсатора
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="name"></param>
        private void DrawCapacitor(Graphics graphics, string name)
        {
            var pen = new Pen(Color.Black, 2);
            var contour = new Rectangle(_x, _y, _x + 40, _y + 40);
            var brush = new SolidBrush(Color.Black);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };
            
            graphics.DrawString(name, Font, brush, contour, format);

            graphics.DrawLine(pen, _x, _y + 20, _x + 17, _y + 20);
            _x += 17;

            graphics.DrawLine(pen, _x, _y, _x, _y + 30);
            _x += 6;
            graphics.DrawLine(pen, _x, _y, _x, _y + 30);

            graphics.DrawLine(pen, _x, _y + 20, _x + 17, _y + 20);
            _x += 17;
        }

        private void DrawConnection(Graphics graphics)
        {
            var pen = new Pen(Color.Black, 2);
            graphics.DrawLine(pen, _x, _y + 20, _x + 10, _y + 20);
            _x += 10;
        }

        private void DrawParallelConnection()
        {

        }

        private void CircuitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCircuitTreeView();
            FillImpedancesListBox();
            ClearElementInfoFields();
        }

        private void CalculateImpedanceButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NewFrequencyTextBox.Text))
                {
                    return;
                }

                var frequency = Convert.ToDouble(NewFrequencyTextBox.Text);
                if (_frequencies.Contains(frequency))
                {
                    throw new FormatException();
                }

                _frequencies.Add(frequency);
                NewFrequencyTextBox.Clear();
                NewFrequencyTextBox.BackColor = Color.White;
                FillFrequenciesListBox();
                FillImpedancesListBox();
            }
            catch (FormatException)
            {
                NewFrequencyTextBox.BackColor = Color.LightCoral;
            }
        }

        private void RemoveFrequencyButton_Click(object sender, EventArgs e)
        {
            if (_frequencies.Count == 0)
            {
                return;
            }

            _frequencies.RemoveAt(FrequenciesListBox.SelectedIndex);
            FillFrequenciesListBox();
            FillImpedancesListBox();

            NewFrequencyTextBox.Clear();
            NewFrequencyTextBox.BackColor = Color.White;
        }

        private void NewCircuitButton_Click(object sender, EventArgs e)
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

            DialogResult result = MessageBox.Show(
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

            if (selectedNode is SegmentTreeNode)
            {
                var selectedSegment = ((SegmentTreeNode) selectedNode).Segment;

                if (selectedSegment is IElement)
                {
                    var element = (IElement)selectedSegment;
                    ValueTextBox.Text = element.Value.ToString();
                    TypeTextBox.Text = GetElementType(element).ToString();
                }
            }
        }

        /// <summary>
        /// Метод получения типа элемента
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private ElementType GetElementType(IElement element)
        {
            if (element is Resistor)
            {
                return ElementType.Resistor;
            }
            else if (element is Inductor)
            {
                return ElementType.Inductor;
            }
            else
            {
                return ElementType.Capacitor;
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

        private void AddParallelButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            var inner = new ElementForm
            {
                Element = new Resistor()
            };
            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var newElement = inner.Element;
            var circuit = (Circuit)CircuitsComboBox.SelectedItem;

            // Выбрана цепь
            if (CircuitTreeView.SelectedNode is SegmentTreeNode == false)
            {
                circuit.Segments.Add(newElement);
                FillCircuitTreeView();
                SelectNodeInTreeView(newElement);

                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;
            var indexInParent = selectedNode.Index;

            // Выбран последовательный сегмент
            if (selectedSegment is SerialSegment)
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        var parallelSegment = new ParallelSegment();
                        parallelSegment.SubSegments.Add(selectedSegment);
                        parallelSegment.SubSegments.Add(newElement);

                        parentSegment.SubSegments[indexInParent] = parallelSegment;
                    }
                    else
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                }
                // Родитель является корневым узлом
                else
                {
                    var parallelSegment = new ParallelSegment();
                    parallelSegment.SubSegments.Add(selectedSegment);
                    parallelSegment.SubSegments.Add(newElement);

                    circuit.Segments[indexInParent] = parallelSegment;
                }
            }
            // Выбран параллельный сегмент
            else if (selectedSegment is ParallelSegment)
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        var parallelSegment = new ParallelSegment();
                        parallelSegment.SubSegments.Add(selectedSegment);
                        parallelSegment.SubSegments.Add(newElement);

                        parentSegment.SubSegments[indexInParent] = parallelSegment;
                    }
                    else
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                }
                // Родитель является корневым узлом
                else
                {
                    var parallelSegment = new ParallelSegment();
                    parallelSegment.SubSegments.Add(selectedSegment);
                    parallelSegment.SubSegments.Add(newElement);

                    circuit.Segments[indexInParent] = parallelSegment;
                }
            }
            // Выбран элемент
            else
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        var parallelSegment = new ParallelSegment();
                        parallelSegment.SubSegments.Add(selectedSegment);
                        parallelSegment.SubSegments.Add(newElement);

                        parentSegment.SubSegments[indexInParent] = parallelSegment;
                    }
                    else
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                }
                else
                {
                    var parallelSegment = new ParallelSegment();
                    parallelSegment.SubSegments.Add(selectedSegment);
                    parallelSegment.SubSegments.Add(newElement);

                    circuit.Segments[indexInParent] = parallelSegment;
                }
            }

            ClearElementInfoFields();
            FillCircuitTreeView();
            SelectNodeInTreeView(newElement);
        }

        private void AddSerialButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            var inner = new ElementForm
            {
                Element = new Resistor()
            };
            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var newElement = inner.Element;
            var circuit = (Circuit)CircuitsComboBox.SelectedItem;

            // Выбрана цепь
            if (CircuitTreeView.SelectedNode is SegmentTreeNode == false)
            {
                circuit.Segments.Add(newElement);
                FillCircuitTreeView();
                SelectNodeInTreeView(newElement);

                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;
            var indexInParent = selectedNode.Index;

            // Выбран последовательный сегмент
            if (selectedSegment is SerialSegment)
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                    else
                    {
                        var serialSegment = new SerialSegment();
                        serialSegment.SubSegments.Add(selectedSegment);
                        serialSegment.SubSegments.Add(newElement);

                        parentSegment.SubSegments[indexInParent] = serialSegment;
                    }
                }
                // Родитель является корневым узлом
                else
                {
                    circuit.Segments.Add(newElement);
                }
            }
            // Выбран параллельный сегмент
            else if (selectedSegment is ParallelSegment)
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                    else
                    {
                        var serialSegment = new SerialSegment();
                        serialSegment.SubSegments.Add(selectedSegment);
                        serialSegment.SubSegments.Add(newElement);
                        
                        parentSegment.SubSegments[indexInParent] = serialSegment;
                    }
                }
                // Родитель является корневым узлом
                else
                {
                    circuit.Segments.Add(newElement);
                }
            }
            // Выбран элемент
            else
            {
                // Родитель не является корневым узлом
                if (parentNode is SegmentTreeNode)
                {
                    var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                    if (parentSegment is SerialSegment)
                    {
                        parentSegment.SubSegments.Add(newElement);
                    }
                    else
                    {
                        var serialSegment = new SerialSegment();
                        serialSegment.SubSegments.Add(selectedSegment);
                        serialSegment.SubSegments.Add(newElement);

                        parentSegment.SubSegments[indexInParent] = serialSegment;
                    }
                }
                else
                {
                    var serialSegment = new SerialSegment();
                    serialSegment.SubSegments.Add(selectedSegment);
                    serialSegment.SubSegments.Add(newElement);

                    circuit.Segments[indexInParent] = serialSegment;
                }
            }

            ClearElementInfoFields();
            FillCircuitTreeView();
            SelectNodeInTreeView(newElement);
        }

        private void EditElementButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            // Выбрана цепь
            if (CircuitTreeView.SelectedNode is SegmentTreeNode == false)
            {
                EditCircuitButton_Click(sender, e);
                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;

            // Выбран сегмент - замена на противоположный сегмент
            if (selectedSegment is ElementBase == false)
            {
                ISegment replacingSegment;
                if (selectedSegment is SerialSegment)
                {
                    DialogResult changeSegment = MessageBox.Show(
                        $"Do you really want to replace this serial segment with parallel segment?",
                        "Replace Segment",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    replacingSegment = new ParallelSegment();
                    foreach (var segment in selectedSegment.SubSegments)
                    {
                        replacingSegment.SubSegments.Add(segment);
                    }
                }
                else
                {
                    DialogResult changeSegment = MessageBox.Show(
                        $"Do you really want to replace this parallel segment with serial segment?",
                        "Replace Segment",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    replacingSegment = new SerialSegment();
                    foreach (var segment in selectedSegment.SubSegments)
                    {
                        replacingSegment.SubSegments.Add(segment);
                    }
                }

                var index = selectedNode.Index;
                if (parentNode is SegmentTreeNode)
                {
                    ((SegmentTreeNode)parentNode).Segment.SubSegments[index] = replacingSegment;
                }
                else
                {
                    ((Circuit)CircuitsComboBox.SelectedItem).Segments[index] = replacingSegment;
                }

                ClearElementInfoFields();
                FillCircuitTreeView();
                SelectNodeInTreeView(replacingSegment);

                return;
            }

            // Выбран элемент
            var inner = new ElementForm
            {
                Element = (IElement)selectedSegment.Clone()
            };
            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var updatedElement = inner.Element;
            var realIndexInSegment = 0;

            if (parentNode is SegmentTreeNode)
            {
                var parentSegment = ((SegmentTreeNode)parentNode).Segment;
                realIndexInSegment = selectedNode.Index;
                parentSegment.SubSegments[realIndexInSegment] = updatedElement;
            }
            // parentNode является корневым узлом
            else
            {
                var selectedCircuit = (Circuit)CircuitsComboBox.SelectedItem;
                realIndexInSegment = selectedNode.Index;
                selectedCircuit.Segments[realIndexInSegment] = updatedElement;
            }

            ClearElementInfoFields();
            FillCircuitTreeView();
            SelectNodeInTreeView(updatedElement);
        }

        private void RemoveElementButton_Click(object sender, EventArgs e)
        {
            if (CircuitTreeView.SelectedNode == null)
            {
                return;
            }

            // Выбрана цепь
            if (CircuitTreeView.SelectedNode is SegmentTreeNode == false)
            {
                RemoveCircuitButton_Click(sender, e);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Do you really want to remove this segment: {NameTextBox.Text}",
                "Remove Segment",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
                var selectedSegment = selectedNode.Segment;
                var parentNode = selectedNode.Parent;

                if (parentNode is SegmentTreeNode)
                {
                    ((SegmentTreeNode)parentNode).Segment.SubSegments.Remove(selectedSegment);
                }
                else
                {
                    ((Circuit)CircuitsComboBox.SelectedItem).Segments.Remove(selectedSegment);
                }

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
            FillImpedancesListBox();
        }

        /// <summary>
        /// Метод выбора элемента в CircuitTreeView
        /// </summary>
        /// <param name="segment"></param>
        private void SelectNodeInTreeView(ISegment segment)
        {
            SegmentTreeNode node = null;
            CircuitTreeView.SelectedNode = SearchNode(segment,
                (SegmentTreeNode)CircuitTreeView.Nodes[0].Nodes[0]);
        }

        /// <summary>
        /// Рекурсивный поиск элемента в дереве, соответствующий нужному <see cref="segment"/>
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private SegmentTreeNode SearchNode(ISegment segment, SegmentTreeNode startNode)
        {
            SegmentTreeNode node = null;
            while (startNode != null)
            {
                if (startNode.Segment.Equals(segment))
                {
                    node = startNode;
                    break;
                }

                if (startNode.Nodes.Count != 0)
                {
                    node = SearchNode(segment, (SegmentTreeNode)startNode.Nodes[0]);
                    if (node != null)
                    {
                        break;
                    }
                }

                startNode = startNode.NextNode as SegmentTreeNode;
            }

            return node;
        }
    }
}