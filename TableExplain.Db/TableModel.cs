using System;


namespace TableExplain.Db
{
    public class TableModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// 行号
        /// </summary>
         public int ColumnNum
        {
            get;
            set;
        }
        /// <summary>
        /// 行名称
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }
        /// <summary>
        /// 行说明
        /// </summary>
        public string  ColumnExplain
        {
            get;
            set;
        }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string DateType
        {
            get;
            set;
        }
        /// <summary>
        /// 类型长度
        /// </summary>
       public int DateSize
        {
            get;
            set;
        }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalSize
        {
            get;
            set;
        }
        /// <summary>
        /// 标识
        /// </summary>
        public string Identification
        {
            get;
            set;
        }
        /// <summary>
        /// 是否主键
        /// </summary>
        public string  MianKey
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
         public string  IsEmpty
        {
            get;
            set;
        }
        /// <summary>
        /// 默认值
        /// </summary>
       public string   defaultValue
        {
            get;
            set;
        }
    }
}
