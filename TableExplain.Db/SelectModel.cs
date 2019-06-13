using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableExplain.Db
{
    public class SelectModel
    {
        public SelectModel()
        {
        }

        public SelectModel(string id,string parentid,string value, string text,string level )
        {
            ID = id;
            ParentID = parentid;
            Value = value;
            Text = text;
            ReportLevel = level;
        }

        /// <summary>
        /// 隐藏的值
        /// </summary>
        
        public string Value { get; set; }

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentID { get; set; }

        public string ReportLevel { get; set; }
    }
}
