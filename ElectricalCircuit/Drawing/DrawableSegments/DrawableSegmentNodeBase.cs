using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableSegments
{
    /// <summary>
    /// Abstract class <see cref="DrawableSegmentNodeBase"/> contains info for drawing segments
    /// and provides a drawing method for implementation
    /// </summary>
    public abstract class DrawableSegmentNodeBase : TreeNode, IDrawableSegment
    {
        //TODO: размеры элементов правильнее было бы хранить в абстрактном базовом классе для элементов, а не в менеджере. Почему здесь?
        /// <summary>
        /// The width that must be provided to drawing a single element segment
        /// </summary>
        protected const int SegmentWidth = 60;

        /// <summary>
        /// The height that must be provided to drawing a single element segment
        /// </summary>
        protected const int SegmentHeight = 50;

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

        /// <inheritdoc/>
        public ISegment Segment { get; private set; }

        /// <inheritdoc/>
        public Point StartPoint { get; set; } = Point.Empty;

        /// <inheritdoc/>
        public Point EndPoint { get; set; } = Point.Empty;

        /// <summary>
        /// Create an inctance of <see cref="DrawableSegmentNodeBase"/>
        /// </summary>
        /// <param name="segment"></param>
        protected DrawableSegmentNodeBase(ISegment segment)
        {
            Segment = segment;
            Text = segment.ToString();
        }

        /// <inheritdoc/>
        public abstract void Draw(Graphics graphics);

        /// <summary>
        /// Calculating the coordinates for each node
        /// </summary>
        public abstract void CalculateCoordinates();

        /// <inheritdoc/>
        public abstract int GetSchemeWidth();

        /// <inheritdoc/>
        public abstract int GetSchemeHeight();

        /// <summary>
        /// Method for drawing a standard connection line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="graphics"></param>
        protected void DrawConnection(Point startPoint, Graphics graphics)
        {
            DrawConnection(startPoint, new Point(startPoint.X
                + ConnectionLength, startPoint.Y), graphics);
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