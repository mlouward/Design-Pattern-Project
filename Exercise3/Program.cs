using System;
using System.Collections.Generic;

namespace Exercise3
{
    internal class Program
    {
        /// <summary>
        /// rolls two dice and checks if double -> go to jail
        /// </summary>
        /// <param name="board"></param>
        /// <param name="diceRolls"></param>
        /// <param name="player"></param>
        private static void MoveAndCheckDouble(Board board, Player player, Random diceRolls)
        {
            int roll1 = diceRolls.Next(1, 7);
            int roll2 = diceRolls.Next(1, 7);
            board.Move(player, roll1 + roll2);

            // rolled doubles
            int consecutiveDoubles = 0;
            while (roll1 == roll2)
            {
                Console.WriteLine($"{player.Name} rolled a double!");
                consecutiveDoubles++;
                if (consecutiveDoubles == 3)
                {
                    player.MoveToJail();
                }
                else
                {
                    roll1 = diceRolls.Next(1, 7);
                    roll2 = diceRolls.Next(1, 7);
                    board.Move(player, roll1 + roll2);
                    Console.WriteLine($"{player.Name}: Position: " + player.Position.Index);
                    if (player.Position.Index == Board.GOTOJAIL)
                    {
                        player.MoveToJail();
                    }

                    // Check final position of player and change player.Balance accordingly.
                    /*
                     * Area to check the balance...
                     */
                }
            }
        }

        /// <summary>
        /// Play a turn for a player following the rules.
        /// </summary>
        /// <param name="players"></param>
        /// <param name="board"></param>
        private static void PlayTurn(List<Player> players, Board board)
        {
            Random diceRolls = new Random();

            foreach (Player player in players)
            {
                int roll1;
                int roll2;

                if (player.TurnsInJail >= 0)
                {
                    // Roll 2 dices and gets out if same value
                    roll1 = diceRolls.Next(1, 7);
                    roll2 = diceRolls.Next(1, 7);
                    bool getsOut = roll1 == roll2;
                    if (!getsOut)
                        Console.WriteLine($"{player.Name} didn't roll a double ({player.TurnsInJail + 1} turn(s) in jail)...");

                    if (player.TurnsInJail == 2 || getsOut)
                    {
                        player.TurnsInJail = -1;
                        // When gets out, moves by the sum of the last dice roll
                        board.Move(player, roll1 + roll2);
                        Console.WriteLine($"{player.Name}: Position: " + player.Position.Index);
                        continue;
                    }
                    else
                    {
                        player.TurnsInJail++;
                        continue;
                    }
                }

                MoveAndCheckDouble(board, player, diceRolls);

                // Actions depending on final Square of player
                Console.WriteLine($"{player.Name}: Position: " + player.Position.Index);
                if (player.Position.Index == Board.GOTOJAIL)
                {
                    player.MoveToJail();
                }

                // determine if player is out of the game
                if (player.Balance == 0)
                {
                    players.Remove(player);
                }
            }
        }

        private static void Main(string[] args)
        {
            // Initialize the game with an empty board of size 40 + players list
            Board board = new Board(40);
            List<Player> players = new List<Player>();

            // Create 2 demo players and assign them to the board
            int nbPlayers = 2;
            for (int i = 0; i < nbPlayers; i++)
            {
                players.Add(new Player($"player{i}", board));
            }
            // Number of players who still have money. Game ends when no
            // more than 1 player has money.
            int playersInGame = nbPlayers;

            while (playersInGame > 1)
            {
                PlayTurn(players, board);
                playersInGame = players.Count;
            }
        }
    }
}