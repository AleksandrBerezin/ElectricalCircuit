using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class CapacitorTest
    {
        [Test(Description = "Positive test of Capacitor constructor")]
        public void TestCapacitorConstructor_CorrectValue()
        {
            var capacitor = new Capacitor("C1", 2e-6);
            var isNull = capacitor == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Capacitor");
        }

        [Test(Description = "Positive test of CalculateZ method")]
        public void TestCalculateZ_CorrectValue()
        {
            var capacitor = new Capacitor("C1", 15e-6);
            var impedance = capacitor.CalculateZ(100);

            var expected = (-106.1033).ToString();
            var actual = impedance.Imaginary.ToString("F4");

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "C1";
            var capacitor = new Capacitor("C1", 2e-6);
            var actual = capacitor.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }
    }
}