using System;
using System.Collections.ObjectModel;
using System.Numerics;

namespace ElectricalCircuit
{
    /// <summary>
    /// Интерфейс <see cref="ISegment"/> определяет поля и методы для работы с сегментами цепей
    /// </summary>
    public interface ISegment : ICloneable
    {
        /// <summary>
        /// Возвращает список внутренних сегментов
        /// </summary>
        ObservableCollection<ISegment> SubSegments { get; }
        
        /// <summary>
        /// Сообщает об изменении сегмента цепи
        /// </summary>
        event EventHandler SegmentChanged;

        /// <summary>
        /// Метод для расчета импеданса участка цепи
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns></returns>
        Complex CalculateZ(double frequency);
    }
}