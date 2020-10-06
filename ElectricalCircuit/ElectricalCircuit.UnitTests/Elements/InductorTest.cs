using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class InductorTest
    {
        [Test(Description = "Позитивный тест конструктора Inductor")]
        public void TestInductorConstructor_CorrectValue()
        {
            var inductor = new Inductor("L1", 2e-3);
            var isNull = inductor == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса Inductor");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
        public void TestCalculateZ_CorrectValue()
        {
            var inductor = new Inductor("L1", 0.002);
            var impedance = inductor.CalculateZ(100);

            var expected = 1.25664.ToString();
            var actual = impedance.Imaginary.ToString("F5");

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "L1";
            var inductor = new Inductor("L1", 0.002);
            var actual = inductor.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }
    }
}