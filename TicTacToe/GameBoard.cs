using System;
using System.Collections;

namespace TicTacToe
{

    public class GameBoard
    {
        private ArrayList _moves;
        public Outcome Outcome { get; private set; }
        public GameBoard()
        {
            this._moves = new ArrayList();
            this.Outcome = Outcome.gameIncomplete;
        }
        public Outcome MakeMove(Square square)
        {
            if (this.Outcome == Outcome.firstMoverWins || this.Outcome == Outcome.secondMoverWins || this.Outcome == Outcome.draw)
            {
                return this.Outcome; // Game over
            }
            if (this._moves.Count == 9)
            {
                return Outcome; // Game over.
            }
            if (this._moves.Count == 0)
            {
                this._moves.Add(new Move(Mover.first, MoveNumber.one, square));
                return this.Outcome;
            }
            for (int i = 0; i < this._moves.Count; i++)
            {
                if (square == ((Move)this._moves[i]).Square)
                {
                    throw new Exception($"Square {square} is currently occupied. Cannot make this move.");
                }
            }

            if(((Move)this._moves[this._moves.Count - 1]).Mover == Mover.first)
            {
                this._moves.Add(new Move(Mover.second, ((Move)this._moves[this._moves.Count - 1]).MoveNumber + 1, square));
            }
            else
            {
                this._moves.Add(new Move(Mover.first, ((Move)this._moves[this._moves.Count - 1]).MoveNumber + 1, square));
            }

            // I don't like this conversion process
            Move[] movesArray = new Move[this._moves.Count];
            for(int i = 0; i < movesArray.Length; i++)
            {
                movesArray[i] = (Move)this._moves[i];
            }
            Progression progression = new Progression(movesArray);


            this.Outcome = progression.Outcome;
            return this.Outcome;
        }
        public Move GetMove(MoveNumber moveNumber)
        {
            if ((int)moveNumber > _moves.Count)
            {
                return null;
            }
            else
            {
                return (Move)this._moves[(int)moveNumber - 1];
            }
        }
    }
}
