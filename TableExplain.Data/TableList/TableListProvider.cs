using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableExplain.Db;
using TableExplain.Db.DBUtility;
using System.Data;
using System.Configuration;

namespace TableExplain.Data.TableList
{
    public class TableListProvider
    {
        /// <summary>
        /// 返回表属性
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public List<TableModel> GetListTable(string TableName)
        {

       
            string strSql = @"SELECT  CASE WHEN col.colorder = 1 THEN obj.name    ELSE ''  END AS TableName, col.colorder AS ColumnNum,col.name AS ColumnName,ISNULL(ep.[value], '') AS ColumnExplain ,t.name AS DateType,col.length AS DateSize 
, ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS DecimalSize ,  
        CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '√'   ELSE ''   END AS Identification ,  
        CASE WHEN EXISTS ( SELECT 1 FROM     dbo.sysindexes si INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id  AND si.indid = sik.indid  INNER JOIN dbo.syscolumns sc ON sc.id = sik.id   AND sc.colid = sik.colid  
                                    INNER JOIN dbo.sysobjects so ON so.name = si.name   AND so.xtype = 'PK'   WHERE    sc.id = col.id  AND sc.colid = col.colid ) THEN '√'   ELSE ''  END AS MianKey ,  
        CASE WHEN col.isnullable = 1 THEN '√'    ELSE ''   END AS IsEmpty ,   ISNULL(comm.text, '') AS defaultValue  
        FROM    dbo.syscolumns col   LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype   inner JOIN dbo.sysobjects obj ON col.id = obj.id   AND obj.xtype = 'U'   AND obj.status >= 0  
        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id   LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id  AND col.colid = ep.minor_id   AND ep.name = 'MS_Description'  
        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id   AND epTwo.minor_id = 0  AND epTwo.name = 'MS_Description'  
       WHERE   obj.name = ''+@TableName+''";


            SqlParameter[] par =new SqlParameter[]{ new SqlParameter("@TableName", TableName) };
            List<TableModel> list = new List<TableModel>();

           // TableName ColumnNum   ColumnName ColumnExplain   DateType DateSize    DecimalSize Identification  MianKey IsEmpty defaultValue

            using (SqlDataReader dr = SQLHelper.ExecuteReader(new ConnectionStrings().Connection(), CommandType.Text, strSql, par)) {
          
                    while (dr.Read())
                    {
                        list.Add(new TableModel
                        {
                            TableName = dr["TableName"] == System.DBNull.Value ? "" : dr["TableName"].ToString(),
                        ColumnNum = Convert.ToInt32(dr["ColumnNum"]),
                            ColumnName = dr["ColumnName"].ToString(),
                            ColumnExplain = dr["ColumnExplain"].ToString(),
                            DateType = dr["DateType"].ToString(),
                            DateSize = dr["DateSize"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DateSize"]),
                            DecimalSize = dr["DecimalSize"] == DBNull.Value ? 0 : Convert.ToInt32(dr["DecimalSize"]),
                            Identification = dr["Identification"] == System.DBNull.Value ? "" : dr["Identification"].ToString(),
                            MianKey = dr["MianKey"] == System.DBNull.Value ? "" : dr["MianKey"].ToString(),
                            IsEmpty = dr["IsEmpty"] == System.DBNull.Value ? "" : dr["IsEmpty"].ToString(),
                            defaultValue = dr["defaultValue"] == System.DBNull.Value ? "" : dr["defaultValue"].ToString()
                    });
                    }
                }
            
                return list;
        }
        /// <summary>
        /// 返回表外键
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public List<ForeignKey> GetListTableforeignkey(string TableName)
        {
            string strSql = @"SELECT
            ID = b.fkeyid,
            FkTableName = object_name(b.fkeyid),
            FkColumnID = b.fkey,
            FkTableExplain = (SELECT value FROM sys.extended_properties ds WHERE major_id = b.fkeyid AND minor_id = 0),
            FkColumnName = (SELECT name FROM syscolumns WHERE colid = b.fkey AND id = b.fkeyid) ,
            PkColumnName = (SELECT name FROM syscolumns WHERE colid = b.rkey AND id = b.rkeyid) ,
            RelevanceUpdate = ObjectProperty(a.id, 'CnstIsUpdateCascade') ,
            RelevanceDelete = ObjectProperty(a.id, 'CnstIsDeleteCascade')
            FROM sysobjects a
            JOIN sysforeignkeys b ON a.id = b.constid
            JOIN sysobjects c ON a.parent_obj = c.id
            WHERE a.xtype = 'F' AND c.xtype = 'U' AND object_name(b.rkeyid) = ''+@TableName+''";

            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@TableName", TableName) };
            List<ForeignKey> list = new List<ForeignKey>();

            using (SqlDataReader dr = SQLHelper.ExecuteReader(new ConnectionStrings().Connection(), CommandType.Text, strSql, par))
            {

                while (dr.Read())
                {
                    list.Add(new ForeignKey
                    {
                        Id = dr["ID"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["ID"]),
                        FkTableName = dr["FkTableName"].ToString(),
                        FkColumnID = dr["FkColumnID"].ToString(),
                        FkTableExplain = dr["FkTableExplain"].ToString(),
                        FkColumnName = dr["FkColumnName"].ToString(),
                        PkColumnName = dr["PkColumnName"].ToString(),
                        RelevanceUpdate = dr["RelevanceUpdate"].ToString(),
                        RelevanceDelete = dr["RelevanceDelete"] == System.DBNull.Value ? "" : dr["RelevanceDelete"].ToString()
                    });
                }
            }

            return list;
        }
        /// <summary>
        /// 查询数据库所以表
        /// </summary>
        /// <returns></returns>
        public List<TableNames> GetAllTableName()
        {
            List<TableNames> list = new List<TableNames>();
            string sqlstr = @"SELECT  tbs.name AS TableName ,ISNULL((SELECT  CONVERT(VARCHAR(120), value)  FROM sys.extended_properties  WHERE major_id=TBS.id AND minor_id=0),'') AS TableExplain FROM 
                 sysobjects tbs WHERE    TYPE='U'  ORDER BY tbs.NAME";
            using (SqlDataReader dr = SQLHelper.ExecuteReader(new ConnectionStrings().Connection(), CommandType.Text, sqlstr))
            {
                while (dr.Read())
                {
                    list.Add(new TableNames
                    {
                        TableName=dr["TableName"].ToString(),
                        TableExplain= dr["TableExplain"]==System.DBNull.Value ? "" : dr["TableExplain"].ToString()
                    });
                }
            }
            return list;
        }


        /// <summary>
        /// 返回所有存储过程，视图等等
        /// </summary>
        /// <returns></returns>
        public List<TableNames> GetListdatabasetype(string type)
        {
            List<TableNames> list = new List<TableNames>();
            string sqlstr = @"select a.name from sys.all_objects a,sys.sql_modules b where a.is_ms_shipped=0 and a.object_id = b.object_id and a.[type] = ''+@databasetype+'' order by a.[name] ASC";
            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@databasetype", type) };
            using (SqlDataReader dr = SQLHelper.ExecuteReader(new ConnectionStrings().Connection(), CommandType.Text, sqlstr,par))
            {
                while (dr.Read())
                {
                    list.Add(new TableNames
                    {
                        TableName = dr["name"].ToString()
                       
                    });
                }
            }
            return list;
        }

        public string GetTypedefinitionByName(string typeName, string type)
        {
            try
            {
                string sqlstr = @"select b.[definition] from sys.all_objects a,sys.sql_modules b where a.is_ms_shipped=0 and a.object_id = b.object_id and a.[type] = ''+@databasetype+'' AND a.name=''+@TypeName+'' ";
                SqlParameter[] par = new SqlParameter[] { new SqlParameter("@databasetype", type),
            new SqlParameter("@TypeName",typeName) };
                object rel = SQLHelper.ExecuteScalar(new ConnectionStrings().Connection(), CommandType.Text, sqlstr,par);
                return rel != null ? rel.ToString() : "";
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        /// <summary>
        /// 返回表属性
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public StringBuilder ProduceProperty(string TableName,string dbName)
        {
            #region 读取webconfig以外的AppSettings节点内容
            //ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            // map.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "/conn.config";
            //Configuration libConfig =  ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            //string strValue = libConfig.AppSettings.Settings["dataName"].Value;
            #endregion

            List<TableModel> list= GetListTable(TableName);
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;\n");
            builder.Append("namespace "+ dbName + ".Model \n");
            builder.Append("{ \n");
            builder.Append("    [Table(\"" + TableName + "\")]");
            builder.Append("\n    public class " + TableName);
             builder.Append("\n    { ");
            foreach (var item in list)
            {
                string wordtype = "string";
                try
                {
                    wordtype = GetDateTypeC()[item.DateType];
                }
                catch (Exception)
                {

                }
                builder.Append("\n         /// <summary>");
                builder.Append("\n         ///" + item.ColumnExplain);
                builder.Append("\n         /// <summary>");
                builder.Append( item.DateType== "datetime2" ? "\n        [Column(TypeName = \"datetime2\")]":"");
                builder.Append("\n         public "+ wordtype + (item.IsEmpty== "√"?"?":"") + " " + item.ColumnName);
                builder.Append("\n         {");
                builder.Append("\n             get;");
                builder.Append("\n             set;");
                builder.Append("\n         }");
            }
            builder.Append("\n    } ");
            builder.Append("\n} ");

            return builder;
        }

        public static Dictionary<string, string> GetDateTypeC()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("bigint", "int");
            dic.Add("int", "int");
            dic.Add("smallint", "int");
            dic.Add("tinyint", "System.Byte");
            dic.Add("bit", "bool");
            dic.Add("decimal", "System.Decimal");
            dic.Add("numeric", "System.Decimal");
            dic.Add("money", "System.Decimal");
            dic.Add("smallmoney", "System.Decimal");
            dic.Add("float", "System.Double");
            dic.Add("real", "System.Single");
            dic.Add("datetime", "System.DateTime");
            dic.Add("datetime2", "System.DateTime");
            dic.Add("smalldatetime", "System.DateTime");
            dic.Add("date", "System.DateTime");
            dic.Add("char", "string");
            dic.Add("varchar", "string");
            dic.Add("text", "string");
            dic.Add("nchar", "string");
            dic.Add("nvarchar", "string");
            dic.Add("ntext", "string");
            dic.Add("binary", "System.Byte[]");
            dic.Add("varbinary", "System.Byte[]");
            dic.Add("image", "System.Byte[]");
            dic.Add("timestamp", "System.DateTime");
            dic.Add("uniqueidentifier", "System.Guid");
            dic.Add("Variant", "Object");
            return dic;
        }


    }
}
