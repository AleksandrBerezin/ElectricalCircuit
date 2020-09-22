using System;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Интерфейс <see cref="IElement"/> определяет поля и методы для работы с элементами
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Возвращает и задает название элемента
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Возвращает и задает номинал элемента
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Метод для расчета импеданса элемента
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        Complex CalculateZ(double frequency);

        /// <summary>
        /// Сообщает об изменении номинала элемента
        /// </summary>
        event EventHandler ValueChanged;
    }
}