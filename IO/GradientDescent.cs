using System;
using System.Collections.Generic;

namespace IO
{
    class GradientDescent
    {
        private int MAX_INTER;
        public Dictionary<int, int[]> tasks;
        public Schedule schedule;
        public int rows, cols;

        public GradientDescent(Dictionary<int, int[]> tasks, int iter)
        {
            MAX_INTER = iter;
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

            for (int iter = 0; iter < MAX_INTER; iter++)
            {
                do
                {
                    // x, y indeksy zadań w tablicy costs, które chcemy podmienić
                    x = rnd.Next(rows);
                    y = rnd.Next(rows);
                } while (x == y);

                int oldSum = costs[rows - 1, cols - 1];
                int xTaskID = costs[x, 0];
                int yTaskID = costs[y, 0];

                costs[x, 0] = yTaskID;
                costs[y, 0] = xTaskID;

                schedule.UpdateCostsFrom(Math.Min(x, y));

                int newCost = costs[rows - 1, cols - 1];
                if (newCost > oldSum)
                {
                    costs[x, 0] = xTaskID;
                    costs[y, 0] = yTaskID;
                    schedule.UpdateCostsFrom(Math.Min(x, y));
                }
            }

            return schedule;
        }
    }
}
