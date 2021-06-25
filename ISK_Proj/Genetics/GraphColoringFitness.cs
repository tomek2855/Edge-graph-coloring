using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

namespace ISK_Proj.Genetics
{
    public class GraphColoringFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var localChromosome = chromosome as GraphColoringChromosome;

            var chromosomeValues = localChromosome.GetValues();
            var graph = GraphProvider.Graph;

            var hasAnyNeighborSameColor = false;
            var countOfBadColoring = 0.0;
            foreach (int vertex in graph.Vertices)
            {
                var neighbors = graph.NeighborsList(vertex).ToList();
                var colorOfCurrentVertex = chromosomeValues[vertex - 1];
                hasAnyNeighborSameColor = hasAnyNeighborSameColor ||
                                            neighbors.Any(x => chromosomeValues[x - 1] == colorOfCurrentVertex);
                countOfBadColoring += (double)(neighbors.Count(x => chromosomeValues[x - 1] == colorOfCurrentVertex) -1) /
                                        neighbors.Count;
            }

            if (hasAnyNeighborSameColor)
            {
                return -2 * countOfBadColoring;
            }

            return -(chromosomeValues.ToList().Distinct().Count());
        }
    }
}
