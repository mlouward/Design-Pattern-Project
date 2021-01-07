using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Exercise2
{
    public class Node
    {
        public Node()
        {
        }

        public Dictionary<string, int> Map(string arg)
        {
            string[] input = arg.Split(" ");
            return input.Select(x => new KeyValuePair<string, int>(x, 1)).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> Reduce(List<Dictionary<string, int>> mappedInput)
        {
            Dictionary<string, List<int>> shuffledList = new Dictionary<string, List<int>>();
            Dictionary<string, int> reducedList = new Dictionary<string, int>();

            foreach (var dict in mappedInput)
            {
                foreach (KeyValuePair<string, int> keyValuePair in dict)
                {
                    if (shuffledList.ContainsKey(keyValuePair.Key))
                        shuffledList[keyValuePair.Key].Add(1);
                    else
                        shuffledList.Add(keyValuePair.Key, new List<int>() { 1 });
                }
            }

            foreach (string item in shuffledList.Keys)
            {
                if (reducedList.Keys.Contains(item))
                {
                    reducedList[item]++;
                }
                else
                {
                    reducedList.Add(item, 1);
                }
            }

            return reducedList;
        }
    }
}