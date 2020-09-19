using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ElectricalCircuit
{
    public class Inductor : IElement
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
            var impedance = 2 * Math.PI * frequence * Value * Complex.ImaginaryOne;
            return impedance;
        }

        public Inductor(string name, double value, Circuit circuit)
        {
            Name = name;
            Value = value;
            ValueChanged += DisplayInductor;
            ValueChanged += circuit.InvokeCircuitChange;
        }

        private void DisplayInductor(object sender, object e)
        {
            Console.WriteLine($"Катушка индуктивности {Name} изменила номинал на {Value}");
        }
    }
}