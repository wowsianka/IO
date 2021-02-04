using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class Tabu : IOptimizer
    {
        private Dictionary<int, int[]> tasks;
        private Schedule schedule;
        private int rows;

        private int k;

        public Tabu(Dictionary<int, int[]> tasks, int k)
        {
            this.tasks = tasks;
            this.k = k;

            rows = tasks.Keys.Count;

            schedule = new Schedule(tasks);
        }

        public Schedule Optimize()
        {
            Queue<Tuple<int, int>> tabu = new Queue<Tuple<int, int>>();
            schedule.UpdateCostsFrom(0);

            for(int i=0; i < rows; i++)
            {
                int bestIndex = i;
                int oldCost = schedule.GetTotalCost();

                for(int j=0; j < rows; j++)
                {
                    if(i == j ||
                        tabu.Contains(new Tuple<int, int>(i, j)) ||
                        tabu.Contains(new Tuple<int, int>(j, i)))
                    {
                        continue;
                    }
                    
                    schedule.SwapAndUpdate(i, j);

                    int newCost = schedule.GetTotalCost();

                    if (newCost < oldCost)
                    {
                        oldCost = newCost;
                        bestIndex = j;
                    }

                    // swap back
                    schedule.SwapAndUpdate(i, j);
                }

                if (bestIndex != i)
                {
                    schedule.SwapAndUpdate(i, bestIndex);

                    tabu.Enqueue(new Tuple<int, int>(bestIndex, i));
                    if(tabu.Count >= k)
                        tabu.Dequeue();
                }
            }

            return schedule;
        }
    }
}
