using System.Drawing;
using ElectricalCircuit;

namespace Drawing.SegmentsDrawing
{
    /// <summary>
    /// <see cref="CircuitDrawingNode"/> contains algorithm for drawing the circuit
    /// </summary>
    public class CircuitDrawingNode : SegmentDrawingNodeBase
    {
        /// <summary>
        /// Create an inctance of <see cref="CircuitDrawingNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public CircuitDrawingNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the circuit
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            CalculateCoordinates();

            foreach (SegmentDrawingNodeBase node in Nodes)
            {
                node.CalculateCoordinates();

                if (node.Index == 0)
                {
                    DrawConnection(StartPoint, node.StartPoint, graphics);
                }
                else
                {
                    DrawConnection(((SegmentDrawingNodeBase)node.PrevNode).EndPoint,
                        node.StartPoint, graphics);
                }

                node.Draw(graphics);

                if (node.Index == Nodes.Count - 1)
                {
                    DrawConnection(node.EndPoint, EndPoint, graphics);
                }
            }
        }
    }
}