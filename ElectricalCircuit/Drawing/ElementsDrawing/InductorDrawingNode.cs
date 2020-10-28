using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// <see cref="InductorDrawingNode"/> contains algorithm for drawing the inductor
    /// </summary>
    public class InductorDrawingNode : ElementDrawingNode
    {
        /// <summary>
        /// Standard inductor arc width
        /// </summary>
        private const int ArcWidth = 10;

        /// <summary>
        /// Standard inductor arc height
        /// </summary>
        private const int ArcHeight = 15;

        /// <summary>
        /// Arct count of inductor
        /// </summary>
        private const int ArcsCount = 4;

        /// <summary>
        /// Create an inctance of <see cref="InductorDrawingNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public InductorDrawingNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the inductor
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            var x = StartPoint.X + ConnectionLength;
            var y = StartPoint.Y - ArcHeight / 2;

            for (int i = 0; i < ArcsCount; i++)
            {
                graphics.DrawArc(pen, x, y, ArcWidth, ArcHeight, 0, -180);
                x += ArcWidth;
            }
        }
    }
}