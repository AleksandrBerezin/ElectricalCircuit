using System.Drawing;
using ElectricalCircuit.Segments;

//TODO: пространства имен поправить во всем проекте во всех классах
namespace Drawing.DrawableSegments
{
    //TODO: Drawing - это процесс, а не объект. Интерфейсы так не именуются, наречие или существительное с прилагательными. По аналогии переименовать все классы от этого интерфейса
    /// <summary>
    /// <see cref="IDrawableSegment"/> defines fields and methods for drawing segments
    /// </summary>
    public interface IDrawableSegment
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