using System;

namespace Exercise1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var q = new CustomQueue<int>();
            q.Enqueue(new Node<int>(1));
            q.Enqueue(new Node<int>(2));
            q.Enqueue(new Node<int>(3));
            q.Enqueue(new Node<int>(4));
            q.Enqueue(new Node<int>(5));
            q.Enqueue(new Node<int>(0));
            Console.WriteLine(q.Peek());
            Console.WriteLine(q.Peek());
            Console.WriteLine(q);
            foreach (int node in q)
            {
                Console.WriteLine(node);
            }
            q.Dequeue();
            q.Dequeue();
            q.Dequeue();
            Console.WriteLine(q);
            foreach (int node in q)
            {
                Console.WriteLine(node);
            }
        }
    }
}