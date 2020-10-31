﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;

namespace ElectricalCircuit.Segments
{
    /// <summary>
    /// Abstract class <see cref="SegmentBase"/> provides method for calculating
    /// the segment impedance for implementation
    /// </summary>
    public abstract class SegmentBase : ISegment
    {
        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; protected set; }

        /// <summary>
        /// Gets and sets count of serial segments for each node
        /// </summary>
        public int SerialSegmentsCount { get; protected set; } = 0;

        /// <summary>
        /// Gets and sets count of parallel segments for each node
        /// </summary>
        public int ParallelSegmentsCount { get; protected set; } = 0;

        /// <summary>
        /// Informs a change in circuit segment
        /// </summary>
        private event EventHandler _segmentChanged;

        /// <summary>
        /// Adds and removes handlers for the circuit segment change event
        /// </summary>
        public event EventHandler SegmentChanged
        {
            add
            {
                _segmentChanged += value;
                if (SubSegments == null)
                {
                    return;
                }

                foreach (var segment in SubSegments)
                {
                    segment.SegmentChanged += value;
                }
            }
            remove
            {
                _segmentChanged -= value;
                if (SubSegments == null)
                {
                    return;
                }

                foreach (var segment in SubSegments)
                {
                    segment.SegmentChanged -= value;
                }
            }
        }

        /// <inheritdoc/>
        public abstract Complex CalculateZ(double frequency);

        /// <summary>
        /// Calculating the count of parallel and serial segments
        /// </summary>
        protected virtual void CalculateSegmentsCount()
        {
            SerialSegmentsCount = 0;
            var maxParallelCount = 0;

            foreach (var segment in SubSegments)
            {
                SerialSegmentsCount += segment.SerialSegmentsCount;
                if (segment.ParallelSegmentsCount > maxParallelCount)
                {
                    maxParallelCount = segment.ParallelSegmentsCount;
                }

                ParallelSegmentsCount = maxParallelCount;
            }
        }

        /// <summary>
        /// Subscribes and unsubscribes elements to segment change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    ISegment segment = e.NewItems[0] as ISegment;
                    segment.SegmentChanged += _segmentChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    ISegment segment = e.OldItems[0] as ISegment;
                    segment.SegmentChanged -= _segmentChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    ISegment replacedSegment = e.OldItems[0] as ISegment;
                    ISegment replacingSegment = e.NewItems[0] as ISegment;
                    replacedSegment.SegmentChanged -= _segmentChanged;
                    replacingSegment.SegmentChanged += _segmentChanged;
                    break;
                }
            }

            CalculateSegmentsCount();
            _segmentChanged?.Invoke(sender, e);
        }

        /// <inheritdoc/>
        public virtual object Clone()
        {
            var segment = (ISegment)Activator.CreateInstance(GetType());
            foreach (var subSegment in SubSegments)
            {
                segment.SubSegments.Add((ISegment)subSegment.Clone());
            }

            return segment;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (GetType() != obj?.GetType())
            {
                return false;
            }

            var segment = (ISegment)obj;
            if (SubSegments.Count != segment.SubSegments.Count)
            {
                return false;
            }

            for (int i = 0; i < SubSegments.Count; i++)
            {
                if (!segment.SubSegments[i].Equals(SubSegments[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}