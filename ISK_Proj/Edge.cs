using System;
using System.Collections.Generic;
using System.Text;

namespace ISK_Proj
{
    class Edge
    {
        public int Id { get; }
        public int Source { get; }
        public int Target { get; }

        public Edge(int id, int source, int target)
        {
            Id = id;
            Source = source;
            Target = target;
        }

        public string toString { get
            {
                return $"Numer krawędzi: {Id} Z: {Source} Do: {Target}";
            }
        }
    }
}
