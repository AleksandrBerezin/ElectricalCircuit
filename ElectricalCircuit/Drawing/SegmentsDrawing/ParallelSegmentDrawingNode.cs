using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// <see cref="ParallelSegmentDrawingNode"/> contains algorithm for drawing the
    /// serial segment
    /// </summary>
    public class ParallelSegmentDrawingNode : SegmentDrawingNodeBase
    {
        /// <summary>
        /// Create an inctance of <see cref="ParallelSegmentDrawingNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public ParallelSegmentDrawingNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the parallel segment
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            var startX = StartPoint.X;
            var endX = EndPoint.X;

            var leftTopCorner = new Point();
            var leftBottomCorner = new Point();
            var rightTopCorner = new Point();
            var rightBottomCorner = new Point();

            foreach (SegmentDrawingNodeBase node in Nodes)
            {
                node.CalculateCoordinates();

                if (node.Index == 0)
                {
                    leftTopCorner.X = startX;
                    leftTopCorner.Y = node.StartPoint.Y;
                    rightTopCorner.X = endX;
                    rightTopCorner.Y = node.StartPoint.Y;
                }

                if (node.Index == Nodes.Count - 1)
                {
                    leftBottomCorner.X = startX;
                    leftBottomCorner.Y = node.StartPoint.Y;
                    rightBottomCorner.X = endX;
                    rightBottomCorner.Y = node.StartPoint.Y;
                }

                DrawConnection(new Point(startX, node.StartPoint.Y),
                    node.StartPoint, graphics);
                node.Draw(graphics);
                DrawConnection(node.EndPoint,
                    new Point(endX, node.EndPoint.Y), graphics);
            }

            if (Nodes.Count > 1)
            {
                DrawConnection(leftTopCorner, leftBottomCorner, graphics);
                DrawConnection(rightTopCorner, rightBottomCorner, graphics);
            }
        }
    }
}