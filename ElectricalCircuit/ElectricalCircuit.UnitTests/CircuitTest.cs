using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests
{
    [TestFixture]
    public class CircuitTest
    {
        /// <summary>
        /// Метод для создания списка сегментов
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

        [Test(Description = "Позитивный тест геттера Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Цепь 1";
            var circuit = new Circuit("Новая цепь");
            circuit.Name = expected;
            var actual = circuit.Name;

            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное значение");
        }

        [Test(Description = "Позитивный тест сеттера Name")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "Цепь 1";
            var circuit = new Circuit("Новая цепь");
            circuit.Name = expected;
            var actual = circuit.Name;

            Assert.AreEqual(expected, actual, "Сеттер Name присваивает неправильное значение");
        }

        [TestCase("", "Должно возникать исключение, если имя - пустая строка",
            TestName = "Присвоение пустой строки в качестве имени")]
        [TestCase(null, "Должно возникать исключение, если имя = null",
            TestName = "Присвоение null в качестве имени")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            var circuit = new Circuit("Новая цепь");

            Assert.Throws<ArgumentException>(
                () => { circuit.Name = wrongName; },
                message);
        }

        [Test(Description = "Позитивный тест геттера Segments")]
        public void TestSegmentsGet_CorrectValue()
        {
            var expected = GetExampleList();
            var circuit = new Circuit("Новая цепь");
            circuit.Segments = expected;
            var actual = circuit.Segments;

            CollectionAssert.AreEqual(expected, actual,
                "Геттер Segments возвращает направильный список сегментов");
        }

        [Test(Description = "Позитивный тест сеттера Segments")]
        public void TestSegmentsSet_CorrectValue()
        {
            var expected = GetExampleList();
            var circuit = new Circuit("Новая цепь");
            circuit.Segments = expected;
            var actual = circuit.Segments;

            CollectionAssert.AreEqual(expected, actual,
                "Сеттер Segments присваивает направильный список сегментов");
        }

        [Test(Description = "Позитивный тест конструктора Circuit")]
        public void TestCircuitConstructor_CorrectValue()
        {
            var circuit = new Circuit("Новая цепь");
            var isNull = circuit == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса Circuit");
        }

        [Test(Description = "Вызов события CircuitChanged при добавлении элемента в цепь")]
        public void TestCircuitChangedInvoke_AddElement()
        {
            var wasCalled = false;
            var circuit = new Circuit("Цепь 1");

            circuit.CircuitChanged += (o, e) => wasCalled = true;
            circuit.Segments.Add(new Inductor("L1", 2e-4));

            Assert.IsTrue(wasCalled,
                "При добавлении элемента в цепь должно вызываться событие CircuitChanged");
        }

        [Test(Description = "Вызов события CircuitChanged при удалении элемента из цепи")]
        public void TestCircuitChangedInvoke_RemoveElement()
        {
            var wasCalled = false;
            var circuit = new Circuit("Цепь 1");
            circuit.Segments.Add(new Inductor("L1", 2e-4));

            circuit.CircuitChanged += (o, e) => wasCalled = true;
            circuit.Segments.RemoveAt(0);

            Assert.IsTrue(wasCalled,
                "При удалении элемента из цепи должно вызываться событие CircuitChanged");
        }

        [Test(Description = "Вызов события CircuitChanged при замене элемента в цепи")]
        public void TestCircuitChangedInvoke_ReplaceElement()
        {
            var wasCalled = false;
            var circuit = new Circuit("Цепь 1");
            circuit.Segments.Add(new Inductor("L1", 2e-4));

            circuit.CircuitChanged += (o, e) => wasCalled = true;
            circuit.Segments[0] = new Capacitor("C1", 2e-6);

            Assert.IsTrue(wasCalled,
                "При замене элемента в цепи должно вызываться событие CircuitChanged");
        }

        [Test(Description = "Вызов события CircuitChanged изменении номинала элемента цепи")]
        public void TestCircuitChangedInvoke_ChangeElementValue()
        {
            var wasCalled = false;
            var circuit = new Circuit("Цепь 1");

            var element = new Resistor("R1", 40);
            circuit.CircuitChanged += (o, e) => wasCalled = true;
            circuit.Segments.Add(element);

            wasCalled = false;
            element.Value = 50;

            Assert.IsTrue(wasCalled,
                "При изменении номинала элемента цепи должно вызываться событие CircuitChanged");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
        public void TestCalculateZ_CorrectValue()
        {
            var resistor = new Resistor("R1", 20);
            var inductor = new Inductor("L1", 2e-3);
            var capacitor = new Capacitor("C1", 2e-6);
            var frequencies = new List<double>()
            {
                100,
                200,
                300
            };

            var serialSegment = new SerialSegment();
            serialSegment.SubSegments.Add(resistor);
            serialSegment.SubSegments.Add(inductor);
            serialSegment.SubSegments.Add(capacitor);

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(resistor);
            parallelSegment.SubSegments.Add(inductor);
            parallelSegment.SubSegments.Add(capacitor);

            var circuit = new Circuit("Новая цепь");
            circuit.Segments.Add(serialSegment);
            circuit.Segments.Add(parallelSegment);

            var impedances = new List<Complex>();
            foreach (var frequency in frequencies)
            {
                impedances.Add(serialSegment.CalculateZ(frequency) +
                               parallelSegment.CalculateZ(frequency));
            }
            
            var expected = impedances.ToArray();
            var actual = circuit.CalculateZ(frequencies);

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "Цепь 1";
            var circuit = new Circuit("Цепь 1");
            var actual = circuit.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }
    }
}