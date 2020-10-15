using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="ParallelSegment"/>, хранящий информацию о последовательном участке цепи
    /// </summary>
    public class ParallelSegment : SegmentBase
    {
        /// <summary>
        /// Создает экземпляр <see cref="ParallelSegment"/>
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
        public override string ToString()
        {
            return "Parallel segment";
        }

        /// <inheritdoc/>
        //public override object Clone()
        //{
        //    var segment = new ParallelSegment();
        //    foreach (var subSegment in SubSegments)
        //    {
        //        segment.SubSegments.Add((ISegment)subSegment.Clone());
        //    }

        //    return segment;
        //}
    }
}