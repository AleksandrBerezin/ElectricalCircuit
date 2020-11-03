using System;
using System.Drawing;
using System.Windows.Forms;
using Drawing.SegmentsDrawing;
using ElectricalCircuit;

namespace Drawing
{
    /// <summary>
    /// Services class <see cref="DrawingManager"/> for drawing circuit
    /// </summary>
    public static class DrawingManager
    { //TODO: размеры элементов правильнее было бы хранить в абстрактном базовом классе для элементов, а не в менеджере. Почему здесь?
        /// <summary>
        /// Standard element width
        /// </summary>
        public const int ElementWidth = 60;

        /// <summary>
        /// Standart element height
        /// </summary>
        public const int ElementHeight = 50;

        /// <summary>
        /// Standard distance between two elements
        /// </summary>
        public const int ConnectionLength = 10;

        /// <summary>
        /// Method for drawing circuit
        /// </summary>
        public static void DrawCircuit(ISegmentDrawing node, PictureBox picture)
        {
            var bitmap = new Bitmap(node.GetSchemeWidth(), node.GetSchemeHeight());
            var graphics = Graphics.FromImage(bitmap);

            node.Draw(graphics);

            picture.Image = bitmap;
        }

        /// <summary>
        /// Create a tree node based on a segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static SegmentDrawingNodeBase CreateNode(ISegment segment)
        {
            if (segment is Resistor)
            {
                return new ResistorDrawingNode(segment);
            }
            else if (segment is Inductor)
            {
                return new InductorDrawingNode(segment);
            }
            else if (segment is Capacitor)
            {
                return new CapacitorDrawingNode(segment);
            }
            else if (segment is ParallelSegment)
            {
                return new ParallelSegmentDrawingNode(segment);
            }
            else if (segment is SerialSegment)
            {
                return new SerialSegmentDrawingNode(segment);
            }
            else if (segment is Circuit)
            {
                return new CircuitDrawingNode(segment);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}