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
            System.Console.WriteLine(coloredGraph.PrintColors());
        }
    }
}
