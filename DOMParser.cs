using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace KPO_laba5
{
    public class DOMParser
    {
        private bool Validation;
        private string xml_file;
        private string xsd_file;
        
        public DOMParser(string xml_Doc, string xsd_Doc)
        {
            xml_file = xml_Doc;
            xsd_file = xsd_Doc;
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

        // Метод для валидации XML-файла по XSD-файлу
        public bool ValidXML()
        {
            Validation = true;
            XmlReaderSettings xmlSetting = new XmlReaderSettings();
            xmlSetting.Schemas.Add("http://www.example.com/students", xsd_file);
            xmlSetting.ValidationType = ValidationType.Schema;
            xmlSetting.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);
            
            XmlReader pars = XmlReader.Create(xsd_file, xmlSetting);
            try {
                while (pars.Read())
                {   }
                pars.Close();
            }
            catch (Exception)
            {
                Validation = false;
                pars.Close();
            }
            return Validation;
        }

        public bool getValidation()
        {
            return Validation;
        }

        // Метод возвращающий список тарифов
        public List<Tariff> XML_TariffDOM()
        {
            List<Tariff> tariffList = new List<Tariff>();

            ValidXML();
            if (Validation == true)
            {
                try
                {
                    XmlDocument docXML = new XmlDocument();
                    docXML.Load(xml_file);
                    XmlElement elemXML = docXML.DocumentElement;
                    foreach (XmlNode nodeXMl in elemXML)
                    {
                        tariffList.Add(createTariff(nodeXMl));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return tariffList;
        }

        // Метод возвращающий тариф
        private Tariff createTariff(XmlNode xml_node)
        {
            Tariff tarif = new Tariff();
            if (xml_node.Attributes.Count > 0)
            {
                XmlNode attrib = xml_node.Attributes.GetNamedItem("name");
                if (attrib!=null)
                {
                    tarif.setName(attrib.Value);
                }

                attrib = xml_node.Attributes.GetNamedItem("operatorName");
                if (attrib!=null)
                {
                    tarif.setOperatorName(attrib.Value);
                }

                foreach (XmlNode node_payr in xml_node.ChildNodes)
                {
                    if (node_payr.Name == "payroll")
                    {
                        tarif.setPayroll(Convert.ToDouble(node_payr.InnerText));
                    }

                    if (node_payr.Name == "callPrices")
                    {
                        tarif.setCallPrices(createCallPrices(node_payr));
                    }

                    if (node_payr.Name == "smsPrice")
                    {
                        tarif.setSmsPrice(Convert.ToDouble(node_payr.InnerText));
                    }

                    if (node_payr.Name == "parameter")
                    {
                        tarif.setParameter(createParameter(node_payr));
                    }
                } 
            }
            return tarif;
        }

        // Метод, возвращающий цены тарифа
        private CallPrices createCallPrices(XmlNode xmlCall)
        {
            CallPrices callPrices = new CallPrices();
            foreach (XmlNode node_call in xmlCall.ChildNodes)
            {
                if (node_call.Name == "insideNetwork")
                {
                    callPrices.setInsideNetwork(Convert.ToDouble(node_call.InnerText));
                }

                if (node_call.Name == "outsideNetwork")
                {
                    callPrices.setOutsideNetwork(Convert.ToDouble(node_call.InnerText));
                }

                if (node_call.Name == "fixedPhone")
                {
                    callPrices.setFixedPhone(Convert.ToDouble(node_call.InnerText));
                }
            }
            return callPrices;
        }

        // Метод, возвращающий параметры тарифа
        private Parameter createParameter (XmlNode xmlParam)
        {
            Parameter parameter = new Parameter();
            foreach (XmlNode nade_param in xmlParam.ChildNodes)
            {
                if (nade_param.Name == "favoriteNumber")
                {
                    parameter.setFavoriteNumber(Convert.ToInt64(nade_param.InnerText));
                }

                if (nade_param.Name == "tariffication")
                {
                    parameter.setTariffication(nade_param.InnerText);
                }

                if (nade_param.Name == "connectionFee")
                {
                    parameter.setConnectionFee(Convert.ToDouble(nade_param.InnerText));
                }
            }
            return parameter;
        }
    }
}