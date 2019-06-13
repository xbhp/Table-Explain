using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableExplain.Db
{
    public class ForeignKey
    {
       
        /// <summary>
        /// 外键ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 外键表名称
        /// </summary>
        public string FkTableName { get; set; }
        /// <summary>
        /// 外键表说明
        /// </summary>
        public string FkTableExplain { get; set; }
        /// <summary>
        /// 外键列ID
        /// </summary>
        public string FkColumnID { get; set; }
        /// <summary>
        /// 外键列名称
        /// </summary>
        public string FkColumnName { get; set; }
        /// <summary>
        /// 主键列名
        /// </summary>
        public string PkColumnName { get; set; }
        /// <summary>
        /// 级联更新
        /// </summary>
        public string   RelevanceUpdate{ get; set; }
        /// <summary>
        /// 级联删除
        /// </summary>
        public string RelevanceDelete { get; set; }
    }
}
