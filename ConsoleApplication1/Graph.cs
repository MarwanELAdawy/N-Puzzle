using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Graph
    {
        private int vertices;
        private List<int>[,] adj;

        public Graph(int v)
        {
            vertices = v;
            adj = new List<int>[v, v];
            for (int i = 0; i < v; i++)
                for (int j = 0; j < v; j++)
                    adj[i, j] = new List<int>();
        }

        public void addedge(int c, int d, int v)
        {
            adj[c, d].Add(v);
            List<int> arr1 = new List<int>();
            arr1.Add(v);
        }

        public void DisplayGraph()
        {
            int counter = 0;
            for (int v = 0; v < vertices; v++)
                for (int i = 0; i < vertices; i++)
                {
                    Console.WriteLine(" Adjacency list of vertex" + counter);
                    counter++;
                    foreach (int j in adj[v, i])
                    {
                        Console.WriteLine("->" + j);
                    }

                    Console.WriteLine();
                }
        }
    }
}
