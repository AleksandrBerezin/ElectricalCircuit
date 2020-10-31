using System;
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
        /// Root node of tree view
        /// </summary>
        private SegmentDrawingNodeBase _rootNode;

        /// <summary>
        /// Dragged node
        /// </summary>
        private SegmentDrawingNodeBase _draggedNode;

        /// <summary>
        /// Current project
        /// </summary>
        public Project project;

        /// <summary>
        /// Gets and sets selected circuit
        /// </summary>
        public Circuit SelectedCircuit { get; private set; }

        /// <summary>
        /// Gets and sets selected node
        /// </summary>
        public SegmentDrawingNodeBase SelectedNode { get; private set; }

        /// <summary>
        /// Picture box in which the circuit is drawn
        /// </summary>
        public PictureBox PictureBox { get; set; }

        public CircuitControl()
        {
            InitializeComponent();

            Load += CircuitControl_Load;
        }

        /// <summary>
        /// Selected circuit has changed in combo box
        /// </summary>
        public event EventHandler SelectedCircuitChanged;

        /// <summary>
        /// Selected segment has changed in tree view
        /// </summary>
        public event TreeViewEventHandler SelectedSegmentChanged;

        private void CircuitControl_Load(object sender, EventArgs e)
        {
            CircuitsComboBox.SelectedIndexChanged += SelectedCircuitChanged;
            CircuitTreeView.AfterSelect += SelectedSegmentChanged;

            project = new Project();

            FillCircuitsComboBox();
        }

        /// <summary>
        /// Method that fill the combo box of circuits
        /// </summary>
        private void FillCircuitsComboBox()
        {
            //TODO: зачем null? А если сразу присвоить нужную коллекцию? - Тогда коллекция не обновится
            CircuitsComboBox.SelectedText = "";
            CircuitsComboBox.DataSource = null;
            CircuitsComboBox.DataSource = project.Circuits;
        }

        /// <summary>
        /// Method that fill the tree of circuit elements
        /// </summary>
        public void FillCircuitTreeView()
        {
            CircuitTreeView.Nodes.Clear();

            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var circuit = (Circuit)CircuitsComboBox.SelectedItem;
            WriteCircuitInTree(circuit);
            _rootNode = (SegmentDrawingNodeBase)CircuitTreeView.Nodes[0];
            DrawCircuit();
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
        private void WriteAllSegmentsInTree(ISegment segment, SegmentDrawingNodeBase node)
        {
            var newNode = DrawingManager.CreateNode(segment);
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
                (SegmentDrawingNodeBase)CircuitTreeView.Nodes[0]);
        }

        /// <summary>
        /// Recursuve search item in tree that appropriate to a segment
        /// </summary>
        /// <param name="segment"></param>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private SegmentDrawingNodeBase SearchNode(ISegment segment, SegmentDrawingNodeBase startNode)
        {
            SegmentDrawingNodeBase node = null;
            while (startNode != null)
            {
                if (startNode.Segment.Equals(segment))
                {
                    node = startNode;
                    break;
                }

                if (startNode.Nodes.Count != 0)
                {
                    node = SearchNode(segment, (SegmentDrawingNodeBase)startNode.Nodes[0]);
                    if (node != null)
                    {
                        break;
                    }
                }

                startNode = startNode.NextNode as SegmentDrawingNodeBase;
            }

            return node;
        }

        private void CircuitsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillCircuitTreeView();
            _rootNode = (SegmentDrawingNodeBase)CircuitTreeView.Nodes[0];
            SelectedCircuit = (Circuit)CircuitsComboBox.SelectedItem;
        }

        private void CircuitTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedNode = (SegmentDrawingNodeBase)CircuitTreeView.SelectedNode;
        }

        private void CircuitTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _draggedNode = (SegmentDrawingNodeBase)e.Item;
            DoDragDrop(e.Item, DragDropEffects.All);
        }

        private void CircuitTreeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void CircuitTreeView_DragOver(object sender, DragEventArgs e)
        {
            var targetPoint = CircuitTreeView.PointToClient(new Point(e.X, e.Y));
            CircuitTreeView.SelectedNode = CircuitTreeView.GetNodeAt(targetPoint);
        }

        private void CircuitTreeView_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = CircuitTreeView.PointToClient(new Point(e.X, e.Y));
            var targetNode = (SegmentDrawingNodeBase)CircuitTreeView.GetNodeAt(targetPoint);
            var draggedNode = _draggedNode;

            // Confirm that the node at the drop location is not 
            // the dragged node or a descendant of the dragged node.
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                if (targetNode.Segment is IElement == false)
                {
                    ((SegmentDrawingNodeBase)draggedNode.Parent).Segment.SubSegments.Remove
                        (draggedNode.Segment);
                    targetNode.Segment.SubSegments.Add(draggedNode.Segment);
                }
                else
                {
                    ((SegmentDrawingNodeBase)draggedNode.Parent).Segment.SubSegments.Remove
                        (draggedNode.Segment);

                    var serialSegment = new SerialSegment();
                    serialSegment.SubSegments.Add(targetNode.Segment);
                    serialSegment.SubSegments.Add(draggedNode.Segment);
                    ((SegmentDrawingNodeBase)targetNode.Parent).Segment.SubSegments[targetNode.Index] =
                        serialSegment;
                }

                var parent = (SegmentDrawingNodeBase)draggedNode.Parent;

                // Delete empty segment
                if (parent.Nodes.Count == 1)
                {
                    ((SegmentDrawingNodeBase)parent.Parent)?.Segment.SubSegments.RemoveAt(parent.Index);
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

        private void AddCircuitButton_Click(object sender, EventArgs e)
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
            project.Circuits.Add(newCircuit);
            FillCircuitsComboBox();
            CircuitsComboBox.SelectedItem = newCircuit;
        }

        public void EditCircuitButton_Click(object sender, EventArgs e)
        {
            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var realIndexInProject = project.Circuits.IndexOf(SelectedCircuit);

            var inner = new CircuitForm
            {
                Circuit = (Circuit)SelectedCircuit.Clone()
            };
            var result = inner.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var updatedCircuit = inner.Circuit;
            project.Circuits.RemoveAt(realIndexInProject);
            project.Circuits.Insert(realIndexInProject, updatedCircuit);

            FillCircuitsComboBox();
            CircuitsComboBox.SelectedItem = updatedCircuit;
        }

        public void RemoveCircuitButton_Click(object sender, EventArgs e)
        {
            if (CircuitsComboBox.SelectedItem == null)
            {
                return;
            }

            var result = MessageBox.Show(
                $@"Do you really want to remove this circuit: {CircuitsComboBox.SelectedItem}",
                @"Remove Circuit",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result == DialogResult.OK)
            {
                project.Circuits.RemoveAt(CircuitsComboBox.SelectedIndex);
                FillCircuitsComboBox();

                if (CircuitsComboBox.SelectedItem == null && CircuitsComboBox.Items.Count != 0)
                {
                    CircuitsComboBox.SelectedItem = CircuitsComboBox.Items[0];
                }
            }
        }

        /// <summary>
        /// Draw current circuit
        /// </summary>
        private void DrawCircuit()
        {
            DrawingManager.DrawCircuit(_rootNode, PictureBox);
        }
    }
}