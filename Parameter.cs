using System;
using System.Xml.Serialization;

namespace KPO_laba5
{
    // Класс описывающий параметры тарифа
    public class Parameter
    {
        // Любимый номер
        [XmlElement]
        private long favoriteNumber;
        
        // Тарификация
        [XmlText]
        private string tariffication;

        // Цена подключения
        [XmlElement]
        private double connectionFee;

        public Parameter() { }

        public Parameter(long favNum, string tarif, double conFee)
        {
            this.favoriteNumber = favNum;
            this.tariffication = tarif;
            this.connectionFee = conFee;
        }

        // Геттеры и Сетторы
        public long getFavoriteNumber()
        {
            return favoriteNumber;
        }
        public void setFavoriteNumber(long favNum)
        {
            this.favoriteNumber = favNum;
        }

        public string getTariffication()
        {
            return tariffication;
        }
        public void setTariffication(string tarif)
        {
            this.tariffication = tarif;
        }

        public double getConnectionFee()
        {
            return connectionFee;
        }
        public void setConnectionFee(double conFee)
        {
            this.connectionFee = conFee;
        }
    }
}