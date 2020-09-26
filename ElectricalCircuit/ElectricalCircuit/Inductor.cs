﻿using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Inductor"/>, хранящий информацию о катушке индуктивности
    /// </summary>
    public class Inductor : Element
    {
        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            var impedance = 2 * Math.PI * frequency * Value * Complex.ImaginaryOne;
            return impedance;
        }

        /// <summary>
        /// Создает экземпляр <see cref="Inductor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Inductor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Катушка {Name}, номинал = {Value} Гн";
        }
    }
}