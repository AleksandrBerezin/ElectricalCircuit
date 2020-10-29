using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit
{
    /// <summary>
    /// <see cref="SerialSegment"/> stores info about serial segment of the circuit
    /// </summary>
    public class SerialSegment : SegmentBase
    {
        /// <summary>
        /// Creates an instance of <see cref="SerialSegment"/>
        /// </summary>
        public SerialSegment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += OnCollectionChanged;
        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = new Complex();
            foreach(var segment in SubSegments)
            {
                impedance += segment.CalculateZ(frequency);
            }

            return impedance;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Serial segment";
        }
    }
}