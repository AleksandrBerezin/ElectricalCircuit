using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    /// <summary>
    /// Services class <see cref="DrawingManager"/> for drawing circuit
    /// </summary>
    public class DrawingManager
    {
        private const int ElementWidth = 60;
        private const int ElementHeight = 50;
        private const int ConnectionLength = 10;

        private readonly Pen _pen = new Pen(Color.Black, 2);
        private readonly Brush _brush = new SolidBrush(Color.Black);
        private Font _font;
        private Graphics _graphics;

        /// <summary>
        /// Gets and sets PictureBox for drawing circuit
        /// </summary>
        public PictureBox Picture { get; set; }

        /// <summary>
        /// Method for drawing circuit
        /// </summary>
        public void DrawCircuit(SegmentTreeNode node)
        {
            var bitmap = new Bitmap(Picture.Width, Picture.Height);
            _graphics = Graphics.FromImage(bitmap);
            _font = Picture.Font;

            CalculateCoordinates(node);
            DrawSerialSegment(node);
            Picture.Image = bitmap;
        }

        /// <summary>
        /// Method for drawing segment
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawSegment(SegmentTreeNode treeNode)
        {
            if (treeNode.Segment is SerialSegment)
            {
                DrawSerialSegment(treeNode);
            }
            else if (treeNode.Segment is ParallelSegment)
            {
                DrawParallelSegment(treeNode);
            }
            else
            {
                DrawElement(treeNode);
            }
        }

        /// <summary>
        /// Method for drawing serial segment
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawSerialSegment(SegmentTreeNode treeNode)
        {
            foreach (SegmentTreeNode node in treeNode.Nodes)
            {
                if (node.Index == 0)
                {
                    DrawConnection(treeNode.StartPoint, node.StartPoint);
                }
                else
                {
                    DrawConnection(((SegmentTreeNode)node.PrevNode).EndPoint,
                        node.StartPoint);
                }

                DrawSegment(node);

                if (node.Index == treeNode.Nodes.Count - 1)
                {
                    DrawConnection(node.EndPoint, treeNode.EndPoint);
                }
            }
        }

        /// <summary>
        /// Method for drawing parallel segment
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawParallelSegment(SegmentTreeNode treeNode)
        {
            var startX = treeNode.StartPoint.X;
            var endX = treeNode.EndPoint.X;

            var leftTopCorner = new Point();
            var leftBottomCorner = new Point();
            var rightTopCorner = new Point();
            var rightBottomCorner = new Point();

            //TODO Дублирование
            foreach (SegmentTreeNode node in treeNode.Nodes)
            {
                if (node.Index == 0)
                {
                    leftTopCorner.X = startX;
                    leftTopCorner.Y = node.StartPoint.Y;
                    rightTopCorner.X = endX;
                    rightTopCorner.Y = node.StartPoint.Y;
                }

                if (node.Index == treeNode.Nodes.Count - 1)
                {
                    leftBottomCorner.X = startX;
                    leftBottomCorner.Y = node.StartPoint.Y;
                    rightBottomCorner.X = endX;
                    rightBottomCorner.Y = node.StartPoint.Y;
                }

                DrawConnection(new Point(startX, node.StartPoint.Y), node.StartPoint);
                DrawSegment(node);
                DrawConnection(node.EndPoint, new Point(endX, node.EndPoint.Y));
            }

            if (treeNode.Nodes.Count > 1)
            {
                DrawConnection(leftTopCorner, leftBottomCorner);
                DrawConnection(rightTopCorner, rightBottomCorner);
            }
        }

        //TODO: отрисовку сразу переноси в другой класс, пусть пока лежит в другом месте - не надо засорять форму
        /// <summary>
        /// Method for drawing element
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawElement(SegmentTreeNode treeNode)
        {
            var element = (IElement)treeNode.Segment;
            var contour = new Rectangle(treeNode.StartPoint.X,
                treeNode.StartPoint.Y - ElementHeight / 2, ElementWidth, 15);

            var format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            _graphics.DrawString(element.Name, _font, _brush, contour, format);
            DrawConnection(treeNode.StartPoint);

            switch (element.Type)
            {
                case ElementType.Resistor:
                {
                    DrawResistor(treeNode);
                    break;
                }
                case ElementType.Inductor:
                {
                    DrawInductor(treeNode);
                    break;
                }
                case ElementType.Capacitor:
                {
                    DrawCapacitor(treeNode);
                    break;
                }
            }

            DrawConnection(new Point(treeNode.EndPoint.X - ConnectionLength,
                treeNode.EndPoint.Y));
        }

        /// <summary>
        /// Method for drawing resistor
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawResistor(SegmentTreeNode treeNode)
        {
            //TODO: здесь и далее - куча магических чисел в отрисовке. В будущем надо создавать локальные и константные переменные с понятным именем, что это за число
            var rectangle = new Rectangle(treeNode.StartPoint.X + ConnectionLength,
                treeNode.StartPoint.Y - 10, 40, 20);
            _graphics.DrawRectangle(_pen, rectangle);
        }

        /// <summary>
        /// Method for drawing inductor
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawInductor(SegmentTreeNode treeNode)
        {
            var x = treeNode.StartPoint.X + ConnectionLength;
            var y = treeNode.StartPoint.Y - 7;

            for (int i = 0; i < 4; i++)
            {
                _graphics.DrawArc(_pen, x, y, 10, 15, 0, -180);
                x += 10;
            }
        }

        /// <summary>
        /// Method for drawing capacitor
        /// </summary>
        /// <param name="treeNode"></param>
        private void DrawCapacitor(SegmentTreeNode treeNode)
        {
            var x = treeNode.StartPoint.X + ConnectionLength;
            var y = treeNode.StartPoint.Y;

            _graphics.DrawLine(_pen, x, y, x + 17, y);
            x += 17;

            _graphics.DrawLine(_pen, x, y - 10, x, y + 10);
            x += 6;
            _graphics.DrawLine(_pen, x, y - 10, x, y + 10);

            _graphics.DrawLine(_pen, x, y, x + 17, y);
        }

        /// <summary>
        /// Method for drawing connection line
        /// </summary>
        /// <param name="startPoint"></param>
        private void DrawConnection(Point startPoint)
        {
            var x = startPoint.X;
            var y = startPoint.Y;
            _graphics.DrawLine(_pen, x, y, x + ConnectionLength, y);
        }

        /// <summary>
        /// Method for drawing connection line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        private void DrawConnection(Point startPoint, Point endPoint)
        {
            _graphics.DrawLine(_pen, startPoint,
                endPoint);
        }

        //TODO Дублирование
        /// <summary>
        /// Calculating the coordinates for each node
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private void CalculateCoordinates(SegmentTreeNode startNode)
        {
            while (startNode != null)
            {
                var parent = (SegmentTreeNode)startNode.Parent;

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
                            startPoint.Y = ((SegmentTreeNode)startNode.PrevNode).StartPoint.Y 
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
                        var preventNode = (SegmentTreeNode)startNode.PrevNode;
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
                    CalculateCoordinates((SegmentTreeNode)startNode.Nodes[0]);
                }

                startNode = startNode.NextNode as SegmentTreeNode;
            }
        }
    }
}