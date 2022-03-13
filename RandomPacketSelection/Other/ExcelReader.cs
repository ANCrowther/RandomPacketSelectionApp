 // Nuget Package used [excel-mapper]:https://github.com/hughbe/excel-mapper
// Go to link to read MIT license: https://github.com/hughbe/excel-mapper/blob/master/LICENSE

using ExcelMapper;
using RandomPacketSelection.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomPacketSelection.Other
{
    public class ExcelReader
    {
        private ExcelSheet sheet;
        private ExcelInputs[] excelInputs;

        private List<ExcelInputs> excelInputsList;
        public List<ExcelInputs> ExcelInputsList
        {
            get { return excelInputsList; }
            private set { excelInputsList = value; }
        }

        public ExcelReader(string fileLocation)
        {
            ImportExcelForm(@fileLocation);
            ExcelInputsList = new List<ExcelInputs>();
            GetExcelInputsList();
        }

        // Converts the excelInputs array into a list and returns the list.
        private void GetExcelInputsList()
        {
            ExcelInputs[] temp = excelInputs.Distinct().ToArray();
            for (int index = 0; index < temp.Length; index++)
            {
                ExcelInputsList.Add(temp[index]);
            }
            
        }

        // Imports the data from the user selected excel form into an array.
        private void ImportExcelForm(string filePath)
        {
            FileStream stream = File.OpenRead(filePath);
            
            using (var importer = new ExcelImporter(stream))
            {
                sheet = importer.ReadSheet("DDMASTER");

                excelInputs = sheet.ReadRows<ExcelInputs>().ToArray();
            }
        }
    }
}
