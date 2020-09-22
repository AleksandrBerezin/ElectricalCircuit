using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Абстрактный класс <see cref="Element"/>, предоставляющий для реализации метод расчета импеданса
    /// </summary>
    public abstract class Element : IElement
    {
        /// <summary>
        /// Название элемента. Название не должно быть пустым
        /// </summary>
        private string _name;

        /// <summary>
        /// Номинал элемента. Номинал должен быть больше нуля
        /// </summary>
        private double _value;

        /// <summary>
        /// Возвращает и задает название элемента
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
                    throw new ArgumentException("Название элемента не должно быть пустым");
                }

                _name = value;
            }
        }

        /// <summary>
        /// Возвращает и задает номинал элемента
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Номинал элемента должен быть положительным");
                }

                if (value != _value)
                {
                    _value = value;
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Метод для расчета импеданса элемента
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public abstract Complex CalculateZ(double frequency);

        /// <summary>
        /// Сообщает об изменении номинала элемента
        /// </summary>
        public event EventHandler ValueChanged;
    }
}