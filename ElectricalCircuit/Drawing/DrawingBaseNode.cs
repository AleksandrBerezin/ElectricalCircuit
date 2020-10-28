using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// Abstract class <see cref="DrawingBaseNode"/> contains info for drawing segments
    /// and provides a drawing method for implementation
    /// </summary>
    public abstract class DrawingBaseNode : TreeNode
    {
        /// <summary>
        /// Standard element width
        /// </summary>
        protected const int ElementWidth = 60;

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
        protected Font font = new Font(FontFamily.GenericSansSerif, 8.25F);

        /// <summary>
        /// Gets and sets segment
        /// </summary>
        public ISegment Segment { get; protected set; }

        /// <summary>
        /// Gets and sets count of serial segments for each node
        /// </summary>
        public int SerialSegmentsCount { get; set; } = 0;

        /// <summary>
        /// Gets and sets count of parallel segments for each node
        /// </summary>
        public int ParallelSegmentsCount { get; set; } = 0;

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
        /// Create an inctance of <see cref="DrawingBaseNode"/>
        /// </summary>
        protected DrawingBaseNode()
        {
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