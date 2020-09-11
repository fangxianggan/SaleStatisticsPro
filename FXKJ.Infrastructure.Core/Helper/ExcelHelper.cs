using Aspose.Cells;
using FXKJ.Infrastructure.Core.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace FXKJ.Infrastructure.Core.Helper
{

    /// <summary>
    /// 导出的工具类
    /// </summary>
    public static class ExportHelper
    {

        /// <summary>
        /// 设置导出的一些参数 默认值
        /// </summary>
        private static FileFormatType _fileFormatType = FileFormatType.Xlsx;  //默认导出的格式是excel
        private static SaveFormat _saveType = SaveFormat.Xlsx;
        //设置一个sheet默认值的最大行数
        private static int _rowCount = 800000;
        //默认的文件目录
        private static string _fileDir = "/Excel";
        //设置本地的路径地址
        private static string _localPath = "";
        //绝对目录
        private static string _directoryPath = "";
        /// <summary>
        /// excel列明显示中文名称
        /// </summary>
        private static bool _isDisplayName = true;
        //列显示中文名称
        private static Dictionary<string, string> _columnNameDisplayName;

        public static void SetExportParam(string directoryPath, string fileDir, int rowCount, FileFormatType fileFormatType, SaveFormat saveType)
        {
            _fileFormatType = fileFormatType;
            _saveType = saveType;
            _rowCount = rowCount;
            _fileDir = fileDir;
            _localPath = FileUtil.MapPath(_fileDir);
            _directoryPath = directoryPath;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDisplayName"></param>
        /// <param name="columnNameDisplayName"></param>
        public static void SetColumnNameDisplayName(bool isDisplayName, Dictionary<string, string> columnNameDisplayName)
        {
            _isDisplayName = isDisplayName;
            _columnNameDisplayName = columnNameDisplayName;

        }
        /// <summary>
        /// 设置破除水印
        /// </summary>
        public static void SetLicense()
        {
            License license = new License();
            string pathLicense = AppDomain.CurrentDomain.BaseDirectory + "\\Aspose\\License.lic";
            license.SetLicense(pathLicense);
        }

        /// <summary>
        /// 导出数据流 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static void ExportData(DataSet ds, string fileName)
        {

            SetLicense();

            var httpReponse = HttpContext.Current.Response;
            httpReponse.Clear();
            httpReponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpReponse.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            Workbook workbook = new Workbook(_fileFormatType);
            workbook.Worksheets.Clear();
            foreach (DataTable table in ds.Tables)
            {
                DataTable dt = RemoveEmptyDataTable(table);
                foreach (DataTable dtt in SplitDataTable(dt, _rowCount).Tables)
                {
                    SetWorkSheet(dtt, workbook);
                }
            }
            httpReponse.BinaryWrite(workbook.SaveToStream().ToArray());
            httpReponse.Flush();
            httpReponse.End();
        }

        /// <summary>
        /// 输出流
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static MemoryStream ExportData(DataSet ds)
        {
            SetLicense();

            Workbook workbook = new Workbook(_fileFormatType);
            workbook.Worksheets.Clear();
            foreach (DataTable table in ds.Tables)
            {
                DataTable dt = RemoveEmptyDataTable(table);
                foreach (DataTable dtt in SplitDataTable(dt, _rowCount).Tables)
                {
                    SetWorkSheet(dtt, workbook);
                }
            }
            return workbook.SaveToStream();

        }


        /// <summary>
        /// 主从合并 输出流
        /// </summary>
        /// <param name="dt1">从表</param>
        /// <param name="dt2">主表</param>
        /// <returns></returns>
        public static MemoryStream ExportMegerData(DataTable dt1, DataTable dt2)
        {
            SetLicense();
            Workbook workbook = new Workbook(_fileFormatType);
            workbook.Worksheets.Clear();
            SetWorkSheet(dt1, dt2, workbook);
            return workbook.SaveToStream();

        }
        /// <summary>
        /// 保存文件 到磁盘  
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string SaveWorkbookData(DataSet ds, string fileName)
        {
            SetLicense();
            Workbook workbook = new Workbook(_fileFormatType);
            workbook.Worksheets.Clear();
            foreach (DataTable table in ds.Tables)
            {
                DataTable dt = RemoveEmptyDataTable(table);
                foreach (DataTable dtt in SplitDataTable(dt, _rowCount).Tables)
                {
                    SetWorkSheet(dtt, workbook);
                }
            }
            var path = "";
            if (!string.IsNullOrEmpty(_directoryPath))
            {
                path = _directoryPath + fileName;
            }
            else
            {
                path = _localPath + fileName;
            }
            workbook.Save(fileName, _saveType);
            return path;
        }


        /// <summary>
        /// 移除空的数据
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable RemoveEmptyDataTable(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        rowdataisnull = false;
                    }
                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
            return dt;
        }

        /// <summary>
        /// 分解数据表
        /// </summary>
        /// <param name="originalTab">需要分解的表</param>
        /// <param name="rowsNum">每个表包含的数据量</param>
        /// <returns></returns>
        public static DataSet SplitDataTable(DataTable originalTab, int rowsNum)
        {
            //获取所需创建的表数量
            int tableNum = originalTab.Rows.Count / rowsNum;
            //获取数据余数
            int remainder = originalTab.Rows.Count % rowsNum;
            DataSet ds = new DataSet();
            //如果只需要创建1个表，直接将原始表存入DataSet
            if (tableNum == 0)
            {
                DataTable data = originalTab.Clone();
                foreach (DataRow r in originalTab.Rows)
                {
                    object[] arr = r.ItemArray;
                    data.Rows.Add(arr);
                }
                ds.Tables.Add(data);
            }
            else
            {
                if (remainder > 0)
                { tableNum = tableNum + 1; }
                DataTable[] tableSlice = new DataTable[tableNum];
                //Save orginal columns into new table.            
                for (int c = 0; c < tableNum; c++)
                {
                    tableSlice[c] = new DataTable();
                    tableSlice[c].TableName = originalTab.TableName + "_" + (c + 1);
                    foreach (System.Data.DataColumn dc in originalTab.Columns)
                    {
                        tableSlice[c].Columns.Add(dc.ColumnName, dc.DataType);
                    }
                }
                //Import Rows
                for (int i = 0; i < tableNum; i++)
                {
                    // if the current table is not the last one
                    if (i != tableNum - 1)
                    {
                        for (int j = i * rowsNum; j < ((i + 1) * rowsNum); j++)
                        {
                            tableSlice[i].ImportRow(originalTab.Rows[j]);
                        }
                    }
                    else
                    {
                        for (int k = i * rowsNum; k < (i * rowsNum + remainder - 1); k++)
                        {
                            tableSlice[i].ImportRow(originalTab.Rows[k]);
                        }
                    }
                }
                //add all tables into a dataset                
                foreach (DataTable dt in tableSlice)
                {
                    ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        /// <summary>
        /// 生成的sheet
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static Worksheet SetWorkSheet(DataTable dt, Workbook workbook)
        {
            Worksheet cellSheet = workbook.Worksheets.Add(dt.TableName);
            int rowIndex = 0;
            int colIndex = 0;
            int colCount = dt.Columns.Count;
            int rowCount = dt.Rows.Count;
            //列名的处理
            for (int i = 0; i < colCount; i++)
            {
                var columnName = dt.Columns[i].ColumnName;
                //是否显示中文名称
                if (_isDisplayName)
                {
                    columnName = _columnNameDisplayName[columnName].ToString();
                }
                cellSheet.Cells[rowIndex, colIndex].PutValue(columnName);
                colIndex++;
            }
            Style style = workbook.Styles[workbook.Styles.Add()];
            style.Font.Size = 12;//文字大小
            style.Font.IsBold = true;//粗体
            style.HorizontalAlignment = TextAlignmentType.Center;//文字居中
            StyleFlag styleFlag = new StyleFlag();
            cellSheet.Cells.ApplyStyle(style, styleFlag);
            rowIndex++;
            for (int i = 0; i < rowCount; i++)
            {
                colIndex = 0;
                for (int j = 0; j < colCount; j++)
                {
                    cellSheet.Cells[rowIndex, colIndex].PutValue(dt.Rows[i][j].ToString());
                    colIndex++;
                }
                rowIndex++;
            }
            cellSheet.AutoFitColumns();
            return cellSheet;
        }

        /// <summary>
        /// 主从合并
        /// </summary>
        /// <param name="dt1">明细表</param>
        /// <param name="dt2">主表</param>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public static Worksheet SetWorkSheet(DataTable dt1, DataTable dt2, Workbook workbook)
        {
            Worksheet cellSheet = workbook.Worksheets.Add(dt1.TableName);
            int rowIndex = 0;
            int colIndex = 0;
            int colCount = dt1.Columns.Count;
            int rowCount = dt1.Rows.Count;

            //需要过滤的列的index
            List<int> filterColumnIndex = new List<int>();
            //获取列明的code
            string columnCode = "";
            //列名的处理
            for (int i = 0; i < colCount; i++)
            {
                //获取数据标识code
                if (i == 0)
                {
                    columnCode = dt1.Columns[i].ColumnName;
                }
                var columnName = dt1.Columns[i].ColumnName;
                //是否显示中文名称
                if (_isDisplayName)
                {
                    if (!_columnNameDisplayName.ContainsKey(columnName))
                    {
                        filterColumnIndex.Add(i);
                        continue;
                    }
                    columnName = _columnNameDisplayName[columnName].ToString();
                }
                //设置表头样式
                Style colStyle = cellSheet.Cells[rowIndex, colIndex].GetStyle();
                colStyle.Number = 15;
                colStyle.Font.Size = 12;//文字大小
                colStyle.Font.IsBold = true;//粗体
                colStyle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                cellSheet.Cells[rowIndex, colIndex].SetStyle(colStyle);
                cellSheet.Cells[rowIndex, colIndex].PutValue(columnName);
                colIndex++;
            }
            rowIndex++;
            for (int i = 0; i < rowCount; i++)
            {
                colIndex = 0;
                for (int j = 0; j < colCount; j++)
                {

                    if (!filterColumnIndex.Contains(j))
                    {
                        //单元格样式
                        Style colStyle = cellSheet.Cells[rowIndex, colIndex].GetStyle();
                        colStyle.Font.Size = 12;//文字大小
                        colStyle.HorizontalAlignment = TextAlignmentType.Center;//文字居中
                        cellSheet.Cells[rowIndex, colIndex].SetStyle(colStyle);
                        cellSheet.Cells[rowIndex, colIndex].PutValue(dt1.Rows[i][j].ToString());
                        colIndex++;
                    }

                }
                rowIndex++;
            }

            //合并单元格
            int q = 0;
            int RowNum = 0;
            int colCount2 = dt2.Columns.Count;
            int rowCount2 = dt2.Rows.Count;
            //循环母表数据
            foreach (DataRow item in dt2.Rows)
            {
                //查询母表下子表的条数
                RowNum = dt1.Select(" " + columnCode + " = '" + item[columnCode].ToString() + "'").Length;
                for (var i = 0; i < colCount2; i++)
                {
                    cellSheet.Cells.Merge(1 + q, i, RowNum, 1);
                }
                q = q + RowNum;
            }
            cellSheet.AutoFitColumns();
            return cellSheet;
        }


        public static DataTable ReadExcel(string path)
        {
            Workbook book = new Workbook();
            book.Open(path);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
        }
        public static DataTable ReadMergedExcel(string path)
        {
            DataTable dt = new DataTable();
            Workbook workbook = new Workbook();
            workbook.Open(path);
            Cells cells = workbook.Worksheets[0].Cells;
            //获取第一行
            Row headrow = cells.GetRow(0);
            //创建列
            for (int i =0; i < cells.MaxDataColumn+1; i++)
            {
                Cell cell = headrow.GetCellOrNull(i);
                dt.Columns.Add(cell.StringValue.ToString());
            }
            //读取每行,从第二行起
            for (int r = 1; r <= cells.MaxDataRow; r++)
            {
                bool result = false;
                DataRow dr = dt.NewRow();
                //获取当前行
                Row row = cells.GetRow(r);
                //读取每列
                for (int j = 0; j < cells.MaxDataColumn+1; j++)
                {
                    Cell cell = row.GetCellOrNull(j); //一个单元格
                    if (cell != null)
                    {
                        if (cell.IsMerged && r > 1)  //检测列的单元格是否合并
                        {
                            dr[j] = dt.Rows[r - 2][j];
                        }
                        else
                        {
                            dr[j] = cell.StringValue.ToString(); //获取单元格的值

                            if (string.IsNullOrWhiteSpace(dr[j].ToString()) && j > 0)
                            {
                                dr[j] = dr[j - 1];
                            }
                        }
                        if (dr[j].ToString() != "")//全为空则不取
                        {
                            result = true;
                        }
                    }
                    else {
                        dr[j] = "";
                    }
                }
                if (result == true)
                {
                    dt.Rows.Add(dr); //把每行追加到DataTable
                }
            }
            return dt;
        }




        /// <summary>
        /// DataTable分组
        /// </summary>
        /// <param name="source">数据源（要拆分的数据）类型转换</param>
        /// <param name="destination">分组结果</param>
        /// <param name="groupByFields">分组条件</param>
        /// <param name="fieldIndex">条件个数</param>
        /// <param name="schema">数据源（要拆分的数据）</param>
        public static void GroupDataRows(IEnumerable<DataRow> source, List<DataTable> destination, string[] groupByFields, int fieldIndex, DataTable schema)
        {
            if (fieldIndex >= groupByFields.Length || fieldIndex < 0)
            {
                DataTable dt = schema.Clone();
                foreach (DataRow row in source)
                {
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = row.ItemArray;
                    dt.Rows.Add(dr);
                }
                destination.Add(dt);
                return;
            }

            var results = source.GroupBy(o => o[groupByFields[fieldIndex]]);
            foreach (var rows in results)
            {
                GroupDataRows(rows, destination, groupByFields, fieldIndex + 1, schema);
            }
            fieldIndex++;
        }

        #region 测试数据

        /// <summary>
        /// 数据源表结构
        /// </summary>
        /// <returns></returns>
        private static DataTable CreateTb()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UUID");
            tb.Columns.Add("PARENT_UUID");
            tb.Columns.Add("MASTER_DESC1");
            tb.Columns.Add("MASTER_DESC2");
            tb.Columns.Add("SUB_DESC1");
            tb.Columns.Add("SUB_DESC2");
            tb.Columns.Add("SUB_DESC3");
            return tb;
        }

        /// <summary>
        /// 拆分后主表数据结构
        /// </summary>
        /// <returns></returns>
        private static DataTable CreateParentTb()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UUID");
            tb.Columns.Add("PARENT_UUID");
            tb.Columns.Add("MASTER_DESC1");
            tb.Columns.Add("MASTER_DESC2");
            return tb;
        }

        /// <summary>
        /// 拆分后子表结构
        /// </summary>
        /// <returns></returns>
        private static DataTable CreateSupTb()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UUID");
            tb.Columns.Add("PARENT_UUID");
            tb.Columns.Add("SUB_DESC1");
            tb.Columns.Add("SUB_DESC2");
            tb.Columns.Add("SUB_DESC3");
            return tb;
        }

        /// <summary>
        /// 数据源（待拆分的数据）
        /// </summary>
        /// <returns></returns>
        private static DataTable retDt()
        {
            DataTable dt = CreateTb();
            DataRow row = null;
            for (int i = 0; i < 7; i++)
            {
                int j = 0;
                if (i % 2 == 0)//PARENT_UUID 的值，按PARENT_UUIDsss1、PARENT_UUIDsss2分组
                    j = 0;
                else
                    j = 1;
                row = dt.NewRow();
                row["UUID"] = System.Guid.NewGuid().ToString();
                row["PARENT_UUID"] = "PARENT_UUID_ " + j.ToString();
                row["MASTER_DESC1"] = "MASTER_DESC1_ " + i.ToString();
                row["MASTER_DESC2"] = "MASTER_DESC2_ " + i.ToString();
                row["SUB_DESC1"] = "SUB_DESC1_ " + i.ToString();
                row["SUB_DESC2"] = "SUB_DESC2_ " + i.ToString();
                row["SUB_DESC3"] = "SUB_DESC3_ " + i.ToString();
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="marstData">返回主表</param>
        /// <param name="subData">返回子表</param>
        /// <param name="mesg">返回信息</param>
        public  static void SeparateDt(ref DataTable marstData, ref DataTable subData, out string mesg)
        {
            mesg = string.Empty;
            DataTable sourceData = retDt().Copy();
            DataTable parentDt = CreateParentTb();
            DataTable subDt = CreateSupTb();
            string parentUuid = string.Empty;
            string subUuid = string.Empty;

            try
            {
                #region 方法
                DataTable source = new DataTable();
                string[] fileds = new string[] { "PARENT_UUID" }; // 分组条件
                List<DataTable> grouped = new List<DataTable>(); // 存储分组结果
                source = sourceData;
                GroupDataRows(source.Rows.Cast<DataRow>(), grouped, fileds, 0, source);

                string mesga = string.Empty;
                // 输出分组
                foreach (DataTable dt in grouped)
                {
                    int i = 0;
                    //处理主表数据
                    DataRow parentRow = parentDt.NewRow();
                    parentUuid = System.Guid.NewGuid().ToString();
                    parentRow["UUID"] = parentUuid;
                    parentRow["PARENT_UUID"] = dt.Rows[i]["PARENT_UUID"].ToString();
                    parentRow["MASTER_DESC1"] = dt.Rows[i]["MASTER_DESC1"].ToString();
                    parentRow["MASTER_DESC2"] = dt.Rows[i]["MASTER_DESC2"].ToString();

                    parentDt.Rows.Add(parentRow);
                    //处理子表数据
                    foreach (DataRow row in dt.Rows)
                    {
                        DataRow subRow = subDt.NewRow();
                        subUuid = System.Guid.NewGuid().ToString();

                        subRow["UUID"] = subUuid;
                        subRow["PARENT_UUID"] = parentUuid;
                        subRow["SUB_DESC1"] = row["SUB_DESC1"].ToString();
                        subRow["SUB_DESC2"] = row["SUB_DESC2"].ToString(); ;
                        subRow["SUB_DESC3"] = row["SUB_DESC3"].ToString(); ;
                        subDt.Rows.Add(subRow);
                    }
                }
                #endregion
                marstData = parentDt.Copy();
                subData = subDt.Copy();
            }
            catch (Exception ex)
            {
                mesg = "程序有问题：" + ex.Message;
            }
        }
        #endregion

    }
}
    
