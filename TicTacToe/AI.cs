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
            for(int remainingMove = 0; remainingMove < (int)movesRemaining; remainingMove++)
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
                bool occupied = false;
                foreach(Square occupiedSquare in occupiedSquares)
                {
                    if(square == occupiedSquare)
                    {
                        occupied = true;
                        break;
                    }
                }
                if (!occupied)
                {
                    ((ArrayList)possibleProgressions[possibleProgressionsIndex]).Add(new Move(nextToMove, (MoveNumber)(existingMoves.Count + 1), square));
                    possibleProgressionsIndex++;
                }
            }

            // Check if any possible progressions wins or draws the game. If so make the move.
            foreach(ArrayList arrayOfMoves in possibleProgressions)
            {

                //TODO: Refactor - A bit silly doing this conversion // (Move[])arrayOfMoves.ToArray()
                Move[] array = new Move[arrayOfMoves.Count];
                for(int i = 0; i < array.Length; i++)
                {
                    array[i] = (Move)arrayOfMoves[i];
                }
                Progression progression = new Progression(array);
                if(progression.Outcome == Outcome.firstMoverWins && nextToMove == Mover.first)
                {
                    return ((Move)arrayOfMoves[arrayOfMoves.Count - 1]).Square;
                }
            }

           

            return CalculateNextMove(possibleProgressions, nextToMove);
        }
        protected int[] GetOutcomes(ArrayList arrayOfMoves)
        {
            // TODO: Refactor - return an object with the outcomes
            int draw = 0;
            int firstMoverWins = 0;
            int secondMoverWins = 0;
            for (int completedProgression = 0; completedProgression < this._ticTacToeProgressions.GetNumberOfProgressions(); completedProgression++)
            {
                bool match = true;
                foreach(Move move in arrayOfMoves)
                {
                    if(move.Square != this._ticTacToeProgressions.GetProgression(completedProgression).GetMove(move.MoveNumber).Square)
                    {
                        match = false;
                    }
                }
                if(match == true)
                {
                    Outcome outcome = this._ticTacToeProgressions.GetProgression(completedProgression).Outcome;
                    switch (outcome)
                    {
                        case Outcome.draw:
                        {
                            draw++;
                            break;
                        }
                        case Outcome.firstMoverWins:
                        {
                            firstMoverWins++;
                            break;
                        }
                        case Outcome.secondMoverWins:
                        {
                            secondMoverWins++;
                            break;
                        }
                    }
                }
            }

            return new int[] { draw, firstMoverWins, secondMoverWins, -1 };
        }
        protected abstract Square GetFirstMove();
        protected abstract Square CalculateNextMove(ArrayList possibleProgressions, Mover nextToMove);
    }
}
