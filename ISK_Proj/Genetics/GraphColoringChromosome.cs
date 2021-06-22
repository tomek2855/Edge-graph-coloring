using System;
using GeneticSharp.Domain.Chromosomes;

namespace ISK_Proj.Genetics
{
    public class GraphColoringChromosome : ChromosomeBase
    {
        private readonly int[] m_geneValues;
        private readonly int m_length;
        private int m_population;

        private int[] RandomizeGenes(int[] geneValues)
        {
            Random rnd = new Random();
            for (int i = 0; i<geneValues.Length;i++)
            {
                int vertexColor = rnd.Next(0, geneValues.Length);
                geneValues[i] = vertexColor;
            }
            return geneValues;
        }

        public GraphColoringChromosome(int length, int[] geneValues, int population = 0) : base(length)
        {
            m_length = length;
            m_geneValues = geneValues;
            m_population = population;
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(m_geneValues[geneIndex]);
        }

        public override IChromosome CreateNew()
        {
            int[] geneValues;
            if (m_population > 0)
            {
                geneValues = (int[])RandomizeGenes(m_geneValues).Clone();
                m_population--;
            }
            else
            {
                geneValues = (int[])m_geneValues.Clone();
            }
            return new GraphColoringChromosome(m_length, geneValues, m_population);
        }

        public int[] GetValues()
        {
            return m_geneValues;
        }
    }
}
