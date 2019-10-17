using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace KPO_laba5
{
    public class SAXParser
    {
        private bool Validation;
        private string xml_file;
        private string xsd_file;
        
        public SAXParser(string xml_Doc, string xsd_Doc)
        {
            this.xml_file = xml_Doc;
            this.xsd_file = xsd_Doc;
        }

        // Метод выводящий ошибки и предупреждения при валидации
        private void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            } 
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }

            Validation = false;
        }

        // Метод который отвечает за парсинг файла XML
        public List<Tariff> XML_TariffSAX()
        {
            List<Tariff> tariff_List = new List<Tariff>();
            Validation = true;
            XmlReaderSettings xmlSetting = new XmlReaderSettings();
            xmlSetting.Schemas.Add("http://www.example.com/students", xsd_file);
            xmlSetting.ValidationType = ValidationType.Schema;
            xmlSetting.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);
            Tariff tariff = new Tariff();
            CallPrices callPrices = new CallPrices();
            Parameter parameter = new Parameter();

            using(XmlReader pars = XmlReader.Create(xml_file, xmlSetting))
            {
                try {
                    while (pars.Read()) 
                    {
                        if (pars.HasAttributes)
                        {
                            // Получение атрибутов
                            while (pars.MoveToNextAttribute())
                            {
                                if (pars.Name == "name")
                                {
                                    tariff.setName(pars.Value);
                                }
                                if (pars.Name == "operatorName")
                                {
                                    tariff.setOperatorName(pars.Value);
                                }
                            }
                        }

                        // Получаем значения элементов из файла XML
                        switch (pars.NodeType)
                        {
                            case XmlNodeType.Element:
                                switch (pars.Name)
                                {
                                    case "payroll":
                                        pars.Read();
                                        tariff.setPayroll(Convert.ToDouble(pars.Value));
                                        break;
                                    case "callPrices":
                                        pars.Read();
                                        tariff.setCallPrices(callPrices);
                                        break;
                                    case "insideNetwork":
                                        pars.Read();
                                        tariff.getCallPrices().setInsideNetwork(Convert.ToDouble(pars.Value));
                                        break;
                                    case "outsideNetwork":
                                        pars.Read();
                                        tariff.getCallPrices().setOutsideNetwork(Convert.ToDouble(pars.Value));
                                        break;
                                    case "fixedPhone":
                                        pars.Read();
                                        tariff.getCallPrices().setFixedPhone(Convert.ToDouble(pars.Value));
                                        break;
                                    case "smsPrice":
                                        pars.Read();
                                        tariff.setSmsPrice(Convert.ToDouble(pars.Value));
                                        break;
                                    case "parameter":
                                        pars.Read();
                                        tariff.setParameter(parameter);
                                        break;
                                    case "favoriteNumber":
                                        pars.Read();
                                        tariff.getParameter().setFavoriteNumber(Convert.ToInt64(pars.Value));
                                        break;
                                    case "tariffication":
                                        pars.Read();
                                        tariff.getParameter().setTariffication(pars.Value);
                                        break;
                                    case "connectionFee":
                                        pars.Read();
                                        tariff.getParameter().setConnectionFee(Convert.ToDouble(pars.Value));
                                        break;
                                }
                            break;

                            // Добавляем в список объект
                            case XmlNodeType.EndElement:
                                if (pars.Name == "tariff")
                                {
                                    tariff_List.Add(tariff);
                                    tariff = new Tariff();
                                    parameter = new Parameter();
                                    callPrices = new CallPrices();
                                }
                            break;    
                        }
                    }
                }
                catch (Exception)
                {
                    Validation = false;
                }
            }

            // Возвращаем список тарифов
            return tariff_List;
        }
    }
}