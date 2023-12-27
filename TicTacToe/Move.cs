using System;
namespace TicTacToe
{
    public class Move
    {
        public Mover Mover { get; private set; }
        public MoveNumber MoveNumber { get; private set; }
        public Square Square { get; private set; }
        public Move(Mover mover, MoveNumber moveNumber, Square square)
        {
            Mover = mover;
            MoveNumber = moveNumber;
            Square = square;
        }
        public override string ToString()
        {
            return $"Mover: {Mover} - MoverNumber: {MoveNumber} - Square: {Square}";
        }
    }
}
