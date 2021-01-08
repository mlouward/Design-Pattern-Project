using System;
using System.Collections.Generic;

namespace Exercise2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Node mappingNode = new Node();

            Node bearReducingNode = new Node();
            Node dearReducingNode = new Node();
            Node carReducingNode = new Node();
            Node riverReducingNode = new Node();

            List<Node> allNodes = new List<Node>
            {
                bearReducingNode,
                dearReducingNode,
                carReducingNode,
                riverReducingNode,
            };

            string[] input = new string[]{
                "Dear Bear River",
                "Car Car River",
                "Dear Car Bear",
            };

            // Tell node it started receiving information
            foreach (Node node in allNodes)
            {
                NodeController.Send("start", node);
            }

            // Send info
            foreach (string textEntry in input)
            {
                List<Tuple<string, int>> mapOutput = mappingNode.Map(textEntry);
                foreach (Tuple<string, int> tuple in mapOutput)
                {
                    switch (tuple.Item1.ToLower())
                    {
                        case "bear":
                            NodeController.Send(tuple, bearReducingNode);
                            break;

                        case "dear":
                            NodeController.Send(tuple, dearReducingNode);
                            break;

                        case "car":
                            NodeController.Send(tuple, carReducingNode);
                            break;

                        case "river":
                            NodeController.Send(tuple, riverReducingNode);
                            break;

                        default:
                            Console.WriteLine($"Could not find a node to process this word: {tuple.Item1}");
                            break;
                    }
                }
            }

            // Tell node it stopped receiving information
            foreach (Node node in allNodes)
            {
                NodeController.Send("end", node);
            }
        }
    }
}