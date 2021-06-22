using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISK_Proj.Genetics
{
    class GraphPainter
    {
        private Graph graph { get; set; }
        private int population { get; set; }
        private int populationSize { get; set; }
        private int[] startValues { get; set; }

        public GraphPainter(Graph graph, int population, int size, int[] startValues)
        {
            this.graph = graph;
            this.population = population;
            this.populationSize = size;
            this.startValues = startValues;
        }

        public Graph ColorGraph()
        {
            GraphProvider.SetGraph(graph);
            GraphColoringChromosome chromosome = new GraphColoringChromosome(startValues.Length, startValues, population);
            Population populationObject = new Population(populationSize, populationSize, chromosome);
            GraphColoringFitness fitness = new GraphColoringFitness();
            EliteSelection selection = new EliteSelection();
            CutAndSpliceCrossover crossover = new CutAndSpliceCrossover();
            GraphColoringMutation mutation = new GraphColoringMutation();
            FitnessStagnationTermination termination = new FitnessStagnationTermination(50);
            GeneticAlgorithm ga = new GeneticAlgorithm(populationObject, fitness, selection, crossover, mutation)
            { Termination = termination, MutationProbability = 0.1f };
            int latestFitness = int.MinValue;
            int bestFitness = 0;
            GraphColoringChromosome bestChromosome = null;
            ga.GenerationRan += (sender, e) =>
            {
                bestChromosome = ga.BestChromosome as GraphColoringChromosome;
                bestFitness = (int)-bestChromosome.Fitness.Value;

                if (bestFitness == latestFitness) return;
                latestFitness = bestFitness;
                bestChromosome.GetValues();
            };
            ga.Start();

            Console.WriteLine("End");

            if (bestFitness > graph.Vertices.Count) return null;

            int[] resultGenType = bestChromosome.GetValues();

            IDictionary<int, int> verticesColors = new Dictionary<int, int>(graph.Vertices.Count);

            foreach (int vertice in graph.Vertices)
            {
                verticesColors.Add(vertice, resultGenType[vertice - 1]);
            }

            graph.VerticesColors = verticesColors;

            return graph;
        }
    }
}
