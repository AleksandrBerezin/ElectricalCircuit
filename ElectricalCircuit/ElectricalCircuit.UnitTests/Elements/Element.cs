using System;
using System.Numerics;
using ElectricalCircuit.Elements;

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