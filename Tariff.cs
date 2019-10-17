using System;
using System.Xml;
using System.Xml.Serialization;

namespace KPO_laba5
{
    // Класс описывающий тариф, который записан в XML-файле
    public class Tariff
    {
        // Наименование тарифа
        [XmlAttribute]
        string name;

        // Наименование оператора
        [XmlAttribute]
        string operatorName;

        // Ежемесячная плата
        [XmlElement]
        double payroll;

        // Цена тарифа
        [XmlElement]
        CallPrices callPrices = new CallPrices();

        // Цена за SMS
        [XmlElement]
        double smsPrice;

        // Параметры тарифа
        [XmlElement]
        Parameter parameter = new Parameter();

        public Tariff() { }
        
        public Tariff(string name, string operName, double payroll, CallPrices callPr, double smsPr, Parameter parameter)
        { 
            this.name = name;
            this.operatorName = operName;
            this.payroll = payroll;
            this.callPrices = callPr;
            this.smsPrice = smsPr;
            this.parameter = parameter;
        }

        // Геттеры и Сетторы
        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }

        public string getOperatorName()
        {
            return operatorName;
        }
        public void setOperatorName(string opName)
        {
            this.operatorName = opName;
        }

        public double getPayroll()
        {
            return payroll;
        }
        public void setPayroll(double payroll)
        {
            this.payroll = payroll;
        }

        public CallPrices getCallPrices()
        {
            return callPrices;
        }
        public void setCallPrices(CallPrices call)
        {
            this.callPrices = call;
        }

        public double getSmsPrice()
        {
            return smsPrice;
        }
        public void setSmsPrice(double sms_P)
        {
            this.smsPrice = sms_P;
        }

        public Parameter getParameter()
        {
            return parameter;
        }
        public void setParameter(Parameter param)
        {
            this.parameter = param;
        }
    }
}