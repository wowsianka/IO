using System;

namespace IO
{
    class GradientDescent
    {
        private int MAX_INTER;
        public int[,] tasks;
        public Schedule schedule;
        public int rows, cols;

        public GradientDescent(int[,] tasks, int iter)
        {
            MAX_INTER = iter;
            this.tasks = tasks;
            rows = tasks.GetLength(0);
            cols = tasks.GetLength(1);
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
                int xTaskIdx = costs[x, 0] - 1;
                int yTaskIdx = costs[y, 0] - 1;

                costs[x, 0] = tasks[yTaskIdx, 0];
                costs[y, 0] = tasks[xTaskIdx, 0];

                schedule.UpdateCostsFrom(Math.Min(x, y));

                int newCost = costs[rows - 1, cols - 1];
                if (newCost > oldSum)
                {
                    costs[x, 0] = tasks[xTaskIdx, 0];
                    costs[y, 0] = tasks[yTaskIdx, 0];
                    schedule.UpdateCostsFrom(Math.Min(x, y));
                }
            }

            return schedule;
        }
    }
}
