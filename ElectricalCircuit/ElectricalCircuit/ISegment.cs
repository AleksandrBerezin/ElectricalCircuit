using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Интерфейс <see cref="ISegment"/> определяет поля и методы для работы с сегментами цепей
    /// </summary>
    public interface ISegment
    {
        /// <summary>
        /// Возвращает и задает список внутренних сегментов
        /// </summary>
        ObservableCollection<ISegment> SubSegments { get; }

        /// <summary>
        /// Метод для расчета импеданса участка цепи
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        Complex CalculateZ(double frequency);

        /// <summary>
        /// Сообщает об изменении сегмента цепи
        /// </summary>
        event EventHandler SegmentChanged;
    }
}