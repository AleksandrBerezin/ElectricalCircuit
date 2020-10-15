using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests
{
    [TestFixture]
    public class CircuitTest
    {
        /// <summary>
        /// Creating an example of SubSegment`s list
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<ISegment> GetExampleList()
        {
            return new ObservableCollection<ISegment>()
            {
                new SerialSegment()
                {
                    SubSegments = { new Resistor("R1", 10), new Resistor("R2", 40) }
                },
                new ParallelSegment()
                {
                    SubSegments = { new Resistor("R3", 20), new Resistor("R4", 20) }
                },
                new Resistor("R5", 15),
                new Resistor("R6", 35)
            };
        }

        /// <summary>
        /// Creating an example of circuit
        /// </summary>
        /// <returns></returns>
        private Circuit GetExampleCircuit()
        {
            var serialSegment = GetExampleSerialSegment();
            var parallelSegment = GetExampleParallelSegment();

            var circuit = new Circuit("New circuit");
            circuit.SubSegments.Add(serialSegment);
            circuit.SubSegments.Add(parallelSegment);

            return circuit;
        }

        /// <summary>
        /// Creating an example of SerialSegment
        /// </summary>
        /// <returns></returns>
        private SerialSegment GetExampleSerialSegment()
        {
            var serialSegment = new SerialSegment();
            serialSegment.SubSegments.Add(new Resistor("R1", 20));
            serialSegment.SubSegments.Add(new Inductor("L1", 2e-3));
            serialSegment.SubSegments.Add(new Capacitor("C1", 2e-6));

            return serialSegment;
        }

        /// <summary>
        /// Creating an example of ParallelSegment
        /// </summary>
        /// <returns></returns>
        private ParallelSegment GetExampleParallelSegment()
        {
            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Resistor("R1", 20));
            parallelSegment.SubSegments.Add(new Inductor("L1", 2e-3));
            parallelSegment.SubSegments.Add(new Capacitor("C1", 2e-6));

            return parallelSegment;
        }

        [Test(Description = "Positive test of getter Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Circuit 1";
            var circuit = new Circuit("New circuit");
            circuit.Name = expected;
            var actual = circuit.Name;

            Assert.AreEqual(expected, actual, "Getter Name returns the wrong value");
        }

        [Test(Description = "Positive test of setter Name")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "Circuit 1";
            var circuit = new Circuit("New circuit");
            circuit.Name = expected;
            var actual = circuit.Name;

            Assert.AreEqual(expected, actual, "Setter Name assigns the wrong value");
        }

        [TestCase("", "An excepcion should be thrown if name is empty string",
            TestName = "Assigning empty string as a name")]
        [TestCase(null, "An excepcion should be thrown if name is null",
            TestName = "Assigning null as a name")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            var circuit = new Circuit("Новая цепь");

            Assert.Throws<ArgumentException>(
                () => { circuit.Name = wrongName; },
                message);
        }

        [Test(Description = "Test Circuit constructor with name")]
        public void TestCircuitConstructor_WithName()
        {
            var circuit = new Circuit("New circuit");
            var isNull = circuit == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Circuit");
        }

        [Test(Description = "Test Circuit constructor without parameters")]
        public void TestCircuitConstructor_WithoutParameters()
        {
            var circuit = new Circuit();
            var isNull = circuit == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the Circuit");
        }

        [Test(Description = "Test CalculateZ method with single frequency as a parameter")]
        public void TestCalculateZ_WithSingleFrequency()
        {
            var frequency = 100;
            var circuit = GetExampleCircuit();
            var expected = GetExampleSerialSegment().CalculateZ(frequency) +
                           GetExampleParallelSegment().CalculateZ(frequency);
            var actual = circuit.CalculateZ(frequency);

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Test CalculateZ method with frequencies list as a parameter")]
        public void TestCalculateZ_WithFrequenciesList()
        {
            var frequencies = new List<double>()
            {
                100,
                200,
                300
            };

            var circuit = GetExampleCircuit();
            var expected = frequencies.Select(frequency => 
                GetExampleSerialSegment().CalculateZ(frequency) +
                GetExampleParallelSegment().CalculateZ(frequency)).ToArray();
            var actual = circuit.CalculateZ(frequencies);

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "Circuit 1";
            var circuit = new Circuit("Circuit 1");
            var actual = circuit.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }

        [Test(Description = "Positive test of Clone method")]
        public void TestClone_CorrectValue()
        {
            var circuit = GetExampleCircuit();
            var clonedCircuit = (Circuit)circuit.Clone();
            var isEqual = clonedCircuit.Equals(circuit);

            Assert.IsTrue(isEqual, "Clone method must create an exact copy of the object");
        }

        [Test(Description = "Positive test of Equals method")]
        public void TestEquals_CorrectValue()
        {
            var circuit = GetExampleCircuit();
            var clonedCircuit = (Circuit)circuit.Clone();
            var isEqual = clonedCircuit.Equals(circuit);

            Assert.IsTrue(isEqual, "Equals method must return the true as objects are identical");
        }

        [Test(Description = "Test Equals method with different SubSegments")]
        public void TestEquals_DifferentSubSegments()
        {
            var circuit1 = new Circuit();
            circuit1.SubSegments.Add(new Resistor("R1", 20));
            circuit1.SubSegments.Add(new Inductor("L1", 2e-4));

            var circuit2 = new Circuit();
            circuit1.SubSegments.Add(new Resistor("R1", 20));

            var isEqual = circuit1.Equals(circuit2);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects have different SubSegments");
        }

        [Test(Description = "Test Equals method with different names")]
        public void TestEquals_DifferentNames()
        {
            var circuit1 = new Circuit("Circuit 1");
            var circuit2 = new Circuit("Circuit 2");
            var isEqual = circuit1.Equals(circuit2);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects have different names");
        }
    }
}