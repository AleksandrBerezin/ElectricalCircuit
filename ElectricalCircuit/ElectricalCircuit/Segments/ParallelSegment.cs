using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="ParallelSegment"/>, хранящий информацию о последовательном участке цепи
    /// </summary>
    public class ParallelSegment : ISegment
    {
        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; private set; }
        
        /// <summary>
        /// Создает экземпляр <see cref="ParallelSegment"/>
        /// </summary>
        public ParallelSegment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += SubSegments_CollectionChanged;
        }

        /// <inheritdoc/>
        public event EventHandler SegmentChanged;

        /// <inheritdoc/>
        public Complex CalculateZ(double frequency)
        {
            var result = new Complex();
            foreach (var segment in SubSegments)
            {
                var impedance = segment.CalculateZ(frequency);
                result += 1 / impedance;
            }

            return 1 / result;
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
            }
        }
    }
}