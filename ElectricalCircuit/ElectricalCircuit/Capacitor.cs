using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Capacitor"/>, хранящий информацию о конденсаторе
    /// </summary>
    public class Capacitor : Element
    {
        /// <summary>
        /// Метод для расчета импеданса элемента
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = 1 / (2 * Math.PI * frequency * Value * Complex.ImaginaryOne);
            return impedance;
        }

        /// <summary>
        /// Создает экземпляр <see cref="Capacitor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Capacitor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(Capacitor)} {Name}, номинал = {Value} Ф";
        }
    }
}