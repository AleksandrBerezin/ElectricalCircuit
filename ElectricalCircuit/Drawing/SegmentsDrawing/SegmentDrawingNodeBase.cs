using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// Abstract class <see cref="SegmentDrawingNodeBase"/> contains info for drawing segments
    /// and provides a drawing method for implementation
    /// </summary>
    public abstract class SegmentDrawingNodeBase : TreeNode, ISegmentDrawing
    {
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
        public Point StartPoint { get; private set; } = Point.Empty;

        /// <inheritdoc/>
        public Point EndPoint { get; private set; } = Point.Empty;

        /// <summary>
        /// Create an inctance of <see cref="SegmentDrawingNodeBase"/>
        /// </summary>
        /// <param name="segment"></param>
        protected SegmentDrawingNodeBase(ISegment segment)
        {
            Segment = segment;
            Text = segment.ToString();
        }

        /// <inheritdoc/>
        public abstract void Draw(Graphics graphics);

        /// <summary>
        /// Calculating the coordinates for each node
        /// </summary>
        public void CalculateCoordinates()
        {
            var parent = (SegmentDrawingNodeBase)Parent;
            var prevNode = (SegmentDrawingNodeBase)PrevNode;

            // start is Root
            if (parent == null)
            {
                //TODO: менеджер ничего не должен знать про количество параллельных или последовательных сегментов внутри сегмента...
                // ...Он должен забирать у отрисовщика конечные размеры и работать с ними.
                // Padding from border of the PictureBox
                StartPoint = new Point(DrawingManager.ElementWidth,
                    DrawingManager.ElementHeight * (Segment.ParallelSegmentsCount / 2 + 1));
                EndPoint = new Point(GetEndX(), StartPoint.Y);
            }
            //TODO: опять - знания о конкретном типе родителя. Можно сделать так, чтобы менеджер ничего не знал о конкретных типах отрисовщиков
            else if (parent.Segment is ParallelSegment)
            {
                if (parent.Nodes.Count == 1)
                {
                    StartPoint = parent.StartPoint;
                    EndPoint = parent.EndPoint;
                }
                else
                {
                    var startPoint = new Point
                    {
                        // Calculate the start X coordinate taking into account the alignment
                        X = parent.StartPoint.X + (parent.EndPoint.X
                        - parent.StartPoint.X - (Segment.SerialSegmentsCount
                        * (DrawingManager.ElementWidth + DrawingManager.ConnectionLength)
                        - DrawingManager.ConnectionLength)) / 2,
                        Y = Segment.ParallelSegmentsCount * DrawingManager.ElementHeight / 2
                    };

                    if (Index == 0)
                    {
                        startPoint.Y += parent.StartPoint.Y
                            - parent.Segment.ParallelSegmentsCount
                            * DrawingManager.ElementHeight / 2;
                    }
                    else
                    {
                        startPoint.Y += prevNode.StartPoint.Y
                            + prevNode.Segment.ParallelSegmentsCount
                            * DrawingManager.ElementHeight / 2;
                    }

                    StartPoint = startPoint;
                    EndPoint = new Point(GetEndX(), StartPoint.Y);
                }
            }
            else
            {
                if (Index == 0)
                {
                    StartPoint = new Point(parent.StartPoint.X,
                        parent.StartPoint.Y);
                }
                else
                {
                    StartPoint = new Point(prevNode.EndPoint.X + DrawingManager.ConnectionLength,
                        prevNode.EndPoint.Y);
                }

                if (Index == parent.Nodes.Count - 1)
                {
                    EndPoint = new Point(parent.EndPoint.X,
                        parent.EndPoint.Y);
                }
                else
                {
                    EndPoint = new Point(GetEndX(), StartPoint.Y);
                }
            }
        }

        /// <inheritdoc/>
        public int GetSchemeWidth()
        {
            return (Segment.SerialSegmentsCount + 2) * DrawingManager.ElementWidth;
        }

        /// <inheritdoc/>
        public int GetSchemeHeight()
        {
            return (Segment.ParallelSegmentsCount + 3) * DrawingManager.ElementHeight;
        }

        /// <summary>
        /// Returns the end X coordinate of node
        /// </summary>
        /// <returns></returns>
        private int GetEndX()
        {
            //TODO: сделано куча классов, но менеджер все равно содержит специфические для конкретных сегментов методы...
            // ... Менеджер должен просто связывать сегмент с его отрисовщиком, отдавать отрисовщику сегмент и вызывать ...
            // ... у отрисовщика метод рисования. Вся магия рисования должна происходить внутри отрисовщика.
            return StartPoint.X + Segment.SerialSegmentsCount
                * (DrawingManager.ElementWidth + DrawingManager.ConnectionLength)
                - DrawingManager.ConnectionLength;
        }

        /// <summary>
        /// Method for drawing a standard connection line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="graphics"></param>
        protected void DrawConnection(Point startPoint, Graphics graphics)
        {
            DrawConnection(startPoint, new Point(startPoint.X
                + DrawingManager.ConnectionLength, startPoint.Y), graphics);
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