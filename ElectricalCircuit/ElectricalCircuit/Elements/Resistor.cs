using System.Numerics;

namespace ElectricalCircuit.Elements
{
    /// <summary>
    /// <see cref="Resistor"/> stores info about resistor
    /// </summary>
    public class Resistor : ElementBase
    {
        /// <summary>
        /// Create an instance of <see cref="Resistor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Resistor(string name, double value)
        {
            Name = name;
            Value = value;
            Type = ElementType.Resistor;
        }

        /// <summary>
        /// Create an instance of <see cref="Resistor"/>
        /// </summary>
        public Resistor()
        {
            Name = "Новый элемент";
            Value = 1;
            Type = ElementType.Resistor;
        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            return new Complex(Value, 0);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }
}