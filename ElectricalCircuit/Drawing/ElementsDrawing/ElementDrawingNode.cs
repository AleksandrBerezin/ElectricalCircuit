using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// <see cref="ElementDrawingNode"/> contains algorithm for drawing the element
    /// </summary>
    public class ElementDrawingNode : DrawingBaseNode
    {
        /// <summary>
        /// Standard text height
        /// </summary>
        private const int TextHeight = 15;

        /// <summary>
        /// Create an inctance of <see cref="ElementDrawingNode"/>
        /// </summary>
        /// <param name="segment"></param>
        protected ElementDrawingNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the element
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            var element = (IElement)Segment;
            var contour = new Rectangle(StartPoint.X, StartPoint.Y - ElementHeight / 2,
                ElementWidth, TextHeight);

            var format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            graphics.DrawString(element.Name, font, brush, contour, format);
            DrawConnection(StartPoint, graphics);

            DrawConnection(new Point(EndPoint.X - ConnectionLength,
                EndPoint.Y), graphics);
        }
    }
}