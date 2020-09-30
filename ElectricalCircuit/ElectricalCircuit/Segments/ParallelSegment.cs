using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="ParallelSegment"/>, хранящий информацию о последовательном участке цепи
    /// </summary>
    class ParallelSegment : ISegment
    {
        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; private set; }

        /// <inheritdoc/>
        public Complex CalculateZ(double frequency)
        {
            var impedancesSum = new Complex();
            var impedancesMultiplication = new Complex();

            foreach (var segment in SubSegments)
            {
                var impedance = segment.CalculateZ(frequency);
                impedancesSum += impedance;

                if (impedancesMultiplication == 0)
                {
                    impedancesMultiplication = impedance;
                }
                else
                {
                    impedancesMultiplication *= impedance;
                }
            }

            return impedancesMultiplication / impedancesSum;
        }

        /// <summary>
        /// Создает экземпляр <see cref="ParallelSegment"/>
        /// </summary>
        public ParallelSegment()
        {
            SubSegments = new ObservableCollection<ISegment>();
            SubSegments.CollectionChanged += SubSegments_CollectionChanged;
        }

        /// <summary>
        /// Подписывает и отписывает элементы на событие изменения сегмента цепи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SubSegments_CollectionChanged(object sender, 
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    ISegment segment = e.NewItems[0] as ISegment;
                    segment.SegmentChanged += this.SegmentChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    ISegment segment = e.OldItems[0] as ISegment;
                    segment.SegmentChanged -= this.SegmentChanged;
                    break;
                }
            }
        }

        /// <inheritdoc/>
        public event EventHandler SegmentChanged;
    }
}