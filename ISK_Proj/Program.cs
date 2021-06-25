using ISK_Proj.Genetics;
using System;
using System.Linq;

namespace ISK_Proj
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.readFromFile("D:/graf.xml");

            var size = 100;

            foreach (var edge in graph.Edges)
            {
                System.Console.WriteLine(edge.toString);
            }

            var painter = new GraphPainter(graph, size, graph.Vertices.ToArray());
            var coloredGraph = painter.ColorGraph();
            coloredGraph.Print();
            System.Console.WriteLine("Użyto kolorów: " + coloredGraph.VerticesColors.Select(x=>x.Value).Distinct().Count());
        }
    }
}
