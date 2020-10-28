using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// Services class <see cref="DrawingManager"/> for drawing circuit
    /// </summary>
    public static class DrawingManager
    {
        /// <summary>
        /// Standard element width
        /// </summary>
        private const int ElementWidth = 60;

        /// <summary>
        /// Standart element height
        /// </summary>
        private const int ElementHeight = 50;

        /// <summary>
        /// Standard distance between two elements
        /// </summary>
        private const int ConnectionLength = 10;

        /// <summary>
        /// Method for drawing circuit
        /// </summary>
        public static void DrawCircuit(DrawingBaseNode node, PictureBox picture)
        {
            var bitmap = new Bitmap((node.SerialSegmentsCount + 2) * ElementWidth,
                (node.ParallelSegmentsCount + 3) * ElementHeight);
            var graphics = Graphics.FromImage(bitmap);

            CalculateCoordinates(node);
            node.Draw(graphics);
            picture.Image = bitmap;
        }

        /// <summary>
        /// Create a tree node based on a segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static DrawingBaseNode CreateNode(ISegment segment)
        {
            if (segment is Resistor)
            {
                return new ResistorDrawingNode(segment);
            }
            else if (segment is Inductor)
            {
                return new InductorDrawingNode(segment);
            }
            else if (segment is Capacitor)
            {
                return new CapacitorDrawingNode(segment);
            }
            else if (segment is ParallelSegment)
            {
                return new ParallelSegmentDrawingNode(segment);
            }
            else
            {
                return new SerialSegmentDrawingNode(segment);
            }
        }

        //TODO Дублирование
        /// <summary>
        /// Calculating the coordinates for each node
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private static void CalculateCoordinates(DrawingBaseNode startNode)
        {
            while (startNode != null)
            {
                var parent = (DrawingBaseNode)startNode.Parent;

                if (parent == null)
                {
                    startNode.StartPoint = new Point(50,
                        50 + ElementHeight * startNode.ParallelSegmentsCount / 2);

                    var endX = startNode.StartPoint.X + startNode.SerialSegmentsCount
                        * (ElementWidth + ConnectionLength) - ConnectionLength;
                    startNode.EndPoint = new Point(endX, startNode.StartPoint.Y);
                }
                else if (parent.Segment is ParallelSegment)
                {
                    if (parent.Nodes.Count == 1)
                    {
                        startNode.StartPoint = parent.StartPoint;
                        startNode.EndPoint = parent.EndPoint;
                    }
                    else
                    {
                        var startPoint = new Point
                        {
                            X = parent.StartPoint.X + (parent.EndPoint.X
                            - parent.StartPoint.X - (startNode.SerialSegmentsCount
                            * (ElementWidth + ConnectionLength) - ConnectionLength)) / 2
                        };

                        if (startNode.Index == 0)
                        {
                            startPoint.Y = parent.StartPoint.Y - (parent.ParallelSegmentsCount - 1)
                                * ElementHeight / 2;
                        }
                        else
                        {
                            startPoint.Y = ((DrawingBaseNode)startNode.PrevNode).StartPoint.Y 
                                           + startNode.ParallelSegmentsCount * ElementHeight;
                        }

                        startNode.StartPoint = startPoint;

                        var endX = startNode.StartPoint.X + startNode.SerialSegmentsCount
                            * (ElementWidth + ConnectionLength) - ConnectionLength;

                        startNode.EndPoint = new Point(endX, startNode.StartPoint.Y);
                    }
                }
                else
                {
                    if (startNode.Index == 0)
                    {
                        startNode.StartPoint = new Point(parent.StartPoint.X,
                            parent.StartPoint.Y);
                    }
                    else
                    {
                        var preventNode = (DrawingBaseNode)startNode.PrevNode;
                        startNode.StartPoint = new Point(preventNode.EndPoint.X + ConnectionLength,
                            preventNode.EndPoint.Y);
                    }

                    if (startNode.Index == parent.Nodes.Count - 1)
                    {
                        startNode.EndPoint = new Point(parent.EndPoint.X,
                            parent.EndPoint.Y);
                    }
                    else
                    {
                        var endX = startNode.StartPoint.X + startNode.SerialSegmentsCount
                            * (ElementWidth + ConnectionLength) - ConnectionLength;

                        startNode.EndPoint = new Point(endX, startNode.StartPoint.Y);
                    }
                }

                if (startNode.Nodes.Count != 0)
                {
                    CalculateCoordinates((DrawingBaseNode)startNode.Nodes[0]);
                }

                startNode = startNode.NextNode as DrawingBaseNode;
            }
        }
    }
}