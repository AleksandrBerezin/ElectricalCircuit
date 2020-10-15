using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class InductorTest
    {
        [Test(Description = "Positive test of Inductor constructor")]
        public void TestInductorConstructor_CorrectValue()
        {
            var inductor = new Inductor("L1", 2e-3);
            var isNull = inductor == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Inductor");
        }

        [Test(Description = "Positive test of CalculateZ method")]
        public void TestCalculateZ_CorrectValue()
        {
            var inductor = new Inductor("L1", 0.002);
            var impedance = inductor.CalculateZ(100);

            var expected = 1.25664.ToString();
            var actual = impedance.Imaginary.ToString("F5");

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "L1";
            var inductor = new Inductor("L1", 0.002);
            var actual = inductor.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }
    }
}