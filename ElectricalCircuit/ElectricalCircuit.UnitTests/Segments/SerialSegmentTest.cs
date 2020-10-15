using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Segments
{
    [TestFixture]
    public class SerialSegmentTest
    {
        [Test(Description = "Positive test of SerialSegment constructor")]
        public void TestSerialSegmentConstructor_CorrectValue()
        {
            var segment = new SerialSegment();
            var isNull = segment == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of the SerialSegment");
        }

        [Test(Description = "Positive test of CalculateZ method")]
        public void TestCalculateZ_CorrectValue()
        {
            var segment = new SerialSegment();
            var resistor = new Resistor("R1", 20);
            var inductor = new Inductor("L1", 2e-3);
            var capacitor = new Capacitor("C1", 2e-6);
            var frequency = 100;

            segment.SubSegments.Add(resistor);
            segment.SubSegments.Add(inductor);
            segment.SubSegments.Add(capacitor);

            var expected = resistor.CalculateZ(frequency) + inductor.CalculateZ(frequency)
                                                          + capacitor.CalculateZ(frequency);
            var actual = segment.CalculateZ(frequency);

            Assert.AreEqual(expected, actual, "CalculateZ method calculate impedance wrong");
        }

        [Test(Description = "Positive test of ToString method")]
        public void TestToString_CorrectValue()
        {
            var expected = "Serial segment";
            var segment = new SerialSegment();
            var actual = segment.ToString();

            Assert.AreEqual(expected, actual, "ToString method returns the wrong string");
        }

        [Test(Description = "Positive test of Clone method")]
        public void TestClone_CorrectValue()
        {
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C1", 2e-6));
            segment.SubSegments.Add(parallelSegment);

            var clonedSegment = (SerialSegment)segment.Clone();
            var isEqual = clonedSegment.Equals(segment);

            Assert.IsTrue(isEqual, "Clone method must create an exact copy of the object");
        }
    }
}