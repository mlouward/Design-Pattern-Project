using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Exercise2
{
    internal class Program
    {
        // one mapping node per input sentence, stored in dictionary where key is index of input sentence

        private static Dictionary<int, Node> MappingNodes { get; set; } = new Dictionary<int, Node>();

        //one reducing node per unique word in input, stored in dictionary where key is unique word

        private static Dictionary<string, Node> ReducingNodes { get; set; } = new Dictionary<string, Node>();

        private static List<string> Input { get; set; } = new List<string>();

        /// <summary>
        /// Sends each tuple of map output to the corresponding reducing node.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="node"></param>
        private static void SendToReducingNode(List<Tuple<string, int>> data)
        {
            foreach (Tuple<string, int> tuple in data)
            {
                if (ReducingNodes.ContainsKey(tuple.Item1.ToLower()))
                    NodeController.Send(tuple, ReducingNodes[tuple.Item1.ToLower()]);
                else
                    Console.WriteLine("Word not found");
            }
        }

        private static void MapThreading()
        {
            foreach (string sentence in Input)
            {
                SendToReducingNode(MappingNodes[Input.FindIndex(x => x == sentence)].Map(sentence));
            }
        }

        private static void Main(string[] args)
        {
            // test.txt in bin/debug/netcore folder
            using (StreamReader sr = new StreamReader("test.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Input.Add(sr.ReadLine());
                }
            }

            //Simple input example
            // Input = new List<string>() {
            //     "Dear Bear River",
            //     "Car Car River",
            //     "Dear Car Bear",
            //};

            // Populate nodes dictionaries
            for (int i = 0; i < Input.Count; i++)
            {
                MappingNodes.Add(i, new Node());
            }
            for (int i = 0; i < Input.Count; i++)
            {
                foreach (string word in Input[i].Split(' '))
                {
                    if (!ReducingNodes.ContainsKey(word.ToLower()))
                    {
                        ReducingNodes.Add(word.ToLower(), new Node());
                    }
                }
            }

            List<Node> allNodes = ReducingNodes.Values.ToList();

            // Tell node it started receiving information
            Thread startThread = new Thread(() => { foreach (Node node in allNodes) { NodeController.Send("start", node); } });

            // mapping thread, also sends data to the reducing nodes
            Thread mappingThread = new Thread(MapThreading);

            // Tell node it stopped receiving information (it should then start processing it)
            Thread endThread = new Thread(() => { foreach (Node node in allNodes) { NodeController.Send("end", node); } });

            startThread.Start();

            mappingThread.Start();
            // Wait for mapping to end then start sending "end" message
            while (mappingThread.IsAlive) ;

            endThread.Start();

            // Un-threaded implementation is faster (average 25ms vs 40 for threading with random input text 1000 lines)

            //foreach (Node node in allNodes) { NodeController.Send("start", node); }
            //MapThreading();
            ////ReduceThreading();
            //foreach (Node node in allNodes) { NodeController.Send("end", node); }
        }
    }
}