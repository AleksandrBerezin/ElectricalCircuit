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
                Y = SegmentHeight * (CalculateElementsCountInHeight() / 2 + 1)
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
            return node.StartPoint.X + node.CalculateElementsCountInWidth() 
                   * (SegmentWidth + ConnectionLength)
                   - ConnectionLength;
        }

        /// <inheritdoc/>
        public override int GetSchemeWidth()
        {
            var width = 0;
            foreach (DrawableSegmentNodeBase node in Nodes)
            {
                width += node.GetSchemeWidth();
            }

            return width + SegmentWidth * 2;
        }

        /// <inheritdoc/>
        public override int GetSchemeHeight()
        {
            var maxHeight = SegmentHeight;
            foreach (DrawableSegmentNodeBase node in Nodes)
            {
                var currentHeight = node.GetSchemeHeight();
                if (currentHeight > maxHeight)
                {
                    maxHeight = currentHeight;
                }
            }

            return maxHeight + SegmentHeight * 3;
        }

        /// <inheritdoc/>
        public override int CalculateElementsCountInWidth()
        {
            return base.CalculateElementsCountInWidth() - 2;
        }

        /// <inheritdoc/>
        public override int CalculateElementsCountInHeight()
        {
            return base.CalculateElementsCountInHeight() - 3;
        }
    }
}