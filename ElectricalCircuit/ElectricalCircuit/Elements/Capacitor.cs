using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// <see cref="Capacitor"/> stores info about capacitor
    /// </summary>
    public class Capacitor : ElementBase
    {
        /// <summary>
        /// Create an instance of <see cref="Capacitor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Capacitor(string name, double value)
        {
            Name = name;
            Value = value;
            Type = ElementType.Capacitor;
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