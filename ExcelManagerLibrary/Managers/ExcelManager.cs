using ExcelManagerLibrary.Models;
using System;
using System.Collections.Generic;

namespace ExcelManagerLibrary.Managers
{
    public class ExcelManager
    {
        //private List<ExcelInputs> _inputs;
        public List<ExcelInputs> Inputs { get; set; }

        public List<ClientModel> ClientList { get; set; }

        public ExcelManager(string excelFilePath, string excelSheetName)
        {
            //_inputs = new List<ExcelInputs>();
            Inputs = GetExcelSheet(excelFilePath, excelSheetName);
            ClientList = MakeClientList();
        }

        private List<ExcelInputs> GetExcelSheet(string excelFilePath, string excelSheetName)
        {
            var outputList = new ExcelImporter().ImportExcel<ExcelInputs>(excelFilePath,excelSheetName);

            return outputList;
        }

        private List<ClientModel> MakeClientList()
        {
            List<ClientModel> outputList = new List<ClientModel>();
            foreach (ExcelInputs e in Inputs)
            {
                outputList.Add(new ClientModel
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    WVSC = e.WVSC
                });
            }

            return outputList;
        }
    }
}
