using System.Drawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// <see cref="ISegmentDrawing"/> defines fields and methods for drawing segments
    /// </summary>
    public interface ISegmentDrawing
    {
        /// <summary>
        /// Gets and sets segment
        /// </summary>
        ISegment Segment { get; }

        /// <summary>
        /// Gets and sets point to start drawing
        /// </summary>
        Point StartPoint { get; }

        /// <summary>
        /// Gets and sets point to end drawing
        /// </summary>
        Point EndPoint { get; }

        /// <summary>
        /// Method for drawing a segment
        /// </summary>
        /// <param name="graphics"></param>
        void Draw(Graphics graphics);

        /// <summary>
        /// Get scheme width that contains all elements
        /// </summary>
        /// <returns></returns>
        int GetSchemeWidth();

        /// <summary>
        /// Get scheme height that contains all elements
        /// </summary>
        /// <returns></returns>
        int GetSchemeHeight();
    }
}