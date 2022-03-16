using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using ExcelManagerLibrary.Models;

namespace ExcelManagerLibrary.Managers
{
    internal class ExcelImporter
    {
        public List<ExcelInputs> Importer(string excelFilePath, string excelSheetName)
        {
            List<ExcelInputs> outputList = new List<ExcelInputs>();

            using (IXLWorkbook workbook = new XLWorkbook(excelFilePath))
            {
                var worksheet = workbook.Worksheets.Where(w=>w.Name == excelSheetName).First();
                
                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    int columnIndex = 1;
                    if (row.Cell(columnIndex + 10).IsEmpty())
                    {
                        continue;
                    }

                    ExcelInputs temp = new ExcelInputs
                    {
                        Sex = row.Cell(columnIndex + 1).Value.ToString(),
                        Race = row.Cell(columnIndex + 2).Value.ToString(),
                        IE_CI = row.Cell(columnIndex + 3).Value.ToString(),
                        PrimaryDisability = row.Cell(columnIndex + 4).Value.ToString(),
                        Status = row.Cell(columnIndex + 5).Value.ToString(),
                        File = row.Cell(columnIndex + 6).Value.ToString(),
                        DVR_Status = row.Cell(columnIndex + 7).Value.ToString(),
                        MonitoringPlan_StartDate = row.Cell(columnIndex + 8).Value.ToString(),
                        LastName = row.Cell(columnIndex + 9).Value.ToString(),
                        FirstName = row.Cell(columnIndex + 10).Value.ToString(),
                        WVSC = row.Cell(columnIndex + 11).Value.ToString(),
                        FileTransfer = row.Cell(columnIndex + 12).Value.ToString(),
                        Acuity = row.Cell(columnIndex + 13).Value.ToString(),
                        AdditionalStaff = row.Cell(columnIndex + 14).Value.ToString(),
                        FundingIncrease_ExpDate = row.Cell(columnIndex + 15).Value.ToString(),
                        ETRHours = row.Cell(columnIndex + 16).Value.ToString(),
                        TargetHours = row.Cell(columnIndex + 17).Value.ToString(),
                        UpToHours = row.Cell(columnIndex + 18).Value.ToString(),
                        JobCoachHours = row.Cell(columnIndex + 19).Value.ToString(),
                        JobCoach = row.Cell(columnIndex + 20).Value.ToString(),
                        Entry = row.Cell(columnIndex + 21).Value.ToString(),
                        Coaching = row.Cell(columnIndex + 22).Value.ToString(),
                        JobStart = row.Cell(columnIndex + 23).Value.ToString(),
                        JobPlace = row.Cell(columnIndex + 24).Value.ToString(),
                        CurrentWage = row.Cell(columnIndex + 25).Value.ToString(),
                        HoursPerWeek = row.Cell(columnIndex + 26).Value.ToString(),
                        JobLoss = row.Cell(columnIndex + 27).Value.ToString(),
                        JobLossReason = row.Cell(columnIndex + 28).Value.ToString(),
                        Closure = row.Cell(columnIndex + 29).Value.ToString(),
                        ClosureReason = row.Cell(columnIndex + 30).Value.ToString()
                    };
                    outputList.Add(temp);
                }
            }

            return outputList;
        }

        public List<T> ImportExcel<T>(string excelFilePath, string excelSheetName)
        {
            List<T> list = new List<T>();
            Type objectType = typeof(T);


            using(IXLWorkbook wb = new XLWorkbook(excelFilePath))
            {
                var worksheet = wb.Worksheets.Where(w=>w.Name==excelSheetName).First();
                var properties = objectType.GetProperties();
                var columns = worksheet.FirstRow().Cells().Select((v, i) => new { Value = v.Value, Index = i + 1 });

                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    T obj = (T)Activator.CreateInstance(objectType);
                    int columnIndex = 0;

                    foreach(IXLCell cell in row.Cells(1, 30))
                    {

                        if (cell.IsEmpty())
                        {
                            cell.GetType();
                        }
                    }

                    //foreach(var prop in properties)
                    //{
                    //    columnIndex++;
                    //    if (columnIndex > 30)
                    //    {
                    //        break;
                    //    }
                    //    //int columnIndex = columns.SingleOrDefault(c=>c.Value.ToString() == prop.Name.ToString()).Index;
                    //    var val = row.Cell(columnIndex).Value;
                    //    var type = prop.PropertyType;
                    //    if (val == DBNull.Value || String.IsNullOrWhiteSpace(val.ToString()))
                    //    {
                    //        continue;
                    //        //if(type == typeof(string))
                    //        //{
                    //        //    val = string.Empty;
                    //        //}
                    //        //if(type == typeof(DateTime))
                    //        //{
                    //        //    val = DateTime.MinValue;
                    //        //}
                    //        //if(type == typeof(double))
                    //        //{
                    //        //    val = 0.0;
                    //        //}
                    //        //if(type == typeof(decimal))
                    //        //{
                    //        //    val = 0.0m;
                    //        //}
                    //    }


                    //    prop.SetValue(obj, Convert.ChangeType(val, type));
                    //}

                    //list.Add(obj);
                }
            }

            //return list;
            throw new NotImplementedException();
        }
    }
}
