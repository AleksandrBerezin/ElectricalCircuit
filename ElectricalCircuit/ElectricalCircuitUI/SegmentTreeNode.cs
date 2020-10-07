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
        public ISegment Segment { get; set; }

        public SegmentTreeNode(ISegment segment)
        {
            Segment = segment;
            Text = segment.ToString();
        }
    }
}