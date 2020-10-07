﻿using System.Numerics;
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

        [Test(Description = "Вызов события SegmentChanged при замене элемента в сегменте")]
        public void TestSegmentChangedInvoke_ReplaceElement()
        {
            var wasCalled = false;
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Inductor("L1", 2e-4));

            segment.SegmentChanged += (o, e) => wasCalled = true;
            segment.SubSegments[0] = new Capacitor("C1", 2e-6);

            Assert.IsTrue(wasCalled,
                "При замене элемента в сегменте должно вызываться событие SegmentChanged");
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

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "Serial segment";
            var segment = new SerialSegment();
            var actual = segment.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }

        [Test(Description = "Тест метода копирования")]
        public void TestClone_CorrectValue()
        {
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C1", 2e-6));
            segment.SubSegments.Add(parallelSegment);

            var clonedSegment = (SerialSegment)segment.Clone();
            var isEqual = clonedSegment.Equals(segment);

            Assert.IsTrue(isEqual, "Метод копирования должен создать точную копию объекта");
        }

        [Test(Description = "Тест метода сравнения двух объектов")]
        public void TestEquals_CorrectValue()
        {
            var segment = new SerialSegment();
            segment.SubSegments.Add(new Resistor("R1", 20));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C1", 2e-6));
            segment.SubSegments.Add(parallelSegment);

            var clonedSegment = (SerialSegment)segment.Clone();
            var isEqual = clonedSegment.Equals(segment);

            Assert.IsTrue(isEqual,
                "Метод сравнения должен вернуть истину, так как объекты идентичны");
        }
    }
}