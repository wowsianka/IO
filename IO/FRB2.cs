using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class FRB2
    {
        private Dictionary<int, int[]> tasks;
        public int rows, cols;
        public FRB2(Dictionary<int, int[]> tasks)
        {
            this.tasks = tasks;
            rows = tasks.Keys.Count;
            cols = 11;
        }

        public Schedule Optimize()
        {
            int[,] insertOrder = new int[rows * cols, 2];
            int[] taskIDs = tasks.Keys.ToArray();
            int ins = 0;

            for (int i = 0; i < rows; i++)
            {
                int[] times = tasks[taskIDs[i]];
                for (int j = 0; j <cols; j++)
                {
                    insertOrder[ins, 0] = taskIDs[i];
                    insertOrder[ins, 1] = times[j];
                    ins++;
                }
            }
            insertOrder = insertOrder.OrderByDescending(x => x[1]);

            Dictionary<int, int[]> subTasks = new Dictionary<int, int[]>();
            List<int> order = new List<int>();
            Schedule sh = null;
            int prevTaskID = 0;

            for (int i=0; i < rows * cols; i++)
            {
                int currentTaskID = insertOrder[i, 0];
                if (prevTaskID != currentTaskID)
                {
                    if (subTasks.ContainsKey(currentTaskID))
                    {
                        subTasks.Remove(currentTaskID);
                        order.Remove(currentTaskID);
                    }

                    subTasks.Add(currentTaskID, tasks[currentTaskID]);
                    sh = new Schedule(subTasks);

                    int minCost = int.MaxValue;
                    int minIndx = 0;

                    for (int j = 0; j < subTasks.Count; j++)
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

                    prevTaskID = currentTaskID;
                }
            }

            return sh;
        }
    }
}
