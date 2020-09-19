using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectricalCircuit;
using System.Numerics;

namespace ElectricalCircuitUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            Circuit circuit = new Circuit();
            circuit.Elements.Add(new Resistor("R1", 30, circuit));
            circuit.Elements.Add(new Resistor("R2", 20, circuit));
            circuit.Elements.Add(new Inductor("L1", 300, circuit));
            circuit.Elements.Add(new Inductor("L2", 600, circuit));
            circuit.Elements.Add(new Capacitor("C1", 3000, circuit));
            circuit.Elements.Add(new Capacitor("C2", 2500, circuit));

            circuit.Elements[1].Value = 40;
            circuit.Elements[3].Value = 400;
            circuit.Elements[5].Value = 4000;

            circuit.Elements.Remove(circuit.Elements[4]);

            //Capacitor capacitor = new Capacitor("C2", 0.006, circuit);
            //Complex impedance = capacitor.CalculateZ(300);
            //Console.WriteLine(impedance);
        }
    }
}
