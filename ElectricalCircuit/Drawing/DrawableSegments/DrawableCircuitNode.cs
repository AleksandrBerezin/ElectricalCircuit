using System.Drawing;
using ElectricalCircuit.Elements;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableSegments
{
    /// <summary>
    /// <see cref="DrawableCircuitNode"/> contains algorithm for drawing the circuit
    /// </summary>
    public class DrawableCircuitNode : DrawableSegmentNodeBase
    {
        /// <summary>
        /// Create an inctance of <see cref="DrawableCircuitNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public DrawableCircuitNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the circuit
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            CalculateCoordinates();

            foreach (DrawableSegmentNodeBase node in Nodes)
            {
                node.CalculateCoordinates();

                if (node.Index == 0)
                {
                    DrawConnection(StartPoint, node.StartPoint, graphics);
                }
                else
                {
                    DrawConnection(((DrawableSegmentNodeBase)node.PrevNode).EndPoint,
                        node.StartPoint, graphics);
                }

                node.Draw(graphics);

                if (node.Index == Nodes.Count - 1)
                {
                    DrawConnection(node.EndPoint, EndPoint, graphics);
                }
            }
        }

        /// <inheritdoc/>
        public override void CalculateCoordinates()
        {
            StartPoint = new Point
            {
                X = SegmentWidth,
                Y = SegmentHeight * (CalculateElementsCountInHeight(Segment) / 2 + 1)
            };

            EndPoint = new Point(GetEndX(this), StartPoint.Y);

            foreach (DrawableSegmentNodeBase node in Nodes)
            {
                if (node.Index == 0)
                {
                    node.StartPoint = new Point(StartPoint.X, StartPoint.Y);
                }
                else
                {
                    var prevNode = (DrawableSegmentNodeBase)node.PrevNode;
                    node.StartPoint = new Point(prevNode.EndPoint.X + ConnectionLength,
                        prevNode.EndPoint.Y);
                }

                if (node.Index == Nodes.Count - 1)
                {
                    node.EndPoint = new Point(EndPoint.X, EndPoint.Y);
                }
                else
                {
                    node.EndPoint = new Point(GetEndX(node), node.StartPoint.Y);
                }
            }
        }

        /// <summary>
        /// Returns end X coordinate for node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int GetEndX(DrawableSegmentNodeBase node)
        {
            return node.StartPoint.X + CalculateElementsCountInWidth(node.Segment) 
                   * (SegmentWidth + ConnectionLength)
                   - ConnectionLength;
        }

        /// <summary>
        /// Calculating count of the elements in width
        /// </summary>
        /// <param name="startSegment"></param>
        /// <returns></returns>
        private int CalculateElementsCountInWidth(ISegment startSegment)
        {
            var elementsCount = 0;

            if (startSegment is IElement)
            {
                elementsCount = 1;
            }
            else if (startSegment is ParallelSegment)
            {
                var maxCount = 0;

                foreach (var segment in startSegment.SubSegments)
                {
                    var count = CalculateElementsCountInWidth(segment);
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                }

                elementsCount = maxCount;
            }
            else if (startSegment is SerialSegment || startSegment is Circuit)
            {
                foreach (var segment in startSegment.SubSegments)
                {
                    elementsCount += CalculateElementsCountInWidth(segment);
                }
            }

            return elementsCount;
        }

        /// <summary>
        /// Calculating count of the elements in height
        /// </summary>
        /// <param name="startSegment"></param>
        /// <returns></returns>
        private int CalculateElementsCountInHeight(ISegment startSegment)
        {
            var elementsCount = 0;

            if (startSegment is IElement)
            {
                elementsCount = 1;
            }
            else if (startSegment is SerialSegment || startSegment is Circuit)
            {
                var maxCount = 0;

                foreach (var segment in startSegment.SubSegments)
                {
                    var count = CalculateElementsCountInHeight(segment);
                    if (count > maxCount)
                    {
                        maxCount = count;
                    }
                }

                elementsCount = maxCount;
            }
            else if (startSegment is ParallelSegment)
            {
                foreach (var segment in startSegment.SubSegments)
                {
                    elementsCount += CalculateElementsCountInHeight(segment);
                }
            }

            return elementsCount;
        }

        /// <inheritdoc/>
        public override int GetSchemeWidth()
        {
            return (CalculateElementsCountInWidth(Segment) + 2) * SegmentWidth;
        }

        /// <inheritdoc/>
        public override int GetSchemeHeight()
        {
            return (CalculateElementsCountInHeight(Segment) + 3) * SegmentHeight;
        }
    }
}