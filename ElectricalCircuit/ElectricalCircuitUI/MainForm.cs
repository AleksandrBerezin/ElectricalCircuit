using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ElectricalCircuit;
using ElectricalCircuit.Elements;

namespace ElectricalCircuitUI
{
    //TODO: форма - 900 строк кода. Слишком много, надо делить на несколько классов или контролов
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
            //TODO: логика в конструкторе формы до метода InitializeComponent() чревато исключениями и потерей всей верстки из-за криво открывающегося дизайнера форм
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

            InitializeComponent();
            //TODO: почему нельзя задать свойство через дизайнер, чтобы оно не болталось здесь?
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            FillCircuitsComboBox();
            FillImpedancesDataGridView();
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
        /// Метод, заполняющий список импедансов
        /// </summary>
        private void FillImpedancesDataGridView()
        {
            var selectedItem = (Circuit)CircuitsComboBox.SelectedItem;
            FillFrequenciesColumn();

            if (selectedItem == null || selectedItem.SubSegments.Count == 0)
            {
                return;
            }

            var impedances = selectedItem.CalculateZ(_frequencies);
            for (int i = 0; i < _frequencies.Count; i++)
            {
                var impedance = impedances[i];
                //TODO +-
                ImpedancesDataGridView[1, i].Value = 
                    String.Format($"{impedance.Real} + {impedance.Imaginary:F4}*j Ом");
            }
        }

        /// <summary>
        /// Метод, заполняющий список частот
        /// </summary>
        private void FillFrequenciesColumn()
        {
            _frequencies.Sort();
            ImpedancesDataGridView.Rows.Clear();

            foreach (var frequency in _frequencies)
            {
                ImpedancesDataGridView.Rows.Add(frequency);
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
            var newNode = new SegmentTreeNode(circuit);
            CircuitTreeView.Nodes.Add(newNode);

            foreach (var segment in circuit.SubSegments)
            {
                WriteAllSegmentsInTree(segment, newNode);
            }

            CircuitTreeView.ExpandAll();
        }

        //TODO: что за AllAll в названии?
        //TODO: всю работу с нодами вынести в отдельный вспомогательный класс
        /// <summary>
        /// Метод поиска всех сегментов в цепи, и добавление в TreeView
        /// </summary>
        /// <param name="segment"></param>
        private void WriteAllSegmentsInTree(ISegment segment, SegmentTreeNode node)
        {
            var newNode = new SegmentTreeNode(segment);
            node.Nodes.Add(newNode);
            if (segment.SubSegments == null)
            {
                return;
            }

            foreach (var subSegment in segment.SubSegments)
            {
                WriteAllSegmentsInTree(subSegment, newNode);
            }
        }

        //TODO: отрисовку сразу переноси в другой класс, пусть пока лежит в другом месте - не надо засорять форму
        /// <summary>
        /// Метод для отрисовки элемента
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="name"></param>
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
            //TODO: здесь и далее - куча магических чисел в отрисовке. В будущем надо создавать локальные и константные переменные с понятным именем, что это за число
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
            FillImpedancesDataGridView();
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
                    //TODO: что за кидание исключения самому себе?
                    throw new FormatException();
                }

                _frequencies.Add(frequency);
                NewFrequencyTextBox.Clear();
                NewFrequencyTextBox.BackColor = Color.White;
                FillImpedancesDataGridView();
            }
            catch (FormatException)
            {
                NewFrequencyTextBox.BackColor = Color.LightCoral;
            }
        }

        private void RemoveFrequencyButton_Click(object sender, EventArgs e)
        {
            //TODO
            //if (_frequencies.Count == 0)
            //{
            //    return;
            //}

            //_frequencies.RemoveAt(ImpedancesDataGridView.Selected);
            //FillFrequenciesListBox();
            //FillImpedancesListBox();

            //NewFrequencyTextBox.Clear();
            //NewFrequencyTextBox.BackColor = Color.White;
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
            //TODO: на подумать: а может имеет смысл тип элемента добавить в интерфейс элементов и забирать его напрямую?
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
                circuit.SubSegments.Add(newElement);
                FillCircuitTreeView();
                SelectNodeInTreeView(newElement);

                return;
            }

            var selectedNode = (SegmentTreeNode)CircuitTreeView.SelectedNode;
            var selectedSegment = selectedNode.Segment;
            var parentNode = selectedNode.Parent;
            var indexInParent = selectedNode.Index;
            //TODO: куча дублирования, нет? Так и не понял, чем принципиально отличаются, имхо можно упростить
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

                    circuit.SubSegments[indexInParent] = parallelSegment;
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

                    circuit.SubSegments[indexInParent] = parallelSegment;
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

                    circuit.SubSegments[indexInParent] = parallelSegment;
                }
            }

            ClearElementInfoFields();
            FillCircuitTreeView();
            SelectNodeInTreeView(newElement);
        }

        private void AddSerialButton_Click(object sender, EventArgs e)
        {
            //TODO: Еще кусок дублирования
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
                circuit.SubSegments.Add(newElement);
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
                    circuit.SubSegments.Add(newElement);
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
                    circuit.SubSegments.Add(newElement);
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

                    circuit.SubSegments[indexInParent] = serialSegment;
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
                    //TODO: дублирование с веткой выше, отличаются только конструктором и текстом сообщения
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
               ((SegmentTreeNode)parentNode).Segment.SubSegments[index] = replacingSegment;

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

            var parentSegment = ((SegmentTreeNode)parentNode).Segment;
            var realIndexInSegment = selectedNode.Index;
            parentSegment.SubSegments[realIndexInSegment] = updatedElement;

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

                ((SegmentTreeNode)parentNode).Segment.SubSegments.Remove(selectedSegment);

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
            //TODO Только импедансы
            FillImpedancesDataGridView();
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

        private void ImpedancesDataGridView_SelectionChanged(object sender, EventArgs e)
        {

        }
    }
}