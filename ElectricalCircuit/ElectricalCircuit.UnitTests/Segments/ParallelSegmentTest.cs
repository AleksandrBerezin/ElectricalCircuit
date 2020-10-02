using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Segments
{
    [TestFixture]
    public class ParallelSegmentTest
    {
        [Test(Description = "Позитивный тест конструктора ParallelSegment")]
        public void TestParallelSegmentConstructor_CorrectValue()
        {
            var segment = new ParallelSegment();
            var isNull = segment == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса ParallelSegment");
        }

        [Test(Description = "Вызов события SegmentChanged при добавлении элемента в сегмент")]
        public void TestSegmentChangedInvoke_AddElement()
        {
            var wasCalled = false;
            var segment = new ParallelSegment();

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            Assert.IsTrue(wasCalled,
                "При добавлении элемента в сегмент должно вызываться событие SegmentChanged");
        }

        [Test(Description = "Вызов события SegmentChanged при удалении элемента из сегмента")]
        public void TestSegmentChangedInvoke_RemoveElement()
        {
            var wasCalled = false;
            var segment = new ParallelSegment();
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments.RemoveAt(0);

            Assert.IsTrue(wasCalled,
                "При удалении элемента из сегмента должно вызываться событие SegmentChanged");
        }

        [Test(Description = "Вызов события SegmentChanged при замене элемента в сегменте")]
        public void TestSegmentChangedInvoke_ReplaceElement()
        {
            var wasCalled = false;
            var segment = new ParallelSegment();
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments[0] = new Capacitor("C1", 2e-6);

            Assert.IsTrue(wasCalled,
                "При замене элемента в сегменте должно вызываться событие SegmentChanged");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
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

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "Parallel segment";
            var segment = new ParallelSegment();
            var actual = segment.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }
    }
}