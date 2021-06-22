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
            GraphColoringChromosome localChromosome = chromosome as GraphColoringChromosome;
            if(localChromosome == null) throw new InvalidProgramException("Chromosome cannot be null");
            int[] chromosomeValues = localChromosome.GetValues();
            Graph graph = GraphProvider.Graph;

            bool hasAnyNeighborSameColor = false;
            double countOfBadColoring = 0;
            foreach (int vertex in graph.Vertices)
            {
                IList<int> neighbors = graph.NeighborsList(vertex).ToList();
                int colorOfCurrentVertex = chromosomeValues[vertex - 1];
                hasAnyNeighborSameColor = hasAnyNeighborSameColor ||
                                            neighbors.Any(x => chromosomeValues[x - 1] == colorOfCurrentVertex);
                countOfBadColoring += (double)(neighbors.Count(x => chromosomeValues[x - 1] == colorOfCurrentVertex) -1) /
                                        neighbors.Count;
            }

            if (hasAnyNeighborSameColor)
            {
                return -(countOfBadColoring + graph.Vertices.Count+1);
            }

            return -(chromosomeValues.ToList().Distinct().Count());
        }
    }
}
