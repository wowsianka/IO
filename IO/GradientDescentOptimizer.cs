using System;

namespace IO
{
    class GradientDescentOptimizer : TaskScheduleOptimizer
    {
        private int MAX_INTER;
        public GradientDescentOptimizer(int[,] tasks, int iter) : base(tasks)
        {
            MAX_INTER = iter;
        }

        public override OptimizedResult optimize()
        {
            Random rnd = new Random();
            int x, y;

            UpdateCosts();

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

                UpdateCostsFrom(Math.Min(x, y));

                int newCost = costs[rows - 1, cols - 1];
                if (newCost > oldSum)
                {
                    costs[x, 0] = tasks[xTaskIdx, 0];
                    costs[y, 0] = tasks[yTaskIdx, 0];
                    UpdateCostsFrom(Math.Min(x, y));
                }
            }

            return new OptimizedResult(costs);
        }
    }
}
