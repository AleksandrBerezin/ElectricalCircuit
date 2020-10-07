﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="SerialSegment"/>, хранящий информацию о последовательном участке цепи
    /// </summary>
    public class SerialSegment : ISegment, ICloneable
    {
        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; private set; }

        /// <summary>
        /// Создает экземпляр <see cref="SerialSegment"/>
        /// </summary>
        public SerialSegment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += SubSegments_CollectionChanged;
        }

        /// <inheritdoc/>
        public event EventHandler SegmentChanged;

        /// <inheritdoc/>
        public Complex CalculateZ(double frequency)
        {
            var impedance = new Complex();
            foreach(var segment in SubSegments)
            {
                impedance += segment.CalculateZ(frequency);
            }

            return impedance;
        }

        /// <summary>
        /// Подписывает и отписывает элементы на событие изменения сегмента цепи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubSegments_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    ISegment segment = e.NewItems[0] as ISegment;
                    segment.SegmentChanged += SegmentChanged;
                    SegmentChanged?.Invoke(sender, e);
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    ISegment segment = e.OldItems[0] as ISegment;
                    segment.SegmentChanged -= SegmentChanged;
                    SegmentChanged?.Invoke(sender, e);
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    ISegment replacedSegment = e.OldItems[0] as ISegment;
                    ISegment replacingSegment = e.NewItems[0] as ISegment;
                    replacedSegment.SegmentChanged -= SegmentChanged;
                    replacingSegment.SegmentChanged += SegmentChanged;
                    SegmentChanged?.Invoke(sender, e);
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return "Serial segment";
        }

        /// <inheritdoc/>
        public object Clone()
        {
            var segment = new SerialSegment();
            foreach (var subSegment in SubSegments)
            {
                segment.SubSegments.Add((ISegment)subSegment.Clone());
            }

            return segment;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var segment = (ISegment)obj;
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