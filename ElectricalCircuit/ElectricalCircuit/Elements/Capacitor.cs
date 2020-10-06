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
    }
}