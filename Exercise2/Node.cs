using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Exercise2
{
    public class Node
    {
        public Node()
        {
        }

        public List<Tuple<string, int>> Map(string arg)
        {
            string[] input = arg.Split(" ");
            return input.Select(x => new Tuple<string, int>(x, 1)).ToList();
        }

        public Tuple<string, int> Reduce(List<Tuple<string, int>> mappedInput)
        {
            Dictionary<string, int> reducedList = new Dictionary<string, int>();
            Tuple<string, List<int>> intermediaryTuple = new Tuple<string, List<int>>(
                mappedInput.First().Item1, new List<int>() { 1 });

            intermediaryTuple.Item2.AddRange(mappedInput.Select(_ => 1));

            Tuple<string, int> reduceResult = new Tuple<string, int>(
                intermediaryTuple.Item1, intermediaryTuple.Item2.Sum());

            return reduceResult;
        }
    }
}