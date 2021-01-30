using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class Schedule
    {
        private Dictionary<int, int[]> tasks;
        private int[,] costs;
        private int rows, cols;

        public Schedule(Dictionary<int, int[]> tasks)
        {
            this.tasks = tasks;
            rows = tasks.Keys.Count;
            cols = 11;

            costs = new int[rows, cols];
            int i = 0;
            foreach(int taskID in tasks.Keys)
            {
                costs[i++, 0] = taskID;
            }


            UpdateCostsFrom(0);
        }

        public Schedule(int[] order, Dictionary<int, int[]> tasks)
        {
            this.tasks = tasks;
            rows = tasks.Keys.Count;
            cols = 11;

            costs = new int[rows, cols];
            ChangeOrder(order);
        }

        public void UpdateCostsFrom(int idx)
        {
            if (idx == 0)
            {
                // first cell
                int taskID = costs[0, 0];
                costs[0, 1] = tasks[taskID][1];

                // rest of cell in first row
                for (int j = 2; j < cols; j++)
                {
                    costs[0, j] = costs[0, j - 1] + tasks[taskID][j];
                }

                idx++;
            }
            for (int i = idx; i < rows; i++)
            {
                int taskID = costs[i, 0];
                costs[i, 1] = costs[i - 1, 1] + tasks[taskID][1];

                for (int j = 2; j < cols; j++)
                {
                    costs[i, j] = tasks[taskID][j] +
                        Math.Max(costs[i - 1, j], costs[i, j - 1]);
                }
            }
        }

        public void ChangeOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                costs[i, 0] = order[i];
            }
            UpdateCostsFrom(0); 
        }

        public int[,] GetCosts()
        {
            return costs;
        }

        public int GetTotalCost()
        {
            return costs[costs.GetLength(0) - 1, costs.GetLength(1) - 1];
        }

        public int[,] GetOrder()
        {
            throw new NotImplementedException();
        }

        public void PrintResult()
        {
            for (int i = 0; i < costs.GetLength(0); i++)
            {
                for (int j = 0; j < costs.GetLength(1); j++)
                {
                    Console.Write(costs[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
