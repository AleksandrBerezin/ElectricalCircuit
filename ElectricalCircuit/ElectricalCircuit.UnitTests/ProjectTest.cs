using System.Collections.Generic;
using NUnit.Framework;

namespace ElectricalCircuit.UnitTests
{
    [TestFixture]
    public class ProjectTest
    {
        /// <summary>
        /// Create example list of 4 circuits
        /// </summary>
        /// <returns></returns>
        private List<Circuit> GetExampleList()
        {
            return new List<Circuit>()
            {
                new Circuit("Circuit 1"),
                new Circuit("Circuit 2"),
                new Circuit("Circuit 3"),
                new Circuit("Circuit 4")
            };
        }

        [Test(Description = "Positive test of getter Circuits")]
        public void TestCircuitsGet_CorrectValue()
        {
            var expected = GetExampleList();
            var project = new Project();
            project.Circuits = expected;
            var actual = project.Circuits;

            CollectionAssert.AreEqual(expected, actual,
                "Getter Circuits returns the wrong value");
        }

        [Test(Description = "Positive test of setter Circuits")]
        public void TestCircuitsSet_CorrectValue()
        {
            var expected = GetExampleList();
            var project = new Project();
            project.Circuits = expected;
            var actual = project.Circuits;

            CollectionAssert.AreEqual(expected, actual,
                "Setter Circuits assigns the wrong value");
        }

        [Test(Description = "Positive test of Project constructor")]
        public void TestProjectConstructor_CorrectValue()
        {
            var project = new Project();
            var isNull = project == null;

            Assert.IsFalse(isNull, "Constructor didn't create an instance of  the Project");
        }
    }
}