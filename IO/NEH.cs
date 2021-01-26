using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IO
{
    class NEH
    {
        private int[,] tasks;
        public int rows, cols;
        public NEH(int[,] tasks)
        {
            this.tasks = tasks;
            rows = tasks.GetLength(0);
            cols = tasks.GetLength(1);
        }

        public  Schedule Optimize()
        {
            int[,] insertOrder = new int[tasks.Length, 2];

            //DataTable order = new DataTable();
            //order.NewRow();

            for (int i=0; i < rows; i++)
            {
                insertOrder[i, 0] = tasks[i, 0] - 1; // index of the task
                insertOrder[i, 1] = 0;
                for(int j=1; j < cols; j++)
                {
                    insertOrder[i, 1] += tasks[i, j];
                }
            }
            insertOrder = insertOrder.OrderByDescending(x => x[1]);

            
            List<int[]> subTasks = new List<int[]>();
            List<int> order = new List<int>();
            int minCost = int.MaxValue;
            int minIndx = 0;

            for (int i = 0; i < rows; i++)
            {
                int currentTaskID = insertOrder[i, 0] + 1; // not index but ID, so +1
                subTasks.Add(tasks.GetRow(currentTaskID));
                Schedule sh = new Schedule(subTasks.ToMultiArray());

                for(int j=0; j<i; i++)
                {
                    order.Insert(j, currentTaskID);

                    sh.ChangeOrder(order.ToArray());
                    sh.UpdateCostsFrom(j);
                    
                    int cost = sh.GetTotalCost();
                    if (cost < minCost)
                    {
                        minCost = cost;
                        minIndx = j;
                    }

                    order.RemoveAt(j);
                }
                order.Insert(minIndx, currentTaskID);
            }

            return new Schedule(order.ToArray(), tasks);
        }
    }
}
