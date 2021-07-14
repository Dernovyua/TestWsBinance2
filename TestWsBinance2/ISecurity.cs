using System;
using System.Collections.Generic;

namespace SourceEts.Table
{
    /// <summary>
    /// Интерфейс для получении информации по инструменту
    /// </summary>
    public interface ISecurity
    {
        /// <summary>
        /// КриптоИнструмент
        /// </summary>
        bool IsCrypto { get; set; }

        /// <summary>
        /// цифровой код инструмента
        /// </summary>
        string Isin { get; set; }

        /// <summary>
        /// код инструмента
        /// </summary>
        string Seccode { get; set; }

        /// <summary>
        /// Краткое название инструмента
        /// </summary>
        string ShortName { get; set; }

        /// <summary>
        /// класс бумаги
        /// </summary>
        string ClassCode { get; set; }
        /// <summary>
        /// точность, количество знаков после запятой
        /// </summary>
        int Accuracy { get; set; }
        /// <summary>
        /// Минимальный объем 
        /// </summary>
        Double MinAmount { get; set; }
        /// <summary>
        /// Максимальный объем 
        /// </summary>
        Double MaxAmount { get; set; }
        /// <summary>
        /// Шаг цены 
        /// </summary>
        Double MinStep { get; set; }
        /// <summary>
        /// максимально возможная цена по инструменту, для выставлении заявки
        /// </summary>
        Double MaxPrice { get; set; }
        /// <summary>
        /// Минимально возможная цена по инструменту, для выставлении заявки
        /// </summary>
        Double MinPrice { get; set; }
        /// <summary>
        /// цена последней сделки
        /// </summary>
        Double LastPrice { get; set; }
        /// <summary>
        /// цена предпоследней сделки
        /// </summary>
        Double PrevLastPrice { get; set; }
        /// <summary>
        /// изменение в процентах от цены закрытия предыдущей сессии
        /// </summary>
        Double LastChangePercent { get; set; }
        /// <summary>
        /// цена закрытия
        /// </summary>
        Double ClosePrice { get; set; }
        /// <summary>
        /// цена Открытия
        /// </summary>
        Double OpenPrice { get; set; }
        /// <summary>
        /// ГО продовца
        /// </summary>
        Double GoSell { get; set; }
        /// <summary>
        /// ГО покупателя
        /// </summary>
        Double GoBuy { get; set; }
        /// <summary>
        /// стоимость шага цены
        /// </summary>
        Double PointCost { get; set; }
        /// <summary>
        /// Статус торговли
        /// </summary>
        string TradingStatus { get; set; }
        /// <summary>
        /// статус сессии
        /// </summary>
        string Status { get; set; }
        /// <summary>
        /// Можно ли торговать по инструменту.
        /// </summary>
        bool IsTrade { get; set; }
        /// <summary>
        /// величина лота
        /// </summary>
        double LotSize { get; set; }
        /// <summary>
        /// точность расчет объема, количество знаков после запятой
        /// </summary>
        int LotSizeAccuracy { get; set; }
        /// <summary>
        /// Минимальная сумма сделки
        /// </summary>
        double MinNational { get; set; }
        /// <summary>
        /// Бид
        /// </summary>
        Double Bid { get; set; }
        /// <summary>
        /// Оффер
        /// </summary>
        Double Offer { get; set; }
        /// <summary>
        /// Время последнего обновления
        /// </summary>
        DateTime TimeLastChange { get; set; }

        /// <summary>
        /// Price Quotation. Данное поле заполняется, если фьючерсный или другой контракт имеет не стандартное вычилсение стоимости. 
        /// Пример контракт ZB - Points ($1,000) and 1/32 of a point. For example, 134-16 represents 134 16/32. Par is on the basis of 100 points. 
        /// В этом случае в поле заносится значение 320
        /// </summary>
        double PriceQuotation { get; set; }

        /// <summary>
        /// теоретическая цена опциона
        /// </summary>
        Double TheorPrice { get; set; }
        /// <summary>
        /// Базавый актив
        /// </summary>
        string BaseActive { get; set; }
        /// <summary>
        /// тип опциона
        /// </summary>
        string TypeOption { get; set; }
        /// <summary>
        /// Базавый актив
        /// </summary>
        Double Strike { get; set; }
        /// <summary>
        /// Базавый актив
        /// </summary>
        DateTime DateExpire { get; set; }

        /// <summary>
        /// Волатильность опциона
        /// </summary>
        double Volatility { get; set; }

        /// <summary>
        /// Последний номер сделки (тик)
        /// </summary>
        string LastNumberTrade { get; set; }

        /// <summary>
        /// Набор допольнительных параметров для конкретного темринала, для доступа в роботе
        /// </summary>
        Dictionary<string, object> DicValue { get; }



        /// <summary>
        /// Заявок на покупку
        /// </summary>
        int NumBids { get; set; }

        /// <summary>
        /// Заявок на продажу
        /// </summary>
        int NumOffers { get; set; }

        /// <summary>
        /// Кол-во сделок
        /// </summary>
        int NumTrades { get; set; }

        /// <summary>
        /// Оборот в деньгах
        /// </summary>
        double VolatToday { get; set; }

        #region Облигации

        /// <summary>
        /// Дата выплаты следующего купона
        /// </summary>
        DateTime NextCoupon { get; set; }

        /// <summary>
        /// Дата погашения
        /// </summary>
        DateTime MatDate { get; set; }

        /// <summary>
        /// Длительность купона
        /// </summary>
        int CouponPeriod { get; set; }

        /// <summary>
        /// Доходность
        /// </summary>
        double Yield { get; set; }
        
        /// <summary>
        /// Дюрация
        /// </summary>
        double Duration { get; set; }

        /// <summary>
        /// НКД
        /// </summary>
        double Accuedint { get; set; }

        /// <summary>
        /// Номанал облигации
        /// </summary>
        double SecFaceValue { get; set; }

        /// <summary>
        /// Размер купона
        /// </summary>
        double CouponValue { get; set; }

        /// <summary>
        /// Тип инструмента
        /// </summary>
        string SecType { get; set; }

        /// <summary>
        /// Тип цены 
        /// </summary>
        string QuotesBasis { get; set; }

        /// <summary>
        /// Тип цены облиагации
        /// </summary>
        string TypePriceBond { get; set; }

        /// <summary>
        /// Дней до погашения облигации
        /// </summary>
        int DaysToMatDateBond { get; set; }



	    #endregion

    }
}
