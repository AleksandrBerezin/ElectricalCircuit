using System.Drawing;
using System.Windows.Forms;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    /// <summary>
    /// Класс <see cref="SegmentTreeNode"/> добавляет в элемент дерева ссылку на
    /// соответствующий сегмент
    /// </summary>
    public class SegmentTreeNode : TreeNode
    {
        /// <summary>
        /// Возвращает и задает сегмент
        /// </summary>
        public ISegment Segment { get; private set; }

        /// <summary>
        /// Возвращает и задает количество последовательных сегментов
        /// </summary>
        public int SerialSegmentsCount { get; set; } = 0;

        /// <summary>
        /// Возвращает и задает количество параллельных сегментов
        /// </summary>
        public int ParallelSegmentsCount { get; set; } = 0;

        /// <summary>
        /// Возвращает и задает точку начала сегмента на графике
        /// </summary>
        public Point StartPoint { get; set; } = Point.Empty;

        /// <summary>
        /// Возвращает и задает точку конца сегмента на графике
        /// </summary>
        public Point EndPoint { get; set; } = Point.Empty;

        public SegmentTreeNode(ISegment segment)
        {
            Segment = segment;
            Text = segment.ToString();
        }
    }
}