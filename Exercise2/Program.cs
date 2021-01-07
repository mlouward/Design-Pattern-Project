using System;
using System.Collections.Generic;

namespace Exercise2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Node n = new Node();
            List<Dictionary<string, int>> list = new List<Dictionary<string, int>>();

            list.Add(n.Map("bear the pretty"));
            list.Add(new Dictionary<string, int>() { { "               ", 0 } });
            list.Add(n.Map("the prettu bear"));
            list.Add(new Dictionary<string, int>() { { "               ", 0 } });
            list.Add(n.Map("the"));
            list.Add(new Dictionary<string, int>() { { "               ", 0 } });
            // Send similar keys to same node to reduce
            foreach (var item in list)
            {
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.Key + " " + item2.Value);
                }
            }
        }
    }
}