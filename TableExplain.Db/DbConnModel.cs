using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableExplain.Db
{
    /// <summary>
    /// 数据库连接相关
    /// </summary>
    public class DbConnModel
    {
        /// <summary>
        /// 数据库说明
        /// </summary>
        public string dbExplain { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string dbName { get; set; }
        /// <summary>
        /// 数据库地址
        /// </summary>
        public string dbAddress { get; set; }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 数据库密码
        /// </summary>
        public string passWord { get; set; }
        /// <summary>
        /// 是否添加
        /// </summary>
        public int isAdd{ get; set; }
    }
}
