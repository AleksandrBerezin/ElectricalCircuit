using System.Drawing;
using System.Windows.Forms;
using Drawing;
using ElectricalCircuit;

namespace ElectricalCircuitUI
{
    /// <summary>
    /// User Control for working with circuits
    /// </summary>
    public partial class CircuitControl : UserControl
    {
        /// <summary>
        /// Project
        /// </summary>
        public readonly Project _project;

        public CircuitControl()
        {
            InitializeComponent();

            _project = new Project();
        }

        /// <summary>
        /// Method that fill the combo box of circuits
        /// </summary>
        private void FillCircuitsComboBox()
        {
            //TODO: зачем null? А если сразу присвоить нужную коллекцию?
            CircuitsComboBox.DataSource = null;
            CircuitsComboBox.DataSource = _project.Circuits;
        }

        /// <summary>
        /// Method that fill the tree of circuit elements
        /// </summary>
        private void FillCircuitTreeView()
        {
            CircuitTreeView.Nodes.Clear();

            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var circuit = (Circuit)CircuitsComboBox.SelectedItem;
            WriteCircuitInTree(circuit);
        }

        /// <summary>
        /// Write circuit and all subsegments in tree
        /// </summary>
        private void WriteCircuitInTree(Circuit circuit)
        {
            var newNode = DrawingManager.CreateNode(circuit);
            CircuitTreeView.Nodes.Add(newNode);

            foreach (var segment in circuit.SubSegments)
            {
                WriteAllSegmentsInTree(segment, newNode);
            }

            CircuitTreeView.ExpandAll();
        }

        /// <summary>
        /// Recursive method that write segment and all this subsegments in tree
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="node"></param>
        private void WriteAllSegmentsInTree(ISegment segment, DrawingBaseNode node)
        {
            var newNode = DrawingManager.CreateNode(segment); ;
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
            CircuitTreeView.SelectedNode = SearchNode(segment,
                (DrawingBaseNode)CircuitTreeView.Nodes[0]);
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

        private void CircuitsComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillCircuitTreeView();
        }

        private void CircuitTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void CircuitTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void CircuitTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void CircuitTreeView_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = CircuitTreeView.PointToClient(new Point(e.X, e.Y));
            CircuitTreeView.SelectedNode = CircuitTreeView.GetNodeAt(targetPoint);
        }

        private void CircuitTreeView_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = CircuitTreeView.PointToClient(new Point(e.X, e.Y));
            var targetNode = (DrawingBaseNode)CircuitTreeView.GetNodeAt(targetPoint);
            var draggedNode = (DrawingBaseNode)e.Data.GetData(typeof(DrawingBaseNode));

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                if (targetNode.Segment is IElement == false)
                {
                    ((DrawingBaseNode)draggedNode.Parent).Segment.SubSegments.Remove
                        (draggedNode.Segment);
                    targetNode.Segment.SubSegments.Add(draggedNode.Segment);
                }
                else
                {
                    ((DrawingBaseNode)draggedNode.Parent).Segment.SubSegments.Remove
                        (draggedNode.Segment);

                    var serialSegment = new SerialSegment();
                    serialSegment.SubSegments.Add(targetNode.Segment);
                    serialSegment.SubSegments.Add(draggedNode.Segment);
                    ((DrawingBaseNode)targetNode.Parent).Segment.SubSegments[targetNode.Index] =
                        serialSegment;
                }

                var parent = (DrawingBaseNode)draggedNode.Parent;

                // Delete empty segment
                if (parent.Nodes.Count == 1)
                {
                    ((DrawingBaseNode)parent.Parent)?.Segment.SubSegments.RemoveAt(parent.Index);
                }

                FillCircuitTreeView();
            }
        }

        // Determine whether one node is a parent 
        // or ancestor of a second node.
        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null)
            {
                return false;
            }

            if (node2.Parent.Equals(node1))
            {
                return true;
            }

            return ContainsNode(node1, node2.Parent);
        }

        private void AddCircuitButton_Click(object sender, System.EventArgs e)
        {
            var inner = new CircuitForm
            {
                Circuit = new Circuit()
            };
            var result = inner.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            var newCircuit = inner.Circuit;
            _project.Circuits.Add(newCircuit);
            FillCircuitsComboBox();
            CircuitsComboBox.SelectedItem = newCircuit;
        }

        private void EditCircuitButton_Click(object sender, System.EventArgs e)
        {

        }

        private void RemoveCircuitButton_Click(object sender, System.EventArgs e)
        {

        }
    }
}