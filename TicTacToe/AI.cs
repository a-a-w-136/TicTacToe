using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TicTacToe
{
    public abstract class AI
    {
        protected TicTacToeProgressions _ticTacToeProgressions;
        public AI()
        {
            this._ticTacToeProgressions = new TicTacToeProgressions();
        }
        public Square GetNextMove(GameBoard gameBoard)
        {
            ArrayList existingMoves = new ArrayList();
            foreach(MoveNumber moveNumber in Enum.GetValues(typeof(MoveNumber)))
            {
                var move = gameBoard.GetMove(moveNumber);
                if(move == null)
                {
                    break;
                }
                else
                {
                    existingMoves.Add(move);
                }
            }

            if(gameBoard.Outcome != Outcome.gameIncomplete)
            {
                throw new Exception("The game has ended.");
            }

            if (existingMoves.Count == 0)
            {
                return GetFirstMove();
            }

            Mover nextToMove;
            if(existingMoves.Count == 0)
            {
                nextToMove = Mover.first;
            }
            else
            {
                nextToMove = ((Move)existingMoves[existingMoves.Count - 1]).Mover == Mover.first ? Mover.second : Mover.first;
            }

            // Find which squares are occupied
            ArrayList occupiedSquares = new ArrayList();
            foreach(Move existingMove in existingMoves)
            {
                occupiedSquares.Add(existingMove.Square);
            }

            ArrayList possibleProgressions = new ArrayList();
            // Create duplicates of the current moves for the gameboard.
            // If one move has been made create an 2 dimensional array with the first dimension having 8 elements and each array in the second dimension will contain one move object. (8 possible next moves)
            // If two moves have been made create an 2 dimensional array with the first dimension having 7 elements and each array in the second dimension will contain two move objects (7 possible next move)
            RemainingSquares movesRemaining = (RemainingSquares)((int)MoveNumber.nine - existingMoves.Count);
            for(int remainingMove = 1; remainingMove < (int)movesRemaining; remainingMove++)
            {
                ArrayList currentProgressionDuplicate = new ArrayList();
                for(int move = 0; move < existingMoves.Count; move++)
                {
                    currentProgressionDuplicate.Add(existingMoves[move]);
                }
                possibleProgressions.Add(currentProgressionDuplicate);
            }

            // For each of the duplicates add all next possible moves.
            int possibleProgressionsIndex = 0;
            foreach(Square square in Enum.GetValues(typeof(Square)))
            {
                foreach(Square occupiedSquare in occupiedSquares)
                {
                    if(square != occupiedSquare)
                    {
                        ((ArrayList)possibleProgressions[possibleProgressionsIndex]).Add(new Move(nextToMove, (MoveNumber)(existingMoves.Count + 1), square));
                    }
                }
            }

            // Check if any possible progressions wins or draws the game. If so make the move.
            foreach(ArrayList arrayOfMoves in possibleProgressions)
            {
                Progression progression = new Progression((Move[])arrayOfMoves.ToArray());
                if(progression.Outcome == Outcome.firstMoverWins && nextToMove == Mover.first)
                {
                    return ((Move)arrayOfMoves[arrayOfMoves.Count - 1]).Square;
                }
            }

            return CalculateNextMove(possibleProgressions, nextToMove);


        }
        protected abstract Square GetFirstMove();
        protected abstract Square CalculateNextMove(ArrayList possibleProgressions, Mover nextToMove);
    }
}
