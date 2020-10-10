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
    public class Circuit : ICloneable
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
                if (string.IsNullOrEmpty(value))
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
        /// Создает экземпляр <see cref="Circuit"/>
        /// </summary>
        /// <param name="name"></param>
        public Circuit(string name)
        {
            Name = name;
            Segments = new ObservableCollection<ISegment>();
            Segments.CollectionChanged += OnCollectionChanged;
        }
        
        /// <summary>
        /// Создает экземпляр <see cref="Circuit"/>
        /// </summary>
        public Circuit()
        {
            Segments = new ObservableCollection<ISegment>();
            Segments.CollectionChanged += OnCollectionChanged;
        }

        /// <summary>
        /// Сообщает об изменении цепи
        /// </summary>
        private event EventHandler _circuitChanged;

        /// <summary>
        /// Добавляет и удаляет обработчики события изменения цепи
        /// </summary>
        public event EventHandler CircuitChanged
        {
            add
            {
                _circuitChanged += value;
                if (Segments == null)
                {
                    return;
                }

                foreach (var segment in Segments)
                {
                    segment.SegmentChanged += value;
                }
            }
            remove
            {
                _circuitChanged -= value;
                if (Segments == null)
                {
                    return;
                }

                foreach (var segment in Segments)
                {
                    segment.SegmentChanged -= value;
                }
            }
        }

        /// <summary>
        /// Метод для расчета импеданса цепи
        /// </summary>
        /// <param name="frequencies"></param>
        /// <returns></returns>
        public Complex[] CalculateZ(List<double> frequencies)
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

        /// <summary>
        /// Подписывает и отписывает элементы на событие изменения цепи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    ISegment segment = e.NewItems[0] as ISegment;
                    segment.SegmentChanged += _circuitChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    ISegment segment = e.OldItems[0] as ISegment;
                    segment.SegmentChanged -= _circuitChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Replace:
                {
                    ISegment replacedSegment = e.OldItems[0] as ISegment;
                    ISegment replacingSegment = e.NewItems[0] as ISegment;
                    replacedSegment.SegmentChanged -= _circuitChanged;
                    replacingSegment.SegmentChanged += _circuitChanged;
                    break;
                }
            }

            _circuitChanged?.Invoke(sender, e);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

        /// <inheritdoc"/>
        public object Clone()
        {
            var circuit = new Circuit(Name);
            foreach (var segment in Segments)
            {
                circuit.Segments.Add((ISegment)segment.Clone());
            }

            return circuit;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var circuit = obj as Circuit;
            if (circuit == null)
            {
                return false;
            }

            if (Segments.Count != circuit.Segments.Count)
            {
                return false;
            }

            for (int i = 0; i < Segments.Count; i++)
            {
                if (!circuit.Segments[i].Equals(Segments[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}