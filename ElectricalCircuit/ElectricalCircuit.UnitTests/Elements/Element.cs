using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace ElectricalCircuit.UnitTests.Elements
{
    public class Element : ElementBase
    {
        public override Complex CalculateZ(double frequency)
        {
            throw new NotImplementedException();
        }
    }
}