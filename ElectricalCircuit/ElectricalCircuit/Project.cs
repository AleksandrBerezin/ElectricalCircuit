using System.Collections.Generic;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Project"/>, хранящий список цепей
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Возвращает и задает список цепей
        /// </summary>
        public List<Circuit> Circuits { get; set; }

        /// <summary>
        /// Создает экземпляр <see cref="Project"/>
        /// </summary>
        public Project()
        {
            Circuits = new List<Circuit>
            {
                Circuit0(),
                Circuit1(),
                Circuit2(),
                Circuit3(),
                Circuit4()
            };
        }

        /// <summary>
        /// Метод создания цепи 0
        /// </summary>
        /// <returns></returns>
        private Circuit Circuit0()
        {
            var circuit = new Circuit("Цепь 0");
            circuit.SubSegments.Add(new Resistor("R1", 15));
            circuit.SubSegments.Add(new Inductor("L1", 0.01));
            circuit.SubSegments.Add(new Inductor("L2", 0.02));
            circuit.SubSegments.Add(new Resistor("R2", 20));
            circuit.SubSegments.Add(new Capacitor("C1", 15e-6));
            circuit.SubSegments.Add(new Resistor("R3", 40));
            circuit.SubSegments.Add(new Capacitor("C2", 40e-6));
            var parallelSegment = new ParallelSegment();

            circuit.SubSegments.Add(parallelSegment);

            return circuit;
        }

        /// <summary>
        /// Метод создания цепи 1
        /// </summary>
        /// <returns></returns>
        private Circuit Circuit1()
        {
            var circuit = new Circuit("Цепь 1");
            circuit.SubSegments.Add(new Resistor("R1", 15));
            circuit.SubSegments.Add(new Resistor("R2", 10));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Resistor("R3", 20));
            parallelSegment.SubSegments.Add(new Resistor("R4", 20));

            var serialSegment = new SerialSegment();
            serialSegment.SubSegments.Add(new Resistor("R5", 40));
            serialSegment.SubSegments.Add(new Resistor("R6", 40));

            parallelSegment.SubSegments.Add(serialSegment);
            circuit.SubSegments.Add(parallelSegment);
            circuit.SubSegments.Add(new Resistor("R7", 15));

            return circuit;
        }

        /// <summary>
        /// Метод создания цепи 2
        /// </summary>
        /// <returns></returns>
        private Circuit Circuit2()
        {
            var circuit = new Circuit("Цепь 2");
            circuit.SubSegments.Add(new Inductor("L1", 0.015));
            circuit.SubSegments.Add(new Inductor("L2", 0.01));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Inductor("L3", 0.02));
            parallelSegment.SubSegments.Add(new Inductor("L4", 0.02));
            circuit.SubSegments.Add(parallelSegment);

            circuit.SubSegments.Add(new Inductor("L5", 0.015));

            parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Inductor("L6", 0.04));
            parallelSegment.SubSegments.Add(new Inductor("L7", 0.04));
            circuit.SubSegments.Add(parallelSegment);

            return circuit;
        }

        /// <summary>
        /// Метод создания цепи 3
        /// </summary>
        /// <returns></returns>
        private Circuit Circuit3()
        {
            var circuit = new Circuit("Цепь 3");
            circuit.SubSegments.Add(new Capacitor("C1", 15e-6));
            circuit.SubSegments.Add(new Capacitor("C2", 10e-6));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C3", 20e-6));
            parallelSegment.SubSegments.Add(new Capacitor("C4", 20e-6));
            circuit.SubSegments.Add(parallelSegment);

            circuit.SubSegments.Add(new Capacitor("C5", 15e-6));

            parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C6", 40e-6));
            parallelSegment.SubSegments.Add(new Capacitor("C7", 40e-6));
            circuit.SubSegments.Add(parallelSegment);

            return circuit;
        }

        /// <summary>
        /// Метод создания цепи 4
        /// </summary>
        /// <returns></returns>
        private Circuit Circuit4()
        {
            var circuit = new Circuit("Цепь 4");
            circuit.SubSegments.Add(new Resistor("R1", 15));
            circuit.SubSegments.Add(new Inductor("L1", 0.01));

            var parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Capacitor("C1", 20e-6));
            parallelSegment.SubSegments.Add(new Resistor("R2", 20));
            circuit.SubSegments.Add(parallelSegment);

            circuit.SubSegments.Add(new Inductor("L2", 0.015));

            parallelSegment = new ParallelSegment();
            parallelSegment.SubSegments.Add(new Inductor("L3", 0.04));
            parallelSegment.SubSegments.Add(new Capacitor("C2", 40e-6));
            circuit.SubSegments.Add(parallelSegment);

            return circuit;
        }
    }
}