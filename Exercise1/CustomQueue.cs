using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.ExceptionServices;
using System.Text;

namespace Exercise1
{
    public class CustomQueue<T> : IEnumerable<T>
    {
        public int Length { get; set; }
        public Node<T> Start { get; set; }

        public CustomQueue()
        {
            Start = null;
            Length = 0;
        }

        public CustomQueue(Node<T> start)
        {
            Start = start;
            Length = 1;
        }

        public void Enqueue(Node<T> node)
        {
            if (Start is null)
                Start = node;
            else
            {
                node.Next = Start;
                Start = node;
            }
            Length++;
        }

        public Node<T> Dequeue()
        {
            Node<T> res;
            Node<T> current = Start;
            if (current is null)
                return null;
            else if (current.Next is null)
            {
                res = Start;
                Start = null;
            }
            else if (current.Next.Next is null)
            {
                res = current.Next;
                current.Next = null;
            }
            else
            {
                while (current.Next.Next != null)
                {
                    current = current.Next;
                }
                res = current.Next;
                current.Next = null;
            }
            Length--;
            return res;
        }

        public Node<T> Peek()
        {
            Node<T> tmp = Start;
            while (tmp.Next != null)
            {
                tmp = tmp.Next;
            }
            return tmp;
        }

        public override string ToString()
        {
            var s = "";
            var current = Start;
            if (current is null)
            {
                return s;
            }
            while (current != null)
            {
                s += current.ToString();
                current = current.Next;
            }
            return s + "x";
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = Start;
            while (current != null)
            {
                yield return current.Content;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}