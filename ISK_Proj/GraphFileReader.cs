using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ISK_Proj
{
    class GraphFileReader
    {
        private IList<string> lines { get; set; }

        public GraphFileReader(string path)
        {
            this.lines = File.ReadAllText(path).Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        }

        public int GetEdgesCount()
        {
            return int.Parse(lines[0].Split('>')[0].Substring(1));
        }

        public IList<Edge> GetEdges()
        {
            var i = 0;

            var list = new List<Edge>();

            lines.ToList().ForEach(a =>
            {
                if (i != 0) list.Add(GetEdge(a));
                i++;
            });

            return list;
        }

        private Edge GetEdge(string line)
        {
            var splited = new List<string>(line.Split(">", StringSplitOptions.RemoveEmptyEntries)).Select(x => x.Substring(1)).ToList();

            return new Edge(int.Parse(splited[0]), int.Parse(splited[1]), int.Parse(splited[2]));
        }
    }
}
