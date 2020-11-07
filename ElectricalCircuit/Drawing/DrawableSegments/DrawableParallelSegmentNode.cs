﻿using System.Drawing;
using ElectricalCircuit.Elements;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableSegments
{
    /// <summary>
    /// <see cref="DrawableParallelSegmentNode"/> contains algorithm for drawing the
    /// serial segment
    /// </summary>
    public class DrawableParallelSegmentNode : DrawableSegmentNodeBase
    {
        /// <summary>
        /// Create an inctance of <see cref="DrawableParallelSegmentNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public DrawableParallelSegmentNode(ISegment segment) : base(segment)
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

            foreach (DrawableSegmentNodeBase node in Nodes)
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

        /// <inheritdoc/>
        public override void CalculateCoordinates()
        {
            if (Nodes.Count == 1)
            {
                var node = (DrawableSegmentNodeBase)Nodes[0];
                node.StartPoint = StartPoint;
                node.EndPoint = EndPoint;
            }
            else
            {
                foreach (DrawableSegmentNodeBase node in Nodes)
                {
                    var startY = CalculateElementsCountInHeight(node.Segment)
                        * SegmentHeight / 2;
                    var prevNode = (DrawableSegmentNodeBase)node.PrevNode;

                    if (node.Index == 0)
                    {
                        startY += StartPoint.Y - CalculateElementsCountInHeight(Segment)
                            * SegmentHeight / 2;
                    }
                    else
                    {
                        startY += prevNode.StartPoint.Y 
                            + CalculateElementsCountInHeight(prevNode.Segment)
                            * SegmentHeight / 2;
                    }

                    node.StartPoint = new Point(StartPoint.X, startY);
                    node.EndPoint = new Point(EndPoint.X, startY);
                }
            }
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
            return SegmentWidth * 2;
        }

        /// <inheritdoc/>
        public override int GetSchemeHeight()
        {
            return (CalculateElementsCountInHeight(Segment) + 3) * SegmentHeight;
        }
    }
}