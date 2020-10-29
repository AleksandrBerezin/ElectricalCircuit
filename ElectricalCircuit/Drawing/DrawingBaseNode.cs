using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace Drawing
{
    //TODO: В C# различия между базовым классом и интерфейсом более существенные...
    // ... Сделать интерфейс, а базовый класс должен его реализовывать...
    // ... Менеджер отрисовки должен работать с отрисовщиками преимущественно через интерфейс.
    //TODO: Base ставится в конце имени класса, не по середине
    /// <summary>
    /// Abstract class <see cref="DrawingBaseNode"/> contains info for drawing segments
    /// and provides a drawing method for implementation
    /// </summary>
    public abstract class DrawingBaseNode : TreeNode
    {
        //TODO: неправильное создание базового класса. Базовый класс ничего не должен знать о наследниках. Здесь же содержится много информации или об элементах, или о дочерних сегментах.
        /// <summary>
        /// Standard element width
        /// </summary>
        protected const int ElementWidth = 60;

        //TODO: не должно быть в базовом классе. Если надо, то создавай еще один базовый класс, специально для элементов.
        /// <summary>
        /// Standart element height
        /// </summary>
        protected const int ElementHeight = 50;

        /// <summary>
        /// Standard distance between two elements
        /// </summary>
        protected const int ConnectionLength = 10;

        /// <summary>
        /// Standard pen for segment drawing
        /// </summary>
        protected readonly Pen pen = new Pen(Color.Black, 2);

        /// <summary>
        /// Standard brush for segment drawing
        /// </summary>
        protected readonly Brush brush = new SolidBrush(Color.Black);

        /// <summary>
        /// Standard font for segment drawing
        /// </summary>
        protected readonly Font font = new Font(FontFamily.GenericSansSerif, 8.25F);

        /// <summary>
        /// Gets and sets segment
        /// </summary>
        public ISegment Segment { get; private set; }

        //TODO: информация о дочерних сегментах? В базовом классе её быть не должно
        /// <summary>
        /// Gets and sets count of serial segments for each node
        /// </summary>
        public int SerialSegmentsCount { get; set; }

        //TODO: в итоге, у тебя базовый класс хранит куски ВСЕХ своих наследников. Это неправильно
        /// <summary>
        /// Gets and sets count of parallel segments for each node
        /// </summary>
        public int ParallelSegmentsCount { get; set; }

        /// <summary>
        /// Gets and sets point to start drawing for each node
        /// </summary>
        public Point StartPoint { get; set; } = Point.Empty;

        /// <summary>
        /// Gets and sets point to end drawing for each node
        /// </summary>
        public Point EndPoint { get; set; } = Point.Empty;

        /// <summary>
        /// Create an inctance of <see cref="DrawingBaseNode"/>
        /// </summary>
        /// <param name="segment"></param>
        protected DrawingBaseNode(ISegment segment)
        {
            Segment = segment;
            Text = segment.ToString();
        }

        /// <summary>
        /// Method for drawing a segment
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);

        /// <summary>
        /// Method for drawing a standard connection line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="graphics"></param>
        protected void DrawConnection(Point startPoint, Graphics graphics)
        {
            DrawConnection(startPoint, new Point(startPoint.X + ConnectionLength,
                startPoint.Y), graphics);
        }

        /// <summary>
        /// Method for drawing connection line between two points
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="graphics"></param>
        protected void DrawConnection(Point startPoint, Point endPoint, Graphics graphics)
        {
            graphics.DrawLine(pen, startPoint, endPoint);
        }
    }
}