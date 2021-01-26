using System;

namespace IO
{
    abstract class TaskScheduleOptimizer
    {
        public int[,] tasks;
        public int[,] costs;
        public int rows, cols;

        public TaskScheduleOptimizer(int[,] tasks)
        {
            this.tasks = tasks;
            rows = tasks.GetLength(0);
            cols = tasks.GetLength(1);

            costs = new int[rows, cols];
            InitCosts();
        }

        private void InitCosts()
        {
            // TODO: random initial order
            for(int i=0; i < rows; i++)
            {
                costs[i, 0] = i + 1;
            }
        }

        public void UpdateCosts()
        {
            UpdateCostsFrom(0);
        }

        public void UpdateCostsFrom(int idx)
        {
            if(idx == 0)
            {
                // first cell
                int taskIdx = costs[0, 0] - 1;
                costs[0, 1] = tasks[taskIdx, 1];

                // rest of cell in first row
                for (int j = 2; j < cols; j++)
                {
                    costs[0, j] = costs[0, j - 1] + tasks[taskIdx, j];
                }

                idx++;
            }
            for (int i = idx; i < rows; i++)
            {
                int taskIdx = costs[i, 0] - 1;
                costs[i, 1] = costs[i - 1, 1] + tasks[taskIdx, 1];

                for (int j = 2; j < cols; j++)
                {
                    costs[i, j] = tasks[taskIdx, j] +
                        Math.Max(costs[i - 1, j], costs[i, j - 1]);
                }
            }
        }

        abstract public OptimizedResult optimize();
    }
}
