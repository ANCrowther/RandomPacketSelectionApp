using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace ExcelManagerLibrary.Managers
{
    internal class ExcelImporter
    {
        public List<T> ImportExcel<T>(string excelFilePath, string excelSheetName)
        {
            List<T> list = new List<T>();
            Type objectType = typeof(T);

            using(var workbook = new XLWorkbook(excelFilePath))
            {

            }


            using(IXLWorkbook wb = new XLWorkbook(excelFilePath))
            {
                var worksheet = wb.Worksheets.Where(w=>w.Name==excelSheetName).First();
                var properties = objectType.GetProperties();
                var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });

                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    T obj = (T)Activator.CreateInstance(objectType);
                    int columnIndex = 0;
                    foreach(var prop in properties)
                    {
                        columnIndex++;
                        if (columnIndex > 30)
                        {
                            break;
                        }
                        //int columnIndex = columns.SingleOrDefault(c=>c.Value.ToString() == prop.Name.ToString()).Index;
                        var val = row.Cell(columnIndex).Value;
                        var type = prop.PropertyType;
                        if (val == DBNull.Value || String.IsNullOrWhiteSpace(val.ToString()))
                        {
                            if(type == typeof(string))
                            {
                                val = string.Empty;
                            }
                            if(type == typeof(DateTime))
                            {
                                val = DateTime.MinValue;
                            }
                            if(type == typeof(double))
                            {
                                val = 0.0;
                            }
                            if(type == typeof(decimal))
                            {
                                val = 0.0m;
                            }
                        }


                        prop.SetValue(obj, Convert.ChangeType(val, type));
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
