using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace IO
{
    class Program
    {
        public static int[,] ReadData(string path)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(path, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

            Excel.Range excelRange = excelWorksheet.Range["A2","K51"];
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


        static int GD(int[,] costs, int[,] devs)
        {
            Random rnd = new Random();
            int rows = costs.GetLength(0);
            int cols = costs.GetLength(1);
            int x, y;
            for (int iter=0; iter<5000; iter++)
            {
                do
                {
                    // x, y indeksy zadań w tablicy costs, które chcemy podmienić
                    x = rnd.Next(rows);
                    y = rnd.Next(rows);
                } while (x == y);

                int oldSum = costs[rows - 1, cols - 1];
                int x_dev = costs[x, 0] - 1;
                int y_dev = costs[y, 0] - 1;

                for(int i=0; i < cols; i++)
                {
                    costs[x, i] = devs[y_dev, i];
                    costs[y, i] = devs[x_dev, i];
                }

                updateCosts(costs, devs);

                int newCost = costs[rows - 1, cols - 1];
                if (newCost > oldSum)
                {
                    for (int i = 0; i < cols; i++)
                    {
                        costs[x, i] = devs[x_dev, i];
                        costs[y, i] = devs[y_dev, i];
                    }
                    updateCosts(costs, devs);
                }
            }

            return costs[rows - 1, cols - 1];
        }



        static void updateCosts(int[,] costs, int[,] devs)
        {
            int rows = costs.GetLength(0);
            int cols = costs.GetLength(1);

            // first row
            for (int j = 0; j < cols; j++)
            {
                int devIdx = costs[0, 0] - 1;
                costs[0, j] = devs[devIdx, j];
            }
            for (int j=2; j<cols; j++)
            {
                costs[0, j] += costs[0, j - 1];
            }

            // other rows
            for(int i=1; i<rows; i++)
            {
                int devIdx = costs[i, 0] - 1;

                costs[i, 1] = costs[i - 1, 1] + devs[devIdx, 1];

                for(int j=2; j<cols; j++)
                {
                    costs[i, j] = devs[devIdx, j] +
                        Math.Max(costs[i - 1, j], costs[i, j - 1]);
                }
            }
        }

        static void print(int[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(arr[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string path = @"C:\Users\wowsi\OneDrive\Pulpit\IO\data.xlsx";

            int[,] data = ReadData(path);
            int rows = data.GetLength(0);
            int cols = data.GetLength(1);

            //print(data);

            // TODO lets hope this makes a deepp copy
            int[,] costs = new int[rows, cols];
            Array.Copy(data, costs, data.Length);
            // If not:
            //for (int i = 0; i < rows; i++)
            //    for (int j = 0; j < cols; j++)
            //        costs[i, j] = data[i, j];

            //updateCosts(costs, devs);
            //print(costs);

            int cost = GD(costs, data);
            print(costs);
            Console.WriteLine(cost);

            Console.Read();
        }
    }
}
