using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    class Board
    {
        public Square First { get; set; }
        public Square Last { get; set; }
        public int Count { get; set; }
        public Board()
        {
            First = new Square("0");
            var tmp = First;
            Count = 1;
            for (int i = 1; i < 40; i++)
            {
                tmp.Next = new Square(i.ToString());
                tmp = tmp.Next;
                Count++;
            }
            Last = tmp;
            Last.Next = First;
        }
        public override string ToString()
        {
            string res = "";
            var tmp = First;
            // Display all nodes + start
            for (int i = 0; i < Count + 1; i++)
            {
                res += tmp.ToString() + " -> ";
                tmp = tmp.Next;

            }
            return res + "...";
        }
    }
}
