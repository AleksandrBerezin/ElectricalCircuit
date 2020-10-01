using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Segments
{
    [TestFixture]
    public class SerialSegmentTest
    {
        [Test(Description = "Позитивный тест конструктора SerialSegment")]
        public void TestSerialSegmentConstructor_CorrectValue()
        {
            var segment = new SerialSegment();
            var isNull = segment == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса SerialSegment");
        }

        [Test(Description = "Вызов события SegmentChanged при добавлении элемента в сегмент")]
        public void TestSegmentChangedInvoke_AddElement()
        {
            var wasCalled = false;
            var segment = new SerialSegment();

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            Assert.IsTrue(wasCalled,
                "При добавлении элемента в сегмент должно вызываться событие SegmentChanged");
        }

        [Test(Description = "Вызов события SegmentChanged при удалении элемента из сегмента")]
        public void TestSegmentChangedInvoke_RemoveElement()
        {
            var wasCalled = false;
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.RemoveAt(0);

            Assert.IsTrue(wasCalled,
                "При удалении элемента из сегмента должно вызываться событие SegmentChanged");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
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

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }
    }
}