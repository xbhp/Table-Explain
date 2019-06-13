using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

namespace TableExplain.Data
{
     public class OperateXml
    {
        public static string GetUrl(string key, string configpath,string keyvalue)
        {
            string value = "";
            XmlTextReader reader = new XmlTextReader(configpath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                if (node.Attributes["key"].Value == key)
                {
                    value = node.Attributes[keyvalue].Value;
                    break;
                }
            }
            return value;
        }
        public bool SetAttribute(string key,string keyvalue, string configpath, string XmlPathNode)
        {
            try
            {
              //  XmlTextReader reader = new XmlTextReader(configpath);

                XmlDocument doc = new XmlDocument();
                doc.Load(configpath);
                XmlElement xe = doc.DocumentElement;
                 //  XmlNodeList nodeList = doc.SelectNodes(XmlPathNode);
                XmlElement selectXe = (XmlElement)xe.SelectSingleNode(XmlPathNode);
                selectXe.SetAttribute(key, keyvalue);
                doc.Save(configpath);
                doc = null;
                return true;
                
            }
            catch (Exception ee)
            {
                //return false;
                throw;
            }
            return false;
        }


    }
}
