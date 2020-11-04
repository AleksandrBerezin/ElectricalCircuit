using System.Drawing;
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
    }
}