using ISK_Proj.Genetics;
using System;
using System.Linq;

namespace ISK_Proj
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = Graph.readFromFile("D:/t.xml");

            var population = 1000;
            var size = 1000;

            foreach (var edge in graph.Edges)
            {
                System.Console.WriteLine(edge.toString);
            }

            var painter = new GraphPainter(graph, population, size, graph.Vertices.ToArray());
            var coloredGraph = painter.ColorGraph();
            System.Console.WriteLine(coloredGraph.PrintColors());
        }
    }
}
