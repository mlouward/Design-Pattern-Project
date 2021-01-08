using System;
using System.Collections.Generic;

namespace Exercise2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Node n = new Node();
            List<Tuple<string, int>> mapResult;

            mapResult = n.Map("Dear Bear River");
            Tuple<string, int> res = n.Reduce(mapResult);
            mapResult = n.Map("Car Car River");
            mapResult = n.Map("Dear Car Bear");
            // Send similar keys to same node to reduce
        }
    }
}