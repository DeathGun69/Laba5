using System;
using System.Xml.Serialization;

namespace KPO_laba5
{
    // Класс описывающий цены на звонки
    public class CallPrices
    {
        // Внутри региона (сети)
        [XmlElement]
        double insideNetwork;
        
        // Вне региона (сети)
        [XmlElement]
        double outsideNetwork;

        // На домашние телефоны
        [XmlElement]
        double fixedPhone;

        public CallPrices() { }

        public CallPrices(double insNet, double outNet, double fPhone)
        {
            this.insideNetwork = insNet;
            this.outsideNetwork = outNet;
            this.fixedPhone = fPhone;
        }

        // Геттеры и Сетторы
        public double getInsideNetwork()
        {
            return insideNetwork;
        }
        public void setInsideNetwork(double insNet)
        {
            this.insideNetwork = insNet;
        }

        public double getOutsideNetwork()
        {
            return outsideNetwork;
        }
        public void setOutsideNetwork(double outNet)
        {
            this.outsideNetwork = outNet;
        }

        public double getFixedPhone()
        {
            return fixedPhone;
        }
        public void setFixedPhone(double fPhone)
        {
            this.fixedPhone = fPhone;
        }
    }
}