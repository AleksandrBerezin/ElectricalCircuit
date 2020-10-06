﻿using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Класс <see cref="Resistor"/>, хранящий информацию о резисторе
    /// </summary>
    public class Resistor : ElementBase
    {
        /// <summary>
        /// Создает экземпляр <see cref="Resistor"/>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public Resistor(string name, double value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Создает экземпляр <see cref="Resistor"/>
        /// </summary>
        public Resistor()
        {

        }

        /// <inheritdoc/>
        public override Complex CalculateZ(double frequency)
        {
            return new Complex(Value, 0);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Name;
        }
    }
}