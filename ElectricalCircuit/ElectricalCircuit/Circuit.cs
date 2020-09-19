using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalCircuit
{
    public class Circuit
    {
        public event ValueChangeEventHandler CircuitChanged;

        public EventDrivenList<IElement> Elements;

        public Complex[] CalculateZ(List<double> frequencies)
        {
            var impedances = new List<Complex>();

            foreach (var frequence in frequencies)
            {
                var newImpedance = new Complex();

                foreach (var element in Elements)
                {
                    newImpedance += element.CalculateZ(frequence);
                }

                impedances.Add(newImpedance);
            }

            return impedances.ToArray();
        }

        public Circuit()
        {
            Elements = new EventDrivenList<IElement>();
            CircuitChanged += DisplayCircuit;
            Elements.ItemAdded += CircuitChanged;
            Elements.ItemRemoved += CircuitChanged;
        }

        private void DisplayCircuit(object sender, object e)
        {
            Console.WriteLine("Схема изменена");
        }

        public void InvokeCircuitChange(object sender, object e)
        {
            CircuitChanged?.Invoke(sender, e);
        }
    }
}