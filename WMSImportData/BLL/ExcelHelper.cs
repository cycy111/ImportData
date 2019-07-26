using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
namespace WMSImportData.BLL
{
    public static class ExcelHelper
    {
        //private static IExcelDataReader GetExcelDataReader(string path)
        //{
        //    using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
        //    {
        //        IExcelDataReader dataReader;

        //        if (path.EndsWith(".xls"))
        //            dataReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
        //        else if (path.EndsWith(".xlsx"))
        //            dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
        //        else
        //            throw new FieldAccessException("The file to be processed is not an Excel file");       
                
        //        return dataReader;
        //    }
        //}

        private static DataSet GetExcelDataAsDataSet(string path, bool isFirstRowAsColumnNames)
        {
            using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader dataReader;

                if (path.EndsWith(".xls"))
                    dataReader = ExcelReaderFactory.CreateBinaryReader(fileStream);
                else if (path.EndsWith(".xlsx"))
                    dataReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                else
                    throw new FieldAccessException("The file to be processed is not an Excel file");

                return dataReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = isFirstRowAsColumnNames
                    }
                });
            }

             
        }


        public static DataTableCollection GetData(string path=null,  bool isFirstRowAsColumnNames = true)
        {
            //return from DataRow row in GetExcelWorkSheet(path, isFirstRowAsColumnNames).Rows select row;
            return GetExcelDataAsDataSet(path, isFirstRowAsColumnNames).Tables;

        }
       
    }
}
