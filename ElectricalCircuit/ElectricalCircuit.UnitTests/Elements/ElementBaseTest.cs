using System;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests.Elements
{
    [TestFixture]
    public class ElementBaseTest
    {
        [Test(Description = "Позитивный тест геттера Name")]
        public void TestNameGet_CorrectValue()
        {
            var expected = "Новый элемент";
            var element = new Element();
            element.Name = expected;
            var actual = element.Name;

            Assert.AreEqual(expected, actual, "Геттер Name возвращает неправильное значение");
        }

        [Test(Description = "Позитивный тест сеттера Name")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "Новый элемент";
            var element = new Element();
            element.Name = expected;
            var actual = element.Name;

            Assert.AreEqual(expected, actual, "Сеттер Name присваивает неправильное значение");
        }

        [TestCase("", "Должно возникать исключение, если имя - пустая строка",
            TestName = "Присвоение пустой строки в качестве имени")]
        [TestCase(null, "Должно возникать исключение, если имя = null",
            TestName = "Присвоение null в качестве имени")]
        public void TestNameSet_ArgumentException(string wrongName, string message)
        {
            var element = new Element();

            Assert.Throws<ArgumentException>(
                () => { element.Name = wrongName; },
                    message);
        }

        [Test(Description = "Позитивный тест геттера Value")]
        public void TestValueGet_CorrectValue()
        {
            var expected = 14.345;
            var element = new Element();
            element.Value = expected;
            var actual = element.Value;

            Assert.AreEqual(expected, actual, "Геттер Value возвращает неправильное значение");
        }

        [Test(Description = "Позитивный тест сеттера Value")]
        public void TestValueSet_CorrectValue()
        {
            var expected = 14.345;
            var element = new Element();
            element.Value = expected;
            var actual = element.Value;

            Assert.AreEqual(expected, actual, "Сеттер Value присваивает неправильное значение");
        }

        [Test(Description = "Присвоение отрицательного числа в качестве значения")]
        public void TestValueSet_NegativeValue()
        {
            var wrongValue = -0.345;
            var element = new Element();

            Assert.Throws<ArgumentException>(
                () => { element.Value = wrongValue; },
                "Должно возникать исключение, если значение отрицательное");
        }

        [Test(Description = "Позитивный тест вызова события SegmentChanged")]
        public void TestSegmentChangedInvoke_CorrectValue()
        {
            var wasCalled = false;
            var element = new Element()
            {
                Name = "Элемент 1",
                Value = 20
            };

            element.SegmentChanged += (o, e) => wasCalled = true;
            element.Value = 30;

            Assert.IsTrue(wasCalled,
                "При изменении номинала элемента должно вызываться событие SegmentChanged");
        }
    }
}