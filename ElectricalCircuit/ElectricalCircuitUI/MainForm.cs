using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ElectricalCircuit;

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

            _frequencies = new List<double>();
            _frequencies.Add(100);
            _frequencies.Add(200);
            _frequencies.Add(300);

            InitializeComponent();

            tableLayoutPanel3.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel5.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel6.BorderStyle = BorderStyle.FixedSingle;
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            CircuitsComboBox.DataSource = _project.Circuits;
            FillFrequenciesListBox();
            FillCircuitTreeView();
            FillImpedancesListBox();
        }

        /// <summary>
        /// Метод, заполняющий список импедансов
        /// </summary>
        private void FillImpedancesListBox()
        {
            ImpedancesListBox.DataSource =
                ((Circuit)CircuitsComboBox.SelectedItem).CalculateZ(_frequencies);
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
            var newNode = CircuitTreeView.Nodes.Add(CircuitsComboBox.SelectedItem.ToString());
            var circuit = (Circuit)CircuitsComboBox.SelectedItem;

            foreach (var segment in circuit.Segments)
            {
                FindAllSegments(segment, ref newNode);
            }
        }

        /// <summary>
        /// Метод поиска всех сегментов в цепи
        /// </summary>
        /// <param name="segment"></param>
        private void FindAllSegments(ISegment segment, ref TreeNode node)
        {
            var newNode = node.Nodes.Add(segment.ToString());
            if (segment.SubSegments == null)
            {
                return;
            }

            foreach (var subSegment in segment.SubSegments)
            {
                FindAllSegments(subSegment, ref newNode);
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

        /// <summary>
        /// При изменении сегмента идет пересчет импедансов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Segment_SegmentChanged(object sender, EventArgs e)
        {

        }

        private void CircuitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCircuitTreeView();
            FillImpedancesListBox();
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
    }
}
