using System;
using System.Numerics;

namespace ElectricalCircuit.Elements
{
    /// <summary>
    /// <see cref="Inductor"/> stores info about inductor
    /// </summary>
    public class Inductor : ElementBase
    {
        /// <summary>
        /// Create an instance of <see cref="Inductor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Inductor(string name, double value)
        {
            Name = name;
            Value = value;
            Type = ElementType.Inductor;
        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = 2 * Math.PI * frequency * Value * Complex.ImaginaryOne;
            return impedance;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }
}