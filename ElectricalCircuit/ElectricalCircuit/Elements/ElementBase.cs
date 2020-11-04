using System;
using System.Collections.ObjectModel;
using System.Numerics;
using ElectricalCircuit.Segments;

namespace ElectricalCircuit.Elements
{
    /// <summary>
    /// Abstract class <see cref="ElementBase"/> provides method for calculating
    /// the element impedance for implementation 
    /// метод расчета импеданса элемента
    /// </summary>
    public abstract class ElementBase : IElement
    {
        /// <summary>
        /// Element name. Name must not be empty
        /// </summary>
        private string _name;

        /// <summary>
        /// Element value. Value must be greater than zero
        /// </summary>
        private double _value;

        /// <inheritdoc/>
        public ObservableCollection<ISegment> SubSegments { get; private set; } = null;

        /// <summary>
        /// Gets and sets count of serial segments for each node
        /// </summary>
        public int SerialSegmentsCount { get; } = 1;

        /// <summary>
        /// Gets and sets count of parallel segments for each node
        /// </summary>
        public int ParallelSegmentsCount { get; } = 1;

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
        public ElementType Type { get; protected set; }
        
        /// <inheritdoc/>
        public event EventHandler SegmentChanged;

        /// <summary>
        /// Method for calculating the impedance of element
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