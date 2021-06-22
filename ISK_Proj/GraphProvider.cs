using System;
using System.Collections.Generic;
using System.Text;

namespace ISK_Proj
{
    class GraphProvider
    {
        public static Graph Graph { get; private set; }

        public static void SetGraph(Graph graph)
        {
            Graph = graph;
        }
    }
}
