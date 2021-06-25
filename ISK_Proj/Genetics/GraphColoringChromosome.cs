using System;
using GeneticSharp.Domain.Chromosomes;

namespace ISK_Proj.Genetics
{
    public class GraphColoringChromosome : ChromosomeBase
    {
        private readonly int[] m_geneValues;
        private readonly int m_length;

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

        public GraphColoringChromosome(int length, int[] geneValues) : base(length)
        {
            m_length = length;
            m_geneValues = geneValues;
            CreateGenes();
        }

        public override Gene GenerateGene(int geneIndex)
        {
            return new Gene(m_geneValues[geneIndex]);
        }

        public override IChromosome CreateNew()
        {
            int[] geneValues;
            geneValues = (int[])RandomizeGenes(m_geneValues).Clone();

            return new GraphColoringChromosome(m_length, geneValues);
        }

        public int[] GetValues()
        {
            return m_geneValues;
        }
    }
}
