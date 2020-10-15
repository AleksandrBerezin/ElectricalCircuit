using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="SerialSegment"/>, хранящий информацию о последовательном участке цепи
    /// </summary>
    public class SerialSegment : SegmentBase
    {
        /// <summary>
        /// Создает экземпляр <see cref="SerialSegment"/>
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

        /// <inheritdoc/>
        //public override object Clone()
        //{
        //    var segment = new SerialSegment();
        //    foreach (var subSegment in SubSegments)
        //    {
        //        segment.SubSegments.Add((ISegment)subSegment.Clone());
        //    }

        //    return segment;
        //}
    }
}