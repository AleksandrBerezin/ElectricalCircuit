using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class ResistorTest
    {
        [Test(Description = "Positive test of Resistor constructor")]
        public void TestResistorConstructor_CorrectValue()
        {
            var resistor = new Resistor();
            var isNull = resistor == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Resistor");
        }

        [Test(Description = "Test Resistor constructor with name and value")]
        public void TestResistorConstructor_WithNameAndValue()
        {
            var resistor = new Resistor("R1", 20);
            var isNull = resistor == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Resistor");
        }

        [Test(Description = "Positive test of CalculateZ method")]
        public void TestCalculateZ_CorrectValue()
        {
            var value = 20;
            var expected = new Complex(value, 0);
            var resistor = new Resistor("R1", value);
            var actual = resistor.CalculateZ(100);

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "R1";
            var resistor = new Resistor("R1", 20);
            var actual = resistor.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }
    }
}