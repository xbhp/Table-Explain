using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using TableExplain.Data;
using TableExplain.Db;

namespace TableExplain_mvc.Controllers
{
    public class TableConnController : Controller
    {
        // GET: TableConn
        public ActionResult Index()
        {
            string  dbName= Session["dbName"]==null?"": Session["dbName"].ToString();
            string strXmlFile = Server.MapPath("/conn.config");
            XmlHelper xmlTool = new XmlHelper(strXmlFile);

            ViewBag.Conntion = string.IsNullOrEmpty(dbName.ToString())? xmlTool.GetAlone("configuration/SqlConnStr"): xmlTool.GetAlone("configuration/SqlConnStr[@value=\"" + dbName.ToString() + "\"]");
            return View();
        }
        [HttpPost]
        public ActionResult GetConList()
        {
            string strXmlFile = Server.MapPath("/conn.config");
            XmlHelper xmlTool = new XmlHelper(strXmlFile);
            var tabls = xmlTool.GetData("configuration");

            return Content(DataTableToJson(tabls));
        }
        public ActionResult SetConn(string dbName)
        {
            Session["dbName"] = dbName;
            return Content("1");
        }
        //public ActionResult UpdateConn(DbConnModel connModel)
        //{

        //    try
        //    {
        //        connModel.dbAddress = string.IsNullOrEmpty(connModel.dbAddress)? "(local)": connModel.dbAddress;
        //        string strXmlFile = Server.MapPath("/conn.config");
        //        XmlHelper xmlTool = new XmlHelper(strXmlFile);
        //        string connstr = "Data Source=" + connModel.dbAddress +";User ID=" + connModel.userName + ";Password=" + connModel.passWord + ";Initial Catalog=" + connModel.dbName + ";Pooling=true;MultipleActiveResultSets=True;App=EntityFramework";
        //        // 更新元素内容
        //        xmlTool.Replace("configuration/SqlConnStr", connstr);
        //        xmlTool.Save();

        //        string  isrel = "1";
        //        //更新数据库名称
        //        isrel= new OperateXml().SetAttribute("value", connModel.dbName, strXmlFile, "/configuration/appSettings/add[@key=\"dataName\"]") ? "1" : "0";
        //        return Content(isrel);
        //    }
        //    catch (Exception)
        //    {
        //        return Content("0");
        //        throw;
        //    }
          
        //}
        public ActionResult _AddOrEditConn(DbConnModel connModel)
        {

            try
            {
                connModel.dbAddress = string.IsNullOrEmpty(connModel.dbAddress) ? "(local)" : connModel.dbAddress;
                string strXmlFile = Server.MapPath("/conn.config");
                XmlHelper xmlTool = new XmlHelper(strXmlFile);
                string connstr = "Data Source=" + connModel.dbAddress + ";User ID=" + connModel.userName + ";Password=" + connModel.passWord + ";Initial Catalog=" + connModel.dbName + ";Pooling=true;MultipleActiveResultSets=True;App=EntityFramework";
                if (connModel.isAdd == 1)
                {
                   string dbName = xmlTool.GetAlone("configuration/SqlConnStr[@value='" + connModel.dbName + "']");
                    if (dbName== "错误或节点不存在！")
                    {

                        xmlTool.InsertElement("configuration", "SqlConnStr", "value", connModel.dbName, connstr);
                        xmlTool.Save();
                        string xmlNodes = "/configuration/SqlConnStr[@value='" + connModel.dbName + "']";
                        string isrel1 = new OperateXml().SetAttribute("dbExplain", connModel.dbExplain, strXmlFile, xmlNodes.Replace(@"\", "")) ? "1" : "0";

                        //if (isrel1 == "1")
                        //{

                        //    string isrel2 = "1";
                        //    //更新数据库名称
                        //    isrel2 = new OperateXml().SetAttribute("value", connModel.dbName, strXmlFile, "/configuration/appSettings/add[@key=\"dataName\"]") ? "1" : "0";
                        //    return Content(isrel2);
                        //}

                        return Content(isrel1);
                    }
                    else
                    {
                        return Content("重复选项");
                    }
                }
                else
                {
                   
                   
                    // 更新元素内容
                    xmlTool.Replace("configuration/SqlConnStr[@value=\"" + connModel.dbName + "\"]", connstr);
                    xmlTool.Save();
                    //修改表说明
                    string isrel1 = new OperateXml().SetAttribute("dbExplain", connModel.dbExplain, strXmlFile, "/configuration/SqlConnStr[@value=\"" + connModel.dbName + "\"]") ? "1" : "0";

                    //if (isrel1 == "1")
                    //{

                    //    string isrel2 = "1";
                    //    //更新数据库名称
                    //    isrel2 = new OperateXml().SetAttribute("value", connModel.dbName, strXmlFile, "/configuration/appSettings/add[@key=\"dataName\"]") ? "1" : "0";
                    //    return Content(isrel2);
                    //}
                    return Content(isrel1);
                }
            }
            catch (Exception ee)
            {
                //return Content("0");
                throw;
            }
            return Content("0");

        }
        public static string DataTableToJson( DataTable dt)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":\"" + dt.Rows[i][j].ToString() + "\"");
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();
        }


        public ActionResult AddOrEditConn(string dbName,string dbExplain)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                string strXmlFile = Server.MapPath("/conn.config");
                XmlHelper xmlTool = new XmlHelper(strXmlFile);

                string strconn = xmlTool.GetAlone("configuration/SqlConnStr[@value=\"" + dbName.ToString() + "\"]");
                string[] strconns = strconn.Split('=');

                ViewBag.dbExplain = dbExplain;
                ViewBag.dbName = strconns[4].Split(';')[0];
                ViewBag.dbAddress = strconns[1].Split(';')[0];
                ViewBag.userName = strconns[2].Split(';')[0];
                ViewBag.passWord = strconns[3].Split(';')[0];
            }
            else
            {
                ViewBag.dbExplain = "";
                ViewBag.dbName = "";
                ViewBag.dbAddress = "";
                ViewBag.userName = "";
                ViewBag.passWord = "";
            }
            return View();
        }

    }
}