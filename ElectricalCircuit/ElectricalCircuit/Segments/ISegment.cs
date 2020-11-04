using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace ElectricalCircuit.Segments
{
    /// <summary>
    /// <see cref="ISegment"/> defines fields and methods for working with circuit segments
    /// </summary>
    public interface ISegment : ICloneable
    {
        /// <summary>
        /// Gets and sets list of sub-segments
        /// </summary>
        ObservableCollection<ISegment> SubSegments { get; }

        //TODO: еще раз: интерфейс ничего не должен знать о дочерних классах. Свойства типа SerialSegmentsCount - это знание о том, как элементы соединены в дочерних классах. При этом оба Count-а взаимоисключающие - это неправильно
        /// <summary>
        /// Gets and sets count of serial segments for each node
        /// </summary>
        int SerialSegmentsCount { get; }

        /// <summary>
        /// Gets and sets count of parallel segments for each node
        /// </summary>
        int ParallelSegmentsCount { get; }

        /// <summary>
        /// Informs a change in circuit segment
        /// </summary>
        event EventHandler SegmentChanged;

        /// <summary>
        /// Method for calculating the impedance of circuit segments
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        Complex CalculateZ(double frequency);
    }
}