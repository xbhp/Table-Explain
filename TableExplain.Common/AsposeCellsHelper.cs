using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace TableExplain.Common
{
    public class AsposeCellsHelper
    {

        private Workbook _Workbook = null;
        Style style1 = null;
        public AsposeCellsHelper()
        {
            _Workbook = new Workbook();
            _Workbook.Worksheets.RemoveAt(0);
            style1 = _Workbook.Styles[_Workbook.Styles.Add()];

            style1.HorizontalAlignment = TextAlignmentType.Center;
            style1.Font.Name = "宋体";
            style1.Font.Size = 10;
            style1.IsLocked = true;
            style1.Font.IsBold = true;
            style1.ForegroundColor = Color.FromArgb(0x99, 0xcc, 0xff);
            style1.Pattern = BackgroundType.Solid;
            style1.IsTextWrapped = true;
            style1.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style1.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style1.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style1.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style1.VerticalAlignment = TextAlignmentType.Center;
            style1.ForegroundColor = System.Drawing.Color.FromArgb(153, 204, 0);
            style1.Pattern = BackgroundType.Solid;
        }

        /// <summary>
        /// 初始化Execl操作类
        /// </summary>
        /// <param name="file">现有的execl文件</param>
        public AsposeCellsHelper(string file)
        {
            _Workbook = new Workbook(file);
        }

        /// <summary>
        /// 添加Sheet
        /// </summary>
        /// <param name="name">Sheet名称</param>
        /// <returns>Sheet的索引</returns>
        public int AddWorksheet(string name)
        {
            var worksheet = _Workbook.Worksheets.Find(t => t.Name == name);
            if (worksheet != null)
                return worksheet.Index;
            worksheet = _Workbook.Worksheets.Add(name);
            if (worksheet != null)
                return worksheet.Index;
            return -1;
        }

        /// <summary>
        /// 获取sheet的索引
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public int GetSheetIndex(string sheetName)
        {


            if (string.IsNullOrEmpty(sheetName))
                throw new ArgumentNullException("sheetName 必须有值！");
            return _Workbook.Worksheets.FindIndex(t => t.Name == sheetName);
        }

        public int GetSheetCellRowCount(int sheetIndex)
        {


            if (sheetIndex >= _Workbook.Worksheets.Count)
                throw new IndexOutOfRangeException("Worksheets 的最大数为:" + _Workbook.Worksheets.Count);
            return _Workbook.Worksheets[sheetIndex].Cells.Rows.Count;
        }


        public void ImportArray(int sheetIndex, string[] array, int firstRow = 0, int firstColumn = 0)
        {
            Worksheet sheet = _Workbook.Worksheets[sheetIndex];
            sheet.Cells.ImportArray(array, firstRow, firstColumn, false);
        }


        /// <summary>
        /// 导入数据到Sheet
        /// </summary>
        /// <param name="sheetIndex">Sheet索引</param>
        /// <param name="dt">书记表</param>
        /// <returns></returns>
        public void ImportDataTable(int sheetIndex, System.Data.DataTable dt)
        {
            Worksheet sheet = _Workbook.Worksheets[sheetIndex];
            sheet.Cells.ImportDataTable(dt, true, 0, 0);
            sheet.Cells.CreateRange(0, 0, 1, dt.Columns.Count).SetStyle(style1);
            sheet.Cells.CreateRange(0, 0, 1, dt.Columns.Count).ColumnWidth = 18;
            sheet.Cells.Rows[0].Height = 20;
        }

        public void ImportDataTable(int sheetIndex, System.Data.DataTable dt, int firstRow = 0, int firstColumn = 0)
        {
            Worksheet sheet = _Workbook.Worksheets[sheetIndex];
            sheet.Cells.ImportDataTable(dt, true, firstRow, firstColumn);
        }

        public void ImportDataTable(Dictionary<string, System.Data.DataTable> sheets)
        {
            foreach (var sh in sheets)
            {
                var index = AddWorksheet(sh.Key);
                if (index > -1)
                {
                    ImportDataTable(index, sh.Value);
                }
            }
        }

        /// <summary>
        /// 导入自定义对象
        /// </summary>
        /// <param name="sheetIndex">索引</param>
        /// <param name="list">自定义对象集合</param>
        /// <param name="propertyNames">导出和显示的对象属性名称和显示名称，key：导出的属性名称，value：导出显示名称</param>
        /// <param name="dateFormat">日期格式化</param>
        public void ImportCustomObjects<T>(int sheetIndex, List<T> list, Dictionary<string, string> propertyNames, string dateFormat = "yyyy-MM-dd")
        {
            Worksheet sheet = _Workbook.Worksheets[sheetIndex];
            sheet.Cells.ImportArray(propertyNames.Values.ToArray(), 0, 0, false);
            var pNames = propertyNames.Keys.ToArray();
            sheet.Cells.ImportCustomObjects(list, pNames, false, sheet.Cells.Rows.Count, 0, list.Count, true, dateFormat, false);
            sheet.Cells.CreateRange(0, 0, 1, propertyNames.Count).SetStyle(style1);
            sheet.Cells.CreateRange(0, 0, 1, propertyNames.Count).ColumnWidth = 18;
            sheet.Cells.Rows[0].Height = 20;
        }

        /// <summary>
        /// 导出Execl数据到DataSet中
        /// </summary>
        /// <returns></returns>
        public DataSet ExportDataTable(int totalColumns = 0)
        {
            var dataSet = new System.Data.DataSet();
            foreach (Worksheet sheet in _Workbook.Worksheets)
            {
                if (sheet.Cells.Rows.Count == 0)
                    continue;
                var dt = sheet.Cells.ExportDataTable(0, 0, sheet.Cells.Rows.Count, totalColumns == 0 ? sheet.Cells.Columns.Count : totalColumns);
                if (dt != null)
                    dataSet.Tables.Add(dt);
            }

            return dataSet;
        }

        /// <summary>
        /// Exel中插入图片
        /// </summary>
        /// <param name="sheetIndex">Sheet</param>
        /// <param name="imagePath">图片路径</param>
        /// <param name="row">单元格 行，从0开始</param>
        /// <param name="col">单元格 列，从0开始</param>
        public void InsertImage(int sheetIndex, string imagePath, int row, int col)
        {
            Worksheet sheet = _Workbook.Worksheets[sheetIndex];
            sheet.Pictures.Add(row, col, imagePath);
        }


        public void Save(string filePath)
        {
            _Workbook.Save(filePath);
        }

        /// <summary>
        /// 路径不存在时自动创建
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveOrCreate(string filePath)
        {
            var dir = filePath.Substring(0, filePath.LastIndexOf("\\"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            _Workbook.Save(filePath);
        }


        /// <summary>
        /// 路径不存在时自动创建
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveOrCreateNoWorkbook(string filePath)
        {
            var dir = filePath.Substring(0, filePath.LastIndexOf("\\"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        
        }

        public void Save(System.IO.Stream stream)
        {
            _Workbook.Save(stream, SaveFormat.Xlsx);
        }
    }
}
