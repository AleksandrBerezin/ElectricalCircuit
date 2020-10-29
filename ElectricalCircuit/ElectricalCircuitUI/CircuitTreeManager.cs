using System.Drawing;
using System.Windows.Forms;
using Drawing;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    /// <summary>
    /// Services class <see cref="CircuitTreeManager"/> for working with CircuitTreeView
    /// </summary>
    public class CircuitTreeManager
    {
        /// <summary>
        /// Gets and sets tree of circuit segments
        /// </summary>
        public TreeView CircuitTree { get; set; }

        /// <summary>
        /// Write circuit and all subsegments in tree
        /// </summary>
        public void WriteCircuitInTree(Circuit circuit)
        {
            var newNode = DrawingManager.CreateNode(circuit);
            CircuitTree.Nodes.Add(newNode);

            foreach (var segment in circuit.SubSegments)
            {
                WriteAllSegmentsInTree(segment, newNode);
            }

            CircuitTree.ExpandAll();
        }

        //TODO: что за AllAll в названии?
        //TODO: всю работу с нодами вынести в отдельный вспомогательный класс
        /// <summary>
        /// Recursive method that write segment and all this subsegments in tree
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="node"></param>
        private void WriteAllSegmentsInTree(ISegment segment, DrawingBaseNode node)
        {
            var newNode = DrawingManager.CreateNode(segment);;
            node.Nodes.Add(newNode);
            if (segment.SubSegments == null)
            {
                return;
            }

            foreach (var subSegment in segment.SubSegments)
            {
                WriteAllSegmentsInTree(subSegment, newNode);
            }
        }

        /// <summary>
        /// Selecting node in the tree that appropriate to a segment
        /// </summary>
        /// <param name="segment"></param>
        public void SelectNodeInTreeView(ISegment segment)
        {
            CircuitTree.SelectedNode = SearchNode(segment,
                (DrawingBaseNode)CircuitTree.Nodes[0]);
        }

        /// <summary>
        /// Recursuve search item in tree that appropriate to a segment
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private DrawingBaseNode SearchNode(ISegment segment, DrawingBaseNode startNode)
        {
            DrawingBaseNode node = null;
            while (startNode != null)
            {
                if (startNode.Segment.Equals(segment))
                {
                    node = startNode;
                    break;
                }

                if (startNode.Nodes.Count != 0)
                {
                    node = SearchNode(segment, (DrawingBaseNode)startNode.Nodes[0]);
                    if (node != null)
                    {
                        break;
                    }
                }

                startNode = startNode.NextNode as DrawingBaseNode;
            }

            return node;
        }
    }
}