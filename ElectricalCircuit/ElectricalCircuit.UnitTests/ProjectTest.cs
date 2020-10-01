using System.Collections.Generic;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests
{
    [TestFixture]
    public class ProjectTest
    {
        /// <summary>
        /// Метод для создания списка цепей на 4 элемента
        /// </summary>
        /// <returns></returns>
        private List<Circuit> GetExampleList()
        {
            return new List<Circuit>()
            {
                new Circuit("Цепь 1"),
                new Circuit("Цепь 2"),
                new Circuit("Цепь 3"),
                new Circuit("Цепь 4")
            };
        }

        [Test(Description = "Позитивный тест геттера Circuits")]
        public void TestCircuitsGet_CorrectValue()
        {
            var expected = GetExampleList();
            var project = new Project();
            project.Circuits = expected;
            var actual = project.Circuits;

            CollectionAssert.AreEqual(expected, actual,
                "Геттер Circuits возвращает направильный список цепей");
        }

        [Test(Description = "Позитивный тест сеттера Circuits")]
        public void TestCircuitsSet_CorrectValue()
        {
            var expected = GetExampleList();
            var project = new Project();
            project.Circuits = expected;
            var actual = project.Circuits;

            CollectionAssert.AreEqual(expected, actual,
                "Сеттер Circuits присваивает направильный список цепей");
        }

        [Test(Description = "Позитивный тест конструктора Project")]
        public void TestProjectConstructor_CorrectValue()
        {
            var project = new Project();
            var isNull = project == null;

            Assert.IsFalse(isNull, "Конструктор не создал экземпляр класса Project");
        }
    }
}