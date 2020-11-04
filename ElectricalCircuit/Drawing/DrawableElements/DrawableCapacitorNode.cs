using System.Drawing;
using ElectricalCircuit.Segments;

namespace Drawing.DrawableElements
{
    /// <summary>
    /// <see cref="DrawableCapacitorNode"/> contains algorithm for drawing the inductor
    /// </summary>
    public class DrawableCapacitorNode : DrawableElementNodeBase
    {
        /// <summary>
        /// Standard capacitor height from center
        /// </summary>
        private const int HeightFromCenter = 10;

        /// <summary>
        /// Standard capacitor side width
        /// </summary>
        private const int SideWidth = 17;

        /// <summary>
        /// Standard capacitor space in center
        /// </summary>
        private const int CenterSpace = 6;

        /// <summary>
        /// Create an inctance of <see cref="DrawableCapacitorNode"/>
        /// </summary>
        /// <param name="segment"></param>
        public DrawableCapacitorNode(ISegment segment) : base(segment)
        {
        }

        /// <summary>
        /// Method for drawing the capacitor
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            base.Draw(graphics);

            var x = StartPoint.X + DrawingManager.ConnectionLength;
            var y = StartPoint.Y;

            DrawConnection(new Point(x, y), new Point(x + SideWidth, y),
                graphics);
            x += SideWidth;

            DrawConnection(new Point(x, y - HeightFromCenter),
                new Point(x, y + HeightFromCenter), graphics);
            x += CenterSpace;
            DrawConnection(new Point(x, y - HeightFromCenter),
                new Point(x, y + HeightFromCenter), graphics);

            DrawConnection(new Point(x, y), new Point(x + SideWidth, y),
                graphics);
        }
    }
}