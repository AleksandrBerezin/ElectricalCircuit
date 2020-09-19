using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ElectricalCircuit
{
    public class Capacitor : IElement
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
            var impedance = 1 / (2 * Math.PI * frequence * Value * Complex.ImaginaryOne);
            return impedance;
        }

        public Capacitor(string name, double value, Circuit circuit)
        {
            Name = name;
            Value = value;
            ValueChanged += DisplayCapacitor;
            ValueChanged += circuit.InvokeCircuitChange;
        }

        private void DisplayCapacitor(object sender, object e)
        {
            Console.WriteLine($"Конденсатор {Name} изменил номинал на {Value}");
        }
    }
}