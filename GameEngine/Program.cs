using System;
using TicTacToe;

namespace GameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            // Shows typical usage of the TicTacToe assembly.
            // Result is the same as usage of https://github.com/a-a-w-136/a-a-w-136.github.io/blob/master/js/TikTakToe.js

            GameBoard gameBoard = new GameBoard();
            AI ai = new AICumulativeDrawAndWin();

            // Make first move
            gameBoard.MakeMove(Square.eight);
            Console.WriteLine($"After move one the outcome is: {gameBoard.Outcome}");

            // Make second move
            gameBoard.MakeMove(ai.GetNextMove(gameBoard));
            Console.WriteLine($"After move two the outcome is: {gameBoard.Outcome}");

            // Make third move
            gameBoard.MakeMove(Square.zero);
            Console.WriteLine($"After move three the outcome is: {gameBoard.Outcome}");

            // Make fourth move
            gameBoard.MakeMove(ai.GetNextMove(gameBoard));
            Console.WriteLine($"After move four the outcome is: {gameBoard.Outcome}");

            // Make fifth move
            gameBoard.MakeMove(Square.six);
            Console.WriteLine($"After move five the outcome is: {gameBoard.Outcome}");

            // Make six move
            gameBoard.MakeMove(ai.GetNextMove(gameBoard));
            Console.WriteLine($"After move six the outcome is: {gameBoard.Outcome}");

            // Make seventh move
            gameBoard.MakeMove(Square.seven);
            Console.WriteLine($"After move seven the outcome is: {gameBoard.Outcome}");

            Console.ReadLine();
        }
    }
}
