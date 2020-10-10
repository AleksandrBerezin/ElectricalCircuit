using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Capacitor"/>, хранящий информацию о конденсаторе
    /// </summary>
    public class Capacitor : ElementBase
    {
        /// <summary>
        /// Создает экземпляр <see cref="Capacitor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Capacitor(string name, double value)
        {
            Name = name;
            Value = value;
        }
        
        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = 1 / (2 * Math.PI * frequency * Value * Complex.ImaginaryOne);
            return impedance;
        }
        
        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var element = obj as Capacitor;
            if (element == null)
            {
                return false;
            }

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