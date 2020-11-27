using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImportExcelFileTemplate.Helpers
{
    public static class FileReader
    {
        public static List<List<string>> ExtractDataFromStream(Stream stream)
        {
            var dataSetResult = new DataSet();

            using (IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream))
            {
                dataSetResult = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (config) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                DataTable dataSet = dataSetResult.Tables[0];

                int dataSetRowsCount = dataSet.Rows.Count;
                int dataSetColumnsCount = dataSet.Columns.Count;

                List<List<String>> rowList = new List<List<string>>(dataSetRowsCount);
                List<String> colValueList = null;


                foreach (DataRow row in dataSet.Rows)
                {
                    colValueList = new List<string>(dataSetColumnsCount);
                    object colValue = null;

                    foreach (DataColumn column in dataSet.Columns)
                    {
                        colValue = row[column];
                        colValueList.Add(colValue.ToString());
                    }

                    rowList.Add(colValueList);
                };

                return rowList;
            }
        }
    }
}
