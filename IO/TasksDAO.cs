using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace IO
{
    class TasksDAO
    {
        public static int[,] getTasks(string path)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

            Excel.Range excelRange = excelWorksheet.Range["A2", "K51"];
            int rows = excelRange.Rows.Count;
            int cols = excelRange.Columns.Count;

            Object[,] values = (Object[,])excelRange.Cells.Value;
            int[,] array = new int[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    array[i, j] = Convert.ToInt32(values.GetValue(i + 1, j + 1));

            excelWorkbook.Close();
            excelApp.Quit();

            return array;
        }
    }
}
