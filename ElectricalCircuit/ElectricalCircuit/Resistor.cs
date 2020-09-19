using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ElectricalCircuit
{
    public class Resistor : IElement
    {
        public event ValueChangeEventHandler ValueChanged;

        private double _value;

        public string Name { get; set; }
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Complex CalculateZ(double frequence)
        {
            return new Complex(Value, 0);
        }

        public Resistor(string name, double value, Circuit circuit)
        {
            Name = name;
            Value = value;
            ValueChanged += DisplayResistor;
            ValueChanged += circuit.InvokeCircuitChange;
        }

        private void DisplayResistor(object sender, object e)
        {
            Console.WriteLine($"Резистор {Name} изменил номинал на {Value}");
        }
    }
}