﻿using ElectricalCircuit.Elements;
using ElectricalCircuit.Segments;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Segments
{
    [TestFixture]
    public class ParallelSegmentTest
    {
        /// <summary>
        /// Creating an example of SerialSegment
        /// </summary>
        /// <returns></returns>
        private ParallelSegment GetExampleSerialSegment()
        {
            var segment = new ParallelSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));
            segment.SubSegments.Add(new Inductor("L1", 2e-3));
            segment.SubSegments.Add(new Capacitor("C1", 2e-6));

            return segment;
        }

        [Test(Description = "Positive test of ParallelSegment constructor")]
        public void TestParallelSegmentConstructor_CorrectValue()
        {
            var segment = new ParallelSegment();
            var isNull = segment == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the ParallelSegment");
        }

        [Test(Description = "Positive test of CalculateZ method")]
        public void TestCalculateZ_CorrectValue()
        {
            var segment = new ParallelSegment();
            var resistor = new Resistor("R1", 20);
            var inductor = new Inductor("L1", 2e-3);
            var capacitor = new Capacitor("C1", 2e-6);
            var frequency = 100;

            segment.SubSegments.Add(resistor);
            segment.SubSegments.Add(inductor);
            segment.SubSegments.Add(capacitor);

            var expected = 1 / resistor.CalculateZ(frequency) +
                           1 / inductor.CalculateZ(frequency) + 1 / capacitor.CalculateZ(frequency);
            expected = 1 / expected;
            var actual = segment.CalculateZ(frequency);

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Test CalculateSegmentsCount method for SerialSegmentsCount")]
        public void TestCalculateSegmentsCount_CalculateSerialSegmentsCount()
        {
            var segment = GetExampleSerialSegment();

            var expected = 1;
            var actual = segment.SerialSegmentsCount;

            Assert.AreEqual(expected, actual, "CalculateSegmentsCount method calculate " +
                                              "count of serial segments wrong");
        }

        [Test(Description = "Test CalculateSegmentsCount method for ParallelSegmentsCount")]
        public void TestCalculateSegmentsCount_CalculateParallelSegmentsCount()
        {
            var segment = GetExampleSerialSegment();

            var expected = 3;
            var actual = segment.ParallelSegmentsCount;

            Assert.AreEqual(expected, actual, "CalculateSegmentsCount method calculate " +
                                              "count of parallel segments wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "Parallel segment";
            var segment = new ParallelSegment();
            var actual = segment.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }

        [Test(Description = "Positive test of Clone method")]
        public void TestClone_CorrectValue()
        {
            var segment = new ParallelSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            var serialSegment = new SerialSegment();
            serialSegment.SubSegments.Add(new Capacitor("C1", 2e-6));
            segment.SubSegments.Add(serialSegment);

            var clonedSegment = (ParallelSegment)segment.Clone();
            var isEqual = clonedSegment.Equals(segment);

            Assert.IsTrue(isEqual, "Clone method must create an exact copy of the object");
        }
    }
}