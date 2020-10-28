using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// <see cref="SerialSegmentDrawingNode"/> contains algorithm for drawing the
    /// serial segment
    /// </summary>
    public class SerialSegmentDrawingNode : DrawingBaseNode
    {
        /// <summary>
        /// Create an inctance of <see cref="SerialSegmentDrawingNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public SerialSegmentDrawingNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the serial segment
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            foreach (DrawingBaseNode node in Nodes)
            {
                if (node.Index == 0)
                {
                    DrawConnection(StartPoint, node.StartPoint, graphics);
                }
                else
                {
                    DrawConnection(((DrawingBaseNode)node.PrevNode).EndPoint,
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