/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace SharpSword.Excel
{
    /// <summary>
    /// 
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 读取EXCEL数据
        /// </summary>
        /// <param name="stream">EXCEL文件字节数据</param>
        /// <param name="sheetName">EXCEL工作簿名称</param>
        /// <param name="firstRowColumn">是否将第一行作为字段名称</param>
        /// <param name="excelType">EXCEL类型</param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(byte[] stream, string sheetName = null, bool firstRowColumn = true)
        {
            using (var memoryStream = new MemoryStream(stream))
            {
                return Excel2DataTable(memoryStream, sheetName, firstRowColumn);
            }
        }

        /// <summary>
        /// 读取EXCEL数据
        /// </summary>
        /// <param name="fileName">EXCEL文件路径</param>
        /// <param name="sheetName">EXCEL工作簿名称</param>
        /// <param name="firstRowColumn">是否将第一行作为字段名称</param>
        /// <param name="excelType">EXCEL类型</param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(string fileName, string sheetName = null, bool firstRowColumn = true)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return Excel2DataTable(fileStream, sheetName, firstRowColumn);
            }
        }

        /// <summary>
        /// 读取EXCEL数据
        /// </summary>
        /// <param name="stream">EXCEL数据流</param>
        /// <param name="sheetName">EXCEL工作簿名称</param>
        /// <param name="firstRowColumn">是否将第一行作为字段名称</param>
        /// <param name="excelType">EXCEL类型</param>
        /// <returns></returns>
        public static DataTable Excel2DataTable(Stream stream, string sheetName = null, bool firstRowColumn = true)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;

            try
            {
                //2007
                if (POIXMLDocument.HasOOXMLHeader(stream))
                    workbook = new XSSFWorkbook(stream);
                else
                    workbook = new HSSFWorkbook(stream);

                if (null != sheetName)
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (null == sheet)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }

                if (null != sheet)
                {
                    IRow firstRow = sheet.GetRow(0);

                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;

                    if (firstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (null != cell)
                            {
                                string cellValue = cell.StringCellValue;
                                if (null != cellValue)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        //没有数据的行默认是null　　　　
                        if (null == row)
                        {
                            continue;
                        }

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (null != row.GetCell(j))
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
