using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    //TODO: этот класс должен быть абстрактным
    /// <summary>
    /// <see cref="ElementDrawingNodeBase"/> contains algorithm for drawing the element
    /// </summary>
    public abstract class ElementDrawingNodeBase : SegmentDrawingNodeBase
    {
        /// <summary>
        /// Standard text height
        /// </summary>
        private const int TextHeight = 15;

        /// <summary>
        /// Create an inctance of <see cref="ElementDrawingNodeBase"/>
        /// </summary>
        /// <param name="segment"></param>
        protected ElementDrawingNodeBase(ISegment segment) : base(segment)
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
                - DrawingManager.ElementHeight / 2, DrawingManager.ElementWidth, TextHeight);

            var format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            graphics.DrawString(element.Name, font, brush, contour, format);
            DrawConnection(StartPoint, graphics);

            DrawConnection(new Point(EndPoint.X - DrawingManager.ConnectionLength,
                EndPoint.Y), graphics);
        }
    }
}