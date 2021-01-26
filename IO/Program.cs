using System;
using System.Collections.Generic;

namespace IO
{
    class Program
    {

        static void Main(string[] args)
        {
            string path = @"C:\Users\wowsi\OneDrive\Pulpit\IO\data.xlsx";
            Dictionary<int, int[]> tasks = TasksDAO.GetTasks(path);

            //GradientDescent opt = new GradientDescent(tasks, 5000);
            //Schedule result = opt.Optimize();

            //Console.WriteLine(result.GetTotalCost());
            //result.PrintResult();

            NEH neh = new NEH(tasks);
            Schedule sh = neh.Optimize();
            //sh.PrintResult();
            Console.WriteLine(sh.GetTotalCost());

            Console.Read();
        }
    }
}
