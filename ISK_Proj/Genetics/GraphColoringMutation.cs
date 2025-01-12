﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

namespace ISK_Proj.Genetics
{
    public class GraphColoringMutation : MutationBase, IMutation
    {
        private readonly IRandomization m_rnd;

        public GraphColoringMutation()
        {
            m_rnd = RandomizationProvider.Current;
        }
        protected override void PerformMutate(IChromosome chromosome, float probability)
        {
            var cpChromosome = chromosome as GraphColoringChromosome;
            var rand = m_rnd.GetDouble();
            if (!(rand <= probability)) return;
                
            var genes = cpChromosome.GetValues();

            var graph = GraphProvider.Graph;

            foreach (var vertex in graph.Vertices)
            {
                var p = graph.NeighborsList(vertex);
                if (p.Any(z => genes[z - 1] == genes[vertex - 1]))
                    genes[vertex - 1] = m_rnd.GetInt(0, chromosome.Length);
            }
        }
    }
}
