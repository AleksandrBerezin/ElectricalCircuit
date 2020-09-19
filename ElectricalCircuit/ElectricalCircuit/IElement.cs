using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.ComponentModel;

namespace ElectricalCircuit
{
    public delegate void ValueChangeEventHandler(object sender, object e);

    public interface IElement
    {
        event ValueChangeEventHandler ValueChanged;

        string Name { get; set; }
        double Value { get; set; }

        Complex CalculateZ(double frequence);
    }
}