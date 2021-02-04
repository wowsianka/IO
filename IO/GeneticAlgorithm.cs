using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    class GeneticAlgorithm : IOptimizer
    {
        class Genotype
        {
            public int[] Genes { get; set; }

            public int Cost { get; set; }

            public int Comp { get; set; }

            public double Prob { get; set; }

            public Genotype(int[] order, int cost)
            {
                Genes = order;
                this.Cost = cost;
                Comp = 4500 - cost;
            }
        }

        private Dictionary<int, int[]> tasks;
        private Schedule schedule;
        private int rows, cols;
        private double pk, pm;
        int generations;

        public GeneticAlgorithm(Dictionary<int, int[]> tasks, int generations, double pk, double pm)
        {
            this.tasks = tasks;
            this.generations = generations;
            this.pk = pk;
            this.pm = pm;
            rows = tasks.Keys.Count;
            cols = 11;
            schedule = new Schedule(tasks);
        }

        private Genotype Cross(Genotype parent1, Genotype parent2)
        {
            int[] childGenes = new int[rows];
            for (int i = 19; i < 30; i++)
                childGenes[i] = parent1.Genes[i];

            int position = 0;
            for(int i=0; i<rows; i++)
            {
                if (position == 19)
                    position += 10;

                Boolean found = false;
                for(int j=19; j < 29; j++)
                {
                    if (parent2.Genes[i] == childGenes[j])
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    childGenes[position] = parent2.Genes[i];
                    position++;
                }
            }

            schedule.ChangeOrderAndUpdate(childGenes);
            return new Genotype(childGenes, schedule.GetTotalCost());
        }

        public Schedule Optimize()
        {
            int compSum = 0;
            int[] taskIDs = tasks.Keys.ToArray();
            Random rnd = new Random();
            Genotype[] population = new Genotype[1000];

            for(int i=0; i<1000; i++)
            {
                int[] order = taskIDs.OrderBy(x => rnd.Next()).ToArray();
                schedule.ChangeOrderAndUpdate(order);
                population[i] = new Genotype(order, schedule.GetTotalCost());
            }
            population = population.OrderBy(x => x.Comp).ToArray();

            for(int p=0; p<generations; p++)
            {
                compSum = population.Sum(x => x.Comp);
                for (int i = 0; i < 1000; i++)
                    population[i].Prob = population[i].Comp / compSum;

                Genotype[] children = new Genotype[2000];
                int insert = 0;
                for (int w = 0; w < 1000; w++)
                {
                    HashSet<Genotype> distinct = new HashSet<Genotype>();
                    while (distinct.Count < 4)
                    {
                        Genotype g = population[rnd.Next(1000)];
                        if (!distinct.Contains(g))
                            distinct.Add(g);
                    }

                    Genotype[] cands = distinct.ToArray();
                    Genotype parent1 = cands[0].Prob > cands[1].Prob ? cands[0] : cands[1];
                    Genotype parent2 = cands[2].Prob > cands[3].Prob ? cands[2] : cands[3];

                    Genotype child1, child2;

                    if (rnd.NextDouble() > pk)
                    {
                        // make two children from conbination of parent genes
                        child1 = Cross(parent1, parent2);
                        child2 = Cross(parent2, parent1);
                    }
                    else
                    {
                        child1 = parent1;
                        child2 = parent2;
                    }

                    //if (rnd.NextDouble() < pm)
                    //{
                    //    // swap random genes
                    //}

                    children[insert++] = child1;
                    children[insert++] = child2;
                }

                children = children.OrderBy(x => x.Comp).ToArray();
                for (int i = 1000; i < 2000; i++)
                    population[i - 1000] = children[i];
            }

            Genotype minGen = null;
            int minCost = int.MaxValue;
            for (int i = 0; i < 1000; i++)
                if(population[i].Cost < minCost)
                {
                    minGen = population[i];
                    minCost = minGen.Cost;
                }
            schedule.ChangeOrderAndUpdate(minGen.Genes);
            return schedule;
        }
    }
}
