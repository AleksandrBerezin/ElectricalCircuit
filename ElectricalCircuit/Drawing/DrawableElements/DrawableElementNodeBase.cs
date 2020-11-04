using System.Drawing;
using Drawing.DrawableSegments;
using ElectricalCircuit.Elements;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableElements
{
    /// <summary>
    /// <see cref="DrawableElementNodeBase"/> contains algorithm for drawing the element
    /// </summary>
    public abstract class DrawableElementNodeBase : DrawableSegmentNodeBase
    {
        /// <summary>
        /// Standard text height
        /// </summary>
        private const int TextHeight = 15;

        /// <summary>
        /// Create an inctance of <see cref="DrawableElementNodeBase"/>
        /// </summary>
        /// <param name="segment"></param>
        protected DrawableElementNodeBase(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the element
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            var element = (IElement)Segment;
            var contour = new Rectangle(StartPoint.X, StartPoint.Y
                - SegmentHeight / 2, SegmentWidth, TextHeight);

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