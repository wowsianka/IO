using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class SimulatedAnnealing : IOptimizer
    {
        private Dictionary<int, int[]> tasks;
        private Schedule schedule;
        private int rows, cols;

        private double T;
        private double alpha;
        private int MAX_ITER;

        public SimulatedAnnealing(Dictionary<int, int[]> tasks, double T, double alpha, int MAX_ITER)
        {
            this.T = T;
            this.alpha = alpha;
            this.MAX_ITER = MAX_ITER;
            this.tasks = tasks;

            rows = tasks.Keys.Count;
            cols = 11;

            schedule = new Schedule(tasks);
        }

        public Schedule Optimize()
        {
            Random rnd = new Random();
            int x, y;

            schedule.UpdateCostsFrom(0);
            int[,] costs = schedule.GetCosts();

            for(int i=0; i < MAX_ITER; i++)
            {
                do
                {
                    // x, y indeksy zadań w tablicy costs, które chcemy podmienić
                    x = rnd.Next(rows);
                    y = rnd.Next(rows);
                } while (x == y);

                int oldCost = costs[rows - 1, cols - 1];
                
                schedule.SwapAndUpdate(x, y);
                
                int newCost = costs[rows - 1, cols - 1];

                if (newCost > oldCost)
                {
                    int De = newCost - oldCost;
                    double p = Math.Exp(-De / T);

                    if (rnd.NextDouble() > p)
                    {
                        schedule.SwapAndUpdate(x, y);
                    }
                }
                T *= alpha;
            }

            return schedule;
        }
    }
}
