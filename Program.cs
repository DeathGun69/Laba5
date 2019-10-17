using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace KPO_laba5
{
    class Program
    {
        static void Main(string[] args)
        {
            // Списки для заполнения 
            List<Tariff> tariffList_1 = new List<Tariff>();
            List<Tariff> tariffList_2 = new List<Tariff>();

            string XML_File = @"C:\Users\Сергей\Documents\Учеба\ИрГУПС\Курганская\Конструирование ПО\KPO_laba5\tariff.xml";
            string XSD_File = @"C:\Users\Сергей\Documents\Учеба\ИрГУПС\Курганская\Конструирование ПО\KPO_laba5\tariff.xsd";

            // Парсинг SAX
            SAXParser saxParser = new SAXParser(XML_File, XSD_File);
            tariffList_1 = saxParser.XML_TariffSAX();

            // Вывод данных
            foreach (Tariff item in tariffList_1)
            {
                Console.WriteLine("\nНазвание тарифа: " + item.getName());
                Console.WriteLine("Оператор: " + item.getOperatorName());
                Console.WriteLine("Цена тарифа: " + item.getPayroll());
                Console.WriteLine("Внутри региона: " + item.getCallPrices().getInsideNetwork());
                Console.WriteLine("Вне региона: " + item.getCallPrices().getOutsideNetwork());
                Console.WriteLine("На домашние телефоны: " + item.getCallPrices().getFixedPhone());
                Console.WriteLine("Цена за SMS: " + item.getSmsPrice());
                Console.WriteLine("Любимый номер: " + item.getParameter().getFavoriteNumber());
                Console.WriteLine("Тарификация: " + item.getParameter().getTariffication());
                Console.WriteLine("Цена подключения: " + item.getParameter().getConnectionFee());
            }
            Console.WriteLine("--------------------------------------");
            
            // Парсинг DOM
            DOMParser domParser = new DOMParser(XML_File, XSD_File);
            tariffList_2 = domParser.XML_TariffDOM();

            // Вывод данных
            foreach (Tariff item in tariffList_2)
            {
                Console.WriteLine("\nНазвание тарифа: " + item.getName());
                Console.WriteLine("Оператор: " + item.getOperatorName());
                Console.WriteLine("Цена тарифа: " + item.getPayroll());
                Console.WriteLine("Внутри региона: " + item.getCallPrices().getInsideNetwork());
                Console.WriteLine("Вне региона: " + item.getCallPrices().getOutsideNetwork());
                Console.WriteLine("На домашние телефоны: " + item.getCallPrices().getFixedPhone());
                Console.WriteLine("Цена за SMS: " + item.getSmsPrice());
                Console.WriteLine("Любимый номер: " + item.getParameter().getFavoriteNumber());
                Console.WriteLine("Тарификация: " + item.getParameter().getTariffication());
                Console.WriteLine("Цена подключения: " + item.getParameter().getConnectionFee());
            }
        }
    }
}
