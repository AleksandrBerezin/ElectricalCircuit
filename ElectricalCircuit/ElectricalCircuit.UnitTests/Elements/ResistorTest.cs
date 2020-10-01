using System.Numerics;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class ResistorTest
    {
        [Test(Description = "Позитивный тест конструктора Resistor")]
        public void TestResistorConstructor_CorrectValue()
        {
            var resistor = new Resistor("R1", 20);
            var isNull = resistor == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса Resistor");
        }

        [Test(Description = "Позитивный тест метода расчета импеданса")]
        public void TestCalculateZ_CorrectValue()
        {
            var value = 20;
            var expected = new Complex(value, 0);
            var resistor = new Resistor("R1", value);
            var actual = resistor.CalculateZ(100);

            Assert.AreEqual(expected, actual, "Метод CalculateZ считает импеданс неправильно");
        }

        [Test(Description = "Позитивный тест метода ToString")]
        public void TestToString_CorrectValue()
        {
            var expected = "Резистор R1 = 20 Ом";
            var resistor = new Resistor("R1", 20);
            var actual = resistor.ToString();

            Assert.AreEqual(expected, actual, "Метод ToString возвращает неправильную строку");
        }
    }
}