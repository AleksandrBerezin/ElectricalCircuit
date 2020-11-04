using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit.Elements;
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
        public Point StartPoint { get; private set; } = Point.Empty;

        /// <inheritdoc/>
        public Point EndPoint { get; private set; } = Point.Empty;

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
        public void CalculateCoordinates()
        {
            var parent = (DrawableSegmentNodeBase)Parent;
            var prevNode = (DrawableSegmentNodeBase)PrevNode;

            // start is Root
            if (parent == null)
            {
                //TODO: менеджер ничего не должен знать про количество параллельных или последовательных сегментов внутри сегмента...
                // ...Он должен забирать у отрисовщика конечные размеры и работать с ними.
                // Padding from border of the PictureBox
                StartPoint = new Point(SegmentWidth,
                    SegmentHeight * (CalculateParallelElementsCount(Segment) / 2
                    + 1));
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
                        - parent.StartPoint.X - (CalculateSerialElementsCount(Segment)
                        * (SegmentWidth + ConnectionLength)
                        - ConnectionLength)) / 2,
                        Y = CalculateParallelElementsCount(Segment) * SegmentHeight
                            / 2
                    };

                    if (Index == 0)
                    {
                        startPoint.Y += parent.StartPoint.Y
                            - CalculateParallelElementsCount(parent.Segment)
                            * SegmentHeight / 2;
                    }
                    else
                    {
                        startPoint.Y += prevNode.StartPoint.Y
                            + CalculateParallelElementsCount(prevNode.Segment)
                            * SegmentHeight / 2;
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
                    StartPoint = new Point(prevNode.EndPoint.X + ConnectionLength,
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
            return (CalculateSerialElementsCount(Segment) + 2) * SegmentWidth;
        }

        /// <inheritdoc/>
        public int GetSchemeHeight()
        {
            return (CalculateParallelElementsCount(Segment) + 3) * SegmentHeight;
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
            return StartPoint.X + CalculateSerialElementsCount(Segment)
                * (SegmentWidth + ConnectionLength)
                - ConnectionLength;
        }

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

        /// <summary>
        /// Calculating count of serial elements in a segment
        /// </summary>
        /// <param name="startSegment"></param>
        /// <returns></returns>
        private int CalculateSerialElementsCount(ISegment startSegment)
        {
            var serialCount = 0;

            if (startSegment is IElement)
            {
                serialCount = 1;
            }
            else if (startSegment is ParallelSegment)
            {
                var maxSerialCount = 0;

                foreach (var segment in startSegment.SubSegments)
                {
                    var count = CalculateSerialElementsCount(segment);
                    if (count > maxSerialCount)
                    {
                        maxSerialCount = count;
                    }
                }

                serialCount = maxSerialCount;
            }
            else if (startSegment is SerialSegment || startSegment is Circuit)
            {
                foreach (var segment in startSegment.SubSegments)
                {
                    serialCount += CalculateSerialElementsCount(segment);
                }
            }

            return serialCount;
        }

        /// <summary>
        /// Calculating count of parallel elements in a segment
        /// </summary>
        /// <param name="startSegment"></param>
        /// <returns></returns>
        private int CalculateParallelElementsCount(ISegment startSegment)
        {
            var parallelCount = 0;

            if (startSegment is IElement)
            {
                parallelCount = 1;
            }
            else if (startSegment is SerialSegment || startSegment is Circuit)
            {
                var maxParallelCount = 0;

                foreach (var segment in startSegment.SubSegments)
                {
                    var count = CalculateParallelElementsCount(segment);
                    if (count > maxParallelCount)
                    {
                        maxParallelCount = count;
                    }
                }

                parallelCount = maxParallelCount;
            }
            else if (startSegment is ParallelSegment)
            {
                foreach (var segment in startSegment.SubSegments)
                {
                    parallelCount += CalculateParallelElementsCount(segment);
                }
            }

            return parallelCount;
        }
    }
}