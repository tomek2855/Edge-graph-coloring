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
            var chromosome = new GraphColoringChromosome(startValues.Length, startValues, population);
            var populationObject = new Population(populationSize, populationSize, chromosome);
            var fitness = new GraphColoringFitness();
            var selection = new EliteSelection();
            var crossover = new CutAndSpliceCrossover();
            var mutation = new GraphColoringMutation();
            var termination = new FitnessStagnationTermination(50);
            var ga = new GeneticAlgorithm(populationObject, fitness, selection, crossover, mutation)
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

            if (bestFitness > graph.Vertices.Count) return null;

            var resultGenType = bestChromosome.GetValues();

            var verticesColors = new Dictionary<int, int>(graph.Vertices.Count);

            foreach (var vertice in graph.Vertices)
            {
                verticesColors.Add(vertice, resultGenType[vertice - 1]);
            }

            graph.VerticesColors = verticesColors;

            return graph;
        }
    }
}
