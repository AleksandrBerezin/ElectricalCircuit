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

        /// <summary>
        /// Calculating the count of parallel and serial segments for each node
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        public void CalculateSegmentsCount(DrawingBaseNode startNode)
        {
            while (startNode != null)
            {
                if (startNode.Nodes.Count != 0)
                {
                    CalculateSegmentsCount((DrawingBaseNode)startNode.Nodes[0]);
                }

                //TODO Дублирование
                var segment = startNode.Segment;
                if (segment is IElement)
                {
                    startNode.SerialSegmentsCount = 1;
                    startNode.ParallelSegmentsCount = 1;
                }
                else if (segment is ParallelSegment)
                {
                    var maxSerialCount = 0;
                    foreach (DrawingBaseNode node in startNode.Nodes)
                    {
                        startNode.ParallelSegmentsCount += node.ParallelSegmentsCount;
                        if (node.SerialSegmentsCount > maxSerialCount)
                        {
                            maxSerialCount = node.SerialSegmentsCount;
                        }

                        startNode.SerialSegmentsCount = maxSerialCount;
                    }
                }
                else
                {
                    var maxParallelCount = 0;
                    foreach (DrawingBaseNode node in startNode.Nodes)
                    {
                        startNode.SerialSegmentsCount += node.SerialSegmentsCount;
                        if (node.ParallelSegmentsCount > maxParallelCount)
                        {
                            maxParallelCount = node.ParallelSegmentsCount;
                        }

                        startNode.ParallelSegmentsCount = maxParallelCount;
                    }
                }

                startNode = startNode.NextNode as DrawingBaseNode;
            }
        }
    }
}