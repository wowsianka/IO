using System;

namespace IO
{
    class OptimizedResult
    {
        private int[,] costs;

        public OptimizedResult(int[,] costs)
        {
            this.costs = costs;
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
