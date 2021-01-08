using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;

namespace Exercise2
{
    internal class Program
    {
        // one mapping node per input sentence, stored in dictionary where key is index of input sentence

        private static Dictionary<int, Node> MappingNodes { get; set; } = new Dictionary<int, Node>();

        //one reducing node per unique word in input, stored in dictionary where key is unique word

        private static Dictionary<string, Node> ReducingNodes { get; set; } = new Dictionary<string, Node>();

        private static List<List<Tuple<string, int>>> MapOutput { get; set; } = new List<List<Tuple<string, int>>>();

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
                MapOutput.Add(MappingNodes[Input.FindIndex(x => x == sentence)].Map(sentence));
            }
        }

        private static void ReduceThreading()
        {
            foreach (var item in MapOutput)
            {
                SendToReducingNode(item);
            }
        }

        private static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader("test.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Input.Add(sr.ReadLine());
                }
            }

            //Input = new List<string>() {
            //    "Dear Bear River",
            //    "Car Car River",
            //    "Dear Car Bear",
            //};

            // Populate nodes dictionaries
            for (int i = 0; i < Input.Count; i++)
            {
                MappingNodes.Add(i, new Node());
            }
            for (int i = 0; i < Input.Count; i++)
            {
                foreach (string word in Input[i].Split(" "))
                {
                    ReducingNodes.TryAdd(word.ToLower(), new Node());
                }
            }

            List<Node> allNodes = ReducingNodes.Values.ToList();

            // Tell node it started receiving information
            Thread startThread = new Thread(() => { foreach (Node node in allNodes) { NodeController.Send("start", node); } });

            // mapping and reducing threads
            Thread mappingThread = new Thread(MapThreading);
            Thread reduceThread = new Thread(ReduceThreading);

            // Tell node it stopped receiving information
            Thread endThread = new Thread(() => { foreach (Node node in allNodes) { NodeController.Send("end", node); } });

            Stopwatch s = new Stopwatch();
            s.Start();

            startThread.Start();

            mappingThread.Start();
            // Wait for mapping to end then start reduce
            while (mappingThread.IsAlive) ;

            reduceThread.Start();
            // Wait to send everything before sending "end" to the nodes
            while (reduceThread.IsAlive) ;

            endThread.Start();
            while (endThread.IsAlive) ;

            //foreach (Node node in allNodes) { NodeController.Send("start", node); }
            //MapThreading();
            //ReduceThreading();
            //foreach (Node node in allNodes) { NodeController.Send("end", node); }

            s.Stop();

            Console.WriteLine(s.ElapsedMilliseconds + "ms");
        }
    }
}