using System;
using ElectricalCircuit.Elements;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class ElementBaseTest
    {
        [Test(Description = "Positive test of getter Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "New element";
            var element = new Element();
            element.Name = expected;
            var actual = element.Name;

            Assert.AreEqual(expected, actual, "Getter Name returns the wrong value");
        }

        [Test(Description = "Positive test of setter Name")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "New element";
            var element = new Element();
            element.Name = expected;
            var actual = element.Name;

            Assert.AreEqual(expected, actual, "Setter Name assigns the wrong value");
        }

        [TestCase("", "An excepcion should be thrown if name is empty string",
            TestName = "Assigning empty string as a name")]
        [TestCase(null, "An excepcion should be thrown if name is null",
            TestName = "Assigning null as a name")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            var element = new Element();

            Assert.Throws<ArgumentException>(
                () => { element.Name = wrongName; },
                    message);
        }

        [Test(Description = "Positive test of getter Value")]
        public void TestValueGet_CorrectValue()
        {
            var expected = 14.345;
            var element = new Element();
            element.Value = expected;
            var actual = element.Value;

            Assert.AreEqual(expected, actual, "Getter Value returns the wrong value");
        }

        [Test(Description = "Positive test of setter Value")]
        public void TestValueSet_CorrectValue()
        {
            var expected = 14.345;
            var element = new Element();
            element.Value = expected;
            var actual = element.Value;

            Assert.AreEqual(expected, actual, "Setter value assigns the wrong value");
        }

        [Test(Description = "Assigning negative number as a value")]
        public void TestValueSet_NegativeValue()
        {
            var wrongValue = -0.345;
            var element = new Element();

            Assert.Throws<ArgumentException>(
                () => { element.Value = wrongValue; },
                "An exception should be thrown if value is negative number");
        }

        [Test(Description = "Positive test of Clone method")]
        public void TestClone_CorrectValue()
        {
            var element = new Element()
            {
                Name = "Element 1",
                Value = 20
            };
            var clonedElement = (Element)element.Clone();
            var isEqual = clonedElement.Equals(element);

            Assert.IsTrue(isEqual, "Clone method must create an exact copy of the object");
        }

        [Test(Description = "Positive test of Equals method")]
        public void TestEquals_CorrectValue()
        {
            var resistor = new Resistor("R1", 20);
            var clonedResistor = (Resistor)resistor.Clone();
            var isEqual = clonedResistor.Equals(resistor);

            Assert.IsTrue(isEqual, "Equals method must return the true as objects are identical");
        }

        [Test(Description = "Test Equals method with different classes")]
        public void TestEquals_DifferentClasses()
        {
            var resistor = new Resistor("R1", 20);
            var capacitor = new Capacitor("R1", 20);
            var isEqual = resistor.Equals(capacitor);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects are different classes");
        }

        [Test(Description = "Test Equals method with different names")]
        public void TestEquals_DifferentNames()
        {
            var resistor1 = new Resistor("R1", 20);
            var resistor2 = new Resistor("R2", 20);
            var isEqual = resistor1.Equals(resistor2);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects have different names");
        }
    }
}