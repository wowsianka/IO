using System;
using System.Collections.Generic;
using System.IO;

namespace IO
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = "../../data.xlsx";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            Dictionary<int, int[]> tasks = TasksDAO.GetTasks(path);

            //GradientDescent opt = new GradientDescent(tasks, 20000);
            //Schedule result = opt.Optimize();
            //result.PrintResult();
            //Console.WriteLine(result.GetTotalCost());

            //NEH neh = new NEH(tasks);
            //Schedule sh = neh.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());

            //FRB2 frb2 = new FRB2(tasks);
            //Schedule sh = frb2.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());

            //SimulatedAnnealing sa = new SimulatedAnnealing(tasks, 500, 0.99, 5000);
            //Schedule sh = sa.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());

            Tabu tabu = new Tabu(tasks, 10);
            Schedule sh = tabu.Optimize();
            sh.PrintResult(); // 3201

            Console.Read();
        }
    }
}
