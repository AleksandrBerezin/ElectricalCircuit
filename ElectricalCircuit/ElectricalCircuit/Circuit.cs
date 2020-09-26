using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Circuit"/>, хранящий список элементов цепи
    /// </summary>
    public class Circuit
    {
        /// <summary>
        /// Название цепи. Название не должно быть пустым
        /// </summary>
        private string _name;

        /// <summary>
        /// Возвращает и задает название цепи
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == "" || value == null)
                {
                    throw new ArgumentException("Название цепи не должно быть пустым");
                }

                _name = value;
            }
        }

        /// <summary>
        /// Возвращает и задает список внутренних элементов
        /// </summary>
        public ObservableCollection<ISegment> Segments { get; set; }

        /// <summary>
        /// Метод для расчета импеданса цепи
        /// </summary>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        public virtual Complex[] CalculateZ(List<double> frequencies)
        {
            var impedances = new List<Complex>();
            foreach (var frequency in frequencies)
            {
                var impedance = new Complex();
                foreach (var segment in Segments)
                {
                    impedance += segment.CalculateZ(frequency);
                }

                impedances.Add(impedance);
            }

            return impedances.ToArray();
        }

        public Circuit(string name)
        {
            Name = name;
            Segments = new ObservableCollection<ISegment>();
            Segments.CollectionChanged += Segments_CollectionChanged;
        }

        /// <summary>
        /// Подписывает и отписывает элементы на событие изменения цепи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Segments_CollectionChanged(object sender, 
            NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    ISegment segment = e.NewItems[0] as ISegment;
                    segment.SegmentChanged += this.CircuitChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    ISegment segment = e.OldItems[0] as ISegment;
                    segment.SegmentChanged -= this.CircuitChanged;
                    break;
                }
            }
        }

        /// <summary>
        /// Сообщает об изменении цепи
        /// </summary>
        public event EventHandler CircuitChanged;
    }
}