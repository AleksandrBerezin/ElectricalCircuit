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