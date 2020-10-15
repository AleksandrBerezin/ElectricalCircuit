using System;
using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit.UnitTests.Segments
{
    public class Segment : SegmentBase
    {
        public override Complex CalculateZ(double frequency)
        {
            throw new NotImplementedException();
        }

        public Segment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += OnCollectionChanged;
        }
    }
}