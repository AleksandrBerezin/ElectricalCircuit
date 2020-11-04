using System.Drawing;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableElements
{
    /// <summary>
    /// <see cref="DrawableResistorNode"/> contains algorithm for drawing the resistor
    /// </summary>
    public class DrawableResistorNode : DrawableElementNodeBase
    {
        /// <summary>
        /// Standard resistor width
        /// </summary>
        private const int Width = 40;

        /// <summary>
        /// Standard resistor height
        /// </summary>
        private const int Height = 20;

        /// <summary>
        /// Create an inctance of <see cref="DrawableResistorNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public DrawableResistorNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the resistor
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            var rectangle = new Rectangle(StartPoint.X + ConnectionLength,
                StartPoint.Y - Height / 2, Width, Height);
            graphics.DrawRectangle(pen, rectangle);
        }
    }
}