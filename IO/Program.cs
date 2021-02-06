using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IO
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = "../../data.xlsx";
            string path = Path.Combine(Environment.CurrentDirectory, fileName);

            Dictionary<int, int[]> tasks = TasksDAO.GetTasks(path);

            ///////////////////////////////////////////////////////////////////////
            // EACH COMMENT IS A DIFFERENT ALGORITHM
            // UNCOMMENT ONE AND RUN
            ///////////////////////////////////////////////////////////////////////



            ///////////////////////////////////////////////////////////////////////
            // HILL CLIMBING

            //HillClimbing opt = new HillClimbing(tasks, 10000);
            //Schedule result = opt.Optimize();
            //result.PrintResult();
            //Console.WriteLine(result.GetTotalCost());


            ///////////////////////////////////////////////////////////////////////
            // NEH

            //NEH neh = new NEH(tasks);
            //Schedule sh = neh.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());


            ///////////////////////////////////////////////////////////////////////
            // FRB2

            FRB2 frb2 = new FRB2(tasks);
            Schedule sh = frb2.Optimize();
            sh.PrintResult();
            Console.WriteLine(sh.GetTotalCost());


            ///////////////////////////////////////////////////////////////////////
            // SIMULATED ANNEALING

            //SimulatedAnnealing sa = new SimulatedAnnealing(tasks, 500, 0.99, 5000);
            //Schedule sh = sa.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());


            ///////////////////////////////////////////////////////////////////////
            // TABU SEARCH

            //Tabu tabu = new Tabu(tasks, 10);
            //Schedule sh = tabu.Optimize();
            //sh.PrintResult(); // 3201


            ///////////////////////////////////////////////////////////////////////
            // GENETIC ALGORITHM WITH TOURNAMENT

            //GAT ga = new GAT(tasks, 100, 0.2, 0.05, false);
            //Schedule sh = ga.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());


            ///////////////////////////////////////////////////////////////////////
            // GENETIC ALGORITHM WITH FIT FUNCTION

            //GAF ga = new GAF(tasks, 100, 0.2, 0.05);
            //Schedule sh = ga.Optimize();
            //sh.PrintResult();
            //Console.WriteLine(sh.GetTotalCost());


            Console.Read();
        }
    }
}
