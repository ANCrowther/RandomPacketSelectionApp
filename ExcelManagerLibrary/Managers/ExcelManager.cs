using ExcelManagerLibrary.Models;
using System;
using System.Collections.Generic;

namespace ExcelManagerLibrary.Managers
{
    public class ExcelManager
    {
        public List<ExcelInputs> Inputs { get; set; }

        public ExcelManager(string excelFilePath, string excelSheetName)
        {
            Inputs = GetExcelSheet(excelFilePath, excelSheetName);
        }

        private List<ExcelInputs> GetExcelSheet(string excelFilePath, string excelSheetName)
        {
            var outputList = new ExcelImporter().Importer(excelFilePath, excelSheetName);
            return outputList;
        }
    }
}
