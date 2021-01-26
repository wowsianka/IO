using System;

namespace IO
{
    class Program
    {

        static void Main(string[] args)
        {
            string path = @"C:\Users\wowsi\OneDrive\Pulpit\IO\data.xlsx";
            int[,] tasks = TasksDAO.getTasks(path);

            GradientDescentOptimizer opt = new GradientDescentOptimizer(tasks, 5000);
            OptimizedResult result = opt.optimize();

            Console.WriteLine(result.GetTotalCost());
            result.PrintResult();

            Console.Read();
        }
    }
}
