using System.Collections.ObjectModel;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Segments
{
    [TestFixture]
    public class SegmentBaseTest
    {
        [Test(Description = "Positive test of getter SubSegments")]
        public void TestSubSegmentsGet_CorrectValue()
        {
            var expected = new ObservableCollection<ISegment>
            {
                new Resistor("R1", 20),
                new Resistor("R2", 40)
            };
            var segment = new Segment();
            segment.SubSegments.Add(new Resistor("R1", 20));
            segment.SubSegments.Add(new Resistor("R2", 40));
            var actual = segment.SubSegments;

            Assert.AreEqual(expected, actual, "Getter SubSegments returns the wrong value");
        }

        [Test(Description = "Invoke SegmentChanged event when item added to a segment")]
        public void TestOnSegmentChanged_AddItem()
        {
            var wasCalled = false;
            var segment = new Segment();

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            Assert.IsTrue(wasCalled,
                "SegmentChangen event must be invoked when item added to a segment");
        }

        [Test(Description = "Invoke SegmentChanged event when item removed from a segment")]
        public void TestOnSegmentChanged_RemoveItem()
        {
            var wasCalled = false;
            var segment = new Segment();
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.RemoveAt(0);

            Assert.IsTrue(wasCalled,
                "SegmentChangen event must be invoked when item removed from a segment");
        }

        [Test(Description = "Invoke SegmentChanged event when item replaced in a segment")]
        public void TestOnSegmentChanged_ReplaceItem()
        {
            var wasCalled = false;
            var segment = new Segment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments[0] = new Capacitor("C1", 2e-6);

            Assert.IsTrue(wasCalled,
                "SegmentChangen event must be invoked when item replaced in a segment");
        }

        [Test(Description = "Positive test of Equals method")]
        public void TestEquals_CorrectValue()
        {
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C1", 2e-6));
            segment.SubSegments.Add(parallelSegment);

            var clonedSegment = (SerialSegment)segment.Clone();
            var isEqual = clonedSegment.Equals(segment);

            Assert.IsTrue(isEqual, "Equals method must return the true as objects are identical");
        }

        [Test(Description = "Test Equals method with different classes")]
        public void TestEquals_DifferentClasses()
        {
            var serialSegment = new SerialSegment();
            serialSegment.SubSegments.Add(new Resistor("R1", 20));
            serialSegment.SubSegments.Add(new Inductor("L1", 2e-4));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Resistor("R1", 20));
            parallelSegment.SubSegments.Add(new Inductor("L1", 2e-4));

            var isEqual = serialSegment.Equals(parallelSegment);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects are different classes");
        }

        [Test(Description = "Test Equals method with different SubSegments count")]
        public void TestEquals_DifferentSubSegmentsCount()
        {
            var serialSegment1 = new SerialSegment();
            serialSegment1.SubSegments.Add(new Resistor("R1", 20));

            var serialSegment2 = new SerialSegment();
            serialSegment2.SubSegments.Add(new Resistor("R1", 20));
            serialSegment2.SubSegments.Add(new Inductor("L1", 2e-4));

            var isEqual = serialSegment1.Equals(serialSegment2);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects have different SubSegments count");
        }

        [Test(Description = "Test Equals method with different SubSegments")]
        public void TestEquals_DifferentSubSegments()
        {
            var serialSegment1 = new SerialSegment();
            serialSegment1.SubSegments.Add(new Resistor("R1", 20));
            serialSegment1.SubSegments.Add(new Inductor("L1", 2e-4));

            var serialSegment2 = new SerialSegment();
            serialSegment2.SubSegments.Add(new Resistor("R2", 20));
            serialSegment2.SubSegments.Add(new Inductor("L2", 2e-4));

            var isEqual = serialSegment1.Equals(serialSegment2);

            Assert.IsFalse(isEqual,
                "Equals method must return the false as objects have different SubSegments");
        }
    }
}