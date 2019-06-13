using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TableExplain.Data.TableList;
using TableExplain.Common;

namespace TableExplain_mvc.Controllers
{
    public class TableListController : Controller
    {
        // GET: TableList
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAllTableName()
        {
            return Json(new TableListProvider().GetAllTableName(),JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 显示表属性
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListTable(string tableName) {
            return Json(new TableListProvider().GetListTable(tableName), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示表属性
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetListTableForeignKey(string tableName)
        {
            return Json(new TableListProvider().GetListTableforeignkey(tableName), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ProduceProperty(string tableName)
        {
            string dbName = Session["dbName"] == null ? "" : Session["dbName"].ToString();
            StringBuilder builder = new TableListProvider().ProduceProperty(tableName, dbName);
            var fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/UploadFiles", "属性", tableName + ".cs.txt");
            new AsposeCellsHelper().SaveOrCreateNoWorkbook(fileName);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName);
            sw.Write(builder.ToString());
            sw.Close();

            return File(fileName, "application/octet-stream", tableName+".cs");
        }


        /// <summary>
        /// 返回所有存储过程等等
        /// </summary>
        /// <returns></returns>
        public ActionResult GetListPorcedu(string type)
        {
            return Json(new TableListProvider().GetListdatabasetype(type), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowTableDes()
        {
            return View();
        }
        public ActionResult ShowTableForeignKey()
        {
            return View();
        }
        public ActionResult Typeindex()
        {
            return View();
        }
        public ActionResult ShowDataBaseType(string typeName,string type)
        {
            ViewBag.definition= new TableListProvider().GetTypedefinitionByName(typeName, type);
            return View();
        }
    }
}