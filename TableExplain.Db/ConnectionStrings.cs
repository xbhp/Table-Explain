using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;
using System.Web.UI;

namespace TableExplain.Db
{
    public class ConnectionStrings
    {
        //public ExeConfigurationFileMap map = new ExeConfigurationFileMap();
        public string Connection()
        {

            //map.ExeConfigFilename = "conn.config";
            //Configuration libConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //string strValue = libConfig.AppSettings.Settings["somekey"].Value;
            return GetAlone();
        }
        // public static readonly string ConnectionTable = ConfigurationManager.ConnectionStrings["SQLConnString1"].ConnectionString;
        protected string strXmlFile;
        protected XmlDocument objXmlDoc = new XmlDocument();
        public string GetAlone()
        {
            try
            {
                string strXmlFile = AppDomain.CurrentDomain.BaseDirectory + "/conn.config";
                try
                {

                    objXmlDoc.Load(strXmlFile);
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
                //string strXmlFile = Server.MapPath("/conn.config");
                string dbName = System.Web.HttpContext.Current.Session["dbName"] == null ? "" : System.Web.HttpContext.Current.Session["dbName"].ToString();
                XmlNode xmlNode1 = objXmlDoc.SelectSingleNode(string.IsNullOrEmpty(dbName)?"configuration/SqlConnStr": "configuration/SqlConnStr[@value=\"" + dbName + "\"]");
                return xmlNode1.InnerText;
            }
            catch (Exception)
            {
                return "错误或节点不存在！";
                throw;
            }

        }
    }
   
}
