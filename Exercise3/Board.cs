using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    internal class Board
    {
        public Square First { get; set; }
        public Square Last { get; set; }
        public static int GOTOJAIL { get; } = 30;
        public static Square Jail { get; set; }

        // Size of the board
        public int Size { get; set; }

        public Board(int size)
        {
            Size = size;
            First = new Square(0);
            var tmp = First;
            Size = 1;
            for (int i = 1; i < 40; i++)
            {
                tmp.Next = new Square(i);
                tmp = tmp.Next;
                if (i == 10)
                    Jail = tmp;
                Size++;
            }
            Last = tmp;
            Last.Next = First;
        }

        public override string ToString()
        {
            string res = "";
            var tmp = First;
            // Display all nodes + start
            for (int i = 0; i < Size + 1; i++)
            {
                res += tmp.ToString() + " -> ";
                tmp = tmp.Next;
            }
            return res + "...";
        }

        public void Move(Player player, int squaresToMove)
        {
            Square tmp = player.Position;
            for (int i = 0; i < squaresToMove; i++)
            {
                tmp = tmp.Next;
            }
            player.Position = tmp;
        }
    }
}