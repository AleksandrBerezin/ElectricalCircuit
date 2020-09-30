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

        /// <summary>
        /// Список элементов цепи
        /// </summary>
        private List<IElement> _elements;

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
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            CircuitsListBox.DataSource = _project.Circuits;
            CircuitsListBox.DisplayMember = "Name";

            FrequenciesListBox.DataSource = _frequencies;

            FillImpedancesListBox();

            FillElementsListBox();
        }

        /// <summary>
        /// Метод, заполняющий список импедансов
        /// </summary>
        private void FillImpedancesListBox()
        {
            ImpedancesListBox.DataSource =
                ((Circuit)CircuitsListBox.SelectedItem).CalculateZ(_frequencies);
        }

        /// <summary>
        /// Метод, заполняющий список элементов
        /// </summary>
        private void FillElementsListBox()
        {
            _elements = new List<IElement>();
            foreach (var segment in ((Circuit)CircuitsListBox.SelectedItem).Segments)
            {
                FindAllElements(segment);
            }

            ElementsListBox.DataSource = _elements;
            CurrentValueTextBox.Text = ((IElement)ElementsListBox.SelectedItem).Value.ToString();
        }

        /// <summary>
        /// Метод поиска всех элементов в цепи
        /// </summary>
        /// <param name="segment"></param>
        private void FindAllElements(ISegment segment)
        {
            if (segment is IElement)
            {
                _elements.Add((IElement)segment);
            }
            else
            {
                foreach (var element in segment.SubSegments)
                {
                    FindAllElements(element);
                }
            }
        }

        private void CircuitsListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillImpedancesListBox();
            FillElementsListBox();
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

        private void ElementsListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            CurrentValueTextBox.Text = ((IElement)ElementsListBox.SelectedItem).Value.ToString();
        }

        private void SaveValueButton_Click(object sender, System.EventArgs e)
        {
            if (double.TryParse(NewValueTexBox.Text, out var newValue))
            {
                try
                {
                    ((IElement)ElementsListBox.SelectedItem).Value = newValue;
                }
                catch (ArgumentException)
                {
                    NewValueTexBox.BackColor = Color.LightCoral;
                }
            }
            else
            {
                NewValueTexBox.BackColor = Color.LightCoral;
            }
        }

        /// <summary>
        /// При изменении сегмента идет пересчет импедансов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Segment_SegmentChanged(object sender, EventArgs e)
        {

        }
    }
}
