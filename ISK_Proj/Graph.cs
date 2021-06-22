using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISK_Proj
{
    class Graph
    {
        public int QuantityOfEdges { get; }
        public IList<Edge> Edges { get; }
        public IList<int> Vertices { get; }
        public IDictionary<int, int> VerticesColors { get; set; }

        public Graph(int quantityOfEdges, IList<Edge> edges)
        {
            QuantityOfEdges = quantityOfEdges;
            Edges = edges;
            Vertices = edges.Select(x => x.Source).Union(edges.Select(y => y.Target)).Distinct().ToList();
        }

        public IList<int> NeighborsList(int vertex)
        {
            IEnumerable<int> vertexFromStarting = Edges.Where(x => x.Source == vertex).Select(x => x.Target);
            IEnumerable<int> vertexFromEnding = Edges.Where(x => x.Target == vertex).Select(x => x.Source);
            return vertexFromEnding.Union(vertexFromStarting).Distinct().ToList();
        }

        public string PrintColors()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<int, int> item in VerticesColors)
            {
                stringBuilder.AppendLine($"Vertice: {item.Key} - Color: {item.Value}");
            }
            return stringBuilder.ToString();
        }

        public static Graph readFromFile(string path)
        {
            var graphFileReader = new GraphFileReader(path);

            var graph = new Graph(graphFileReader.GetEdgesCount(), graphFileReader.GetEdges());

            return graph;
        }
    }
}
