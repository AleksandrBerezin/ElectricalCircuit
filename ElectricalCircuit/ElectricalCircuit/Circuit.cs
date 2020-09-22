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
        /// Список элеентов цепи
        /// </summary>
        public ObservableCollection<IElement> Elements;

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
                foreach (var element in Elements)
                {
                    impedance += element.CalculateZ(frequency);
                }

                impedances.Add(impedance);
            }

            return impedances.ToArray();
        }

        /// <summary>
        /// Создает экземпляр <see cref="Circuit"/>
        /// </summary>
        public Circuit()
        {
            Elements = new ObservableCollection<IElement>();
            Elements.CollectionChanged += Elements_CollectionChanged;
        }

        /// <summary>
        /// Подписывает и отписывает элементы на событие изменения цепи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    Element element = e.NewItems[0] as Element;
                    element.ValueChanged += this.CircuitChanged;
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    Element element = e.OldItems[0] as Element;
                    element.ValueChanged -= this.CircuitChanged;
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