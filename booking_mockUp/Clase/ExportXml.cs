using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Web.Configuration;
using System.Xml;
namespace xDock_scanners_app
{
    public static class export_xml
    {
        public static string locatie;

        static export_xml()
        {
            export_xml.locatie = WebConfigurationManager.AppSettings["xml_export"];
        }

        public static void scris_status_finish(List<string> det, string actiune)
        {
            XmlTextWriter wr = new XmlTextWriter(string.Concat(ConfigurationManager.AppSettings["xml_export"], det[0], ".xml"), Encoding.UTF8)
            {
                Formatting = Formatting.Indented
            };
            wr.WriteStartElement("comtec-mobile-communication");
            wr.WriteAttributeString("version", "2010");
            wr.WriteStartElement("message-in");
            wr.WriteStartElement("actions");
            wr.WriteStartElement("stop");
            wr.WriteAttributeString("id", det[1]);
            wr.WriteStartElement("action");
            wr.WriteAttributeString("id", det[4]);
            wr.WriteStartElement("planning-status");
            wr.WriteString(det[2]);
            wr.WriteEndElement();
            wr.WriteStartElement("realised-times");
            wr.WriteStartElement(actiune);
            wr.WriteString(det[3]);
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.WriteStartElement("realisationkind");
            wr.WriteString(det[5]);
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.WriteEndElement();
            wr.Flush();
            wr.Close();
        }
    }
}