using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    internal class Player
    {
        public string Name { get; set; }

        public Square Position { get; set; }

        // Money available to a player (to determine when the game ends)
        public int Balance { get; set; } = 200;

        /// -1 if not in jail, 0 when in jail for the first time
        public int TurnsInJail { get; set; }

        public Player(string name, Board assignedBoard)
        {
            Name = name;
            Position = assignedBoard.First;
            TurnsInJail = -1;
        }

        /// <summary>
        /// Sets player position to jail + TurnsInJail value to 0
        /// </summary>
        public void MoveToJail()
        {
            Console.WriteLine($"{this.Name} goes to Jail.");
            this.Position = Board.Jail;
            this.TurnsInJail = 0;
        }
    }
}