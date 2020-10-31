using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit
{
    /// <summary>
    /// <see cref="ParallelSegment"/> stores info about parallel segment of the circuit
    /// </summary>
    public class ParallelSegment : SegmentBase
    {
        /// <summary>
        /// Create an instance of <see cref="ParallelSegment"/>
        /// </summary>
        public ParallelSegment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += OnCollectionChanged;
        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var result = new Complex();
            foreach (var segment in SubSegments)
            {
                var impedance = segment.CalculateZ(frequency);
                result += 1 / impedance;
            }

            return 1 / result;
        }

        /// <inheritdoc/>
        protected override void CalculateSegmentsCount()
        {
            ParallelSegmentsCount = 0;
            var maxSerialCount = 0;

            foreach (var segment in SubSegments)
            {
                ParallelSegmentsCount += segment.ParallelSegmentsCount;
                if (segment.SerialSegmentsCount > maxSerialCount)
                {
                    maxSerialCount = segment.SerialSegmentsCount;
                }

                SerialSegmentsCount = maxSerialCount;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Parallel segment";
        }
    }
}