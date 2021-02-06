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
            //Schedule sh = opt.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // NEH

            //NEH neh = new NEH(tasks);
            //Schedule sh = neh.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // FRB2

            //FRB2 frb2 = new FRB2(tasks);
            //Schedule sh = frb2.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // SIMULATED ANNEALING

            //SimulatedAnnealing sa = new SimulatedAnnealing(tasks, 500, 0.99, 5000);
            //Schedule sh = sa.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // TABU SEARCH

            //Tabu tabu = new Tabu(tasks, 10);
            //Schedule sh = tabu.Optimize();
            //sh.PrintOrder(); // 3201
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // GENETIC ALGORITHM WITH TOURNAMENT

            //GAT ga = new GAT(tasks, 100, 0.2, 0.05, false);
            //Schedule sh = ga.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            ///////////////////////////////////////////////////////////////////////
            // GENETIC ALGORITHM WITH FIT FUNCTION

            //GAF ga = new GAF(tasks, 100, 0.2, 0.05, true);
            //Schedule sh = ga.Optimize();
            //sh.PrintOrder();
            //Console.WriteLine("TOTAL COST:" + sh.GetTotalCost().ToString());


            Console.Read();
        }
    }
}
