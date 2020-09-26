using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Resistor"/>, хранящий информацию о резисторе
    /// </summary>
    public class Resistor : Element
    {
        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            return new Complex(Value, 0);
        }

        /// <summary>
        /// Создает экземпляр <see cref="Resistor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Resistor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Резистор {Name}, номинал = {Value} Ом";
        }
    }
}