using System;
using System.Numerics;
using System.Collections.ObjectModel;

namespace ElectricalCircuit
{
    /// <summary>
    /// Абстрактный класс <see cref="ElementBase"/>, предоставляющий для реализации 
    /// метод расчета импеданса элемента
    /// </summary>
    public abstract class ElementBase : IElement
    {
        /// <summary>
        /// Название элемента. Название не должно быть пустым
        /// </summary>
        private string _name;

        /// <summary>
        /// Номинал элемента. Номинал должен быть больше нуля
        /// </summary>
        private double _value;

        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; private set; } = null;

        /// <inheritdoc/>
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
                    throw new ArgumentException("Название элемента не должно быть пустым");
                }

                _name = value;
            }
        }

        /// <inheritdoc/>
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
                }
            }
        }
        
        /// <inheritdoc/>
        public event EventHandler SegmentChanged;

        /// <summary>
        /// Метод для расчета импеданса элемента
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public abstract Complex CalculateZ(double frequency);

        /// <inheritdoc/>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }

            var element = (IElement)obj;
            if (Name == element.Name && Value == element.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}