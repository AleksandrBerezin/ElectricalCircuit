using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class CapacitorTest
    {
        [Test(Description = "Позитивный тест конструктора Capacitor")]
        public void TestCapacitorConstructor_CorrectValue()
        {
            var capacitor = new Capacitor("C1", 2e-6);
            var isNull = capacitor == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса Capacitor");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
        public void TestCalculateZ_CorrectValue()
        {
            var capacitor = new Capacitor("C1", 15e-6);
            var impedance = capacitor.CalculateZ(100);

            var expected = (-106.1033).ToString();
            var actual = impedance.Imaginary.ToString("F4");

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "Конденсатор C1 = 2E-06 Ф";
            var capacitor = new Capacitor("C1", 2e-6);
            var actual = capacitor.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }
    }
}