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

            using(IXLWorkbook wb = new XLWorkbook(excelFilePath))
            {
                var worksheet = wb.Worksheets.Where(w=>w.Name==excelSheetName).First();
                var properties = objectType.GetProperties();
                var columns = worksheet.FirstRow().Cells().Select((v, i) => new { v.Value, Index = i + 1 });

                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    T obj = (T)Activator.CreateInstance(objectType);

                    foreach(var prop in properties)
                    {
                        int columnIndex = columns.SingleOrDefault(c=>c.Value.ToString() == prop.Name.ToString()).Index;
                        var val = row.Cell(columnIndex).Value;
                        var type = prop.PropertyType;
                        prop.SetValue(obj, Convert.ChangeType(val, type));
                    }

                    list.Add(obj);
                }
            }

            return list;
        }
    }
}
