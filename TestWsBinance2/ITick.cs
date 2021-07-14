using System;

namespace SourceEts.Table
{
    /// <summary>
    /// Используется для таблицы всех сделок и тиков
    /// </summary>
    public interface ITick
    {
        /// <summary>
        /// Номер тика/сделки
        /// </summary>
        long TradeNum { get; set; }
        /// <summary>
        /// Дата время тика/сделки
        /// </summary>
        DateTime TradeDateTime { get; set; }
        /// <summary>
        /// Код инструмента
        /// </summary>
        string Seccode { get; set; }
        /// <summary>
        /// Код класса инструмента
        /// </summary>
        string ClassCode { get; set; }
        /// <summary>
        /// Цена тика/сделки
        /// </summary>
        double Price { get; set; }
        /// <summary>
        /// Количество лотов/контрактов в сделке
        /// </summary>
        double Qty { get; set; }
        /// <summary>
        /// Объем лотов/контрактов * цену
        /// </summary>
        double Volume { get; set; }
        /// <summary>
        /// Направление сделки 'B'-Buy 'S'-Sell
        /// </summary>
        char BuySell { get; set; }
        /// <summary>
        /// Открытый интерес
        /// </summary>
        int Oi { get; set; }
    }
}
