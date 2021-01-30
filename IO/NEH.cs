using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace IO
{
    class NEH
    {
        private Dictionary<int, int[]> tasks;
        public int rows, cols;
        public NEH(Dictionary<int, int[]> tasks)
        {
            this.tasks = tasks;
            rows = tasks.Keys.Count;
            cols = 11;
        }

        public  Schedule Optimize()
        {
            int[,] insertOrder = new int[rows, 2];
            int[] taskIDs = tasks.Keys.ToArray();

            for (int i=0; i < rows; i++)
            {
                insertOrder[i, 0] = taskIDs[i];
                insertOrder[i, 1] = 0;

                int[] times = tasks[i + 1];
                for(int j=0; j < cols; j++)
                {
                    insertOrder[i, 1] += times[j];
                }
            }
            insertOrder = insertOrder.OrderByDescending(x => x[1]);

            Dictionary<int, int[]> subTasks = new Dictionary<int, int[]>();
            List<int> order = new List<int>();
            Schedule sh = null;
            
            for (int i = 0; i < insertOrder.GetLength(0); i++)
            {
                int currentTaskID = insertOrder[i, 0];
                subTasks.Add(currentTaskID, tasks[currentTaskID]);
                sh = new Schedule(subTasks);

                int minCost = int.MaxValue;
                int minIndx = 0;

                for (int j=0; j<=i; j++)
                {
                    order.Insert(j, currentTaskID);

                    sh.ChangeOrder(order.ToArray());

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

            return sh;
        }
    }
}
