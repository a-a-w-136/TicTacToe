using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

namespace TicTacToe
{
    public class TicTacToeProgressions
    {
        private ArrayList _progressions;
        public TicTacToeProgressions()
        {
            _progressions = new ArrayList();
            CreateProgressions();
        }
        public Progression GetProgression(int index)
        {
            if(index >= this._progressions.Count || index < 0)
            {
                return null;
            }
            else
            {
                var x = this._progressions[index];
                return (Progression)(this._progressions[index]);
            }
        }
        public int GetNumberOfProgressions()
        {
            return this._progressions.Count;
        }
        private void CreateProgressions()
        {

            CalculateMoves(RemainingSquares.nine, Mover.first, MoveNumber.one);
            CalculateMoves(RemainingSquares.eight, Mover.second, MoveNumber.two);
            CalculateMoves(RemainingSquares.seven, Mover.first, MoveNumber.three);
            CalculateMoves(RemainingSquares.six, Mover.second, MoveNumber.four);
            CalculateMoves(RemainingSquares.five, Mover.first, MoveNumber.five);
            CalculateMoves(RemainingSquares.four, Mover.second, MoveNumber.six);
            CalculateMoves(RemainingSquares.three, Mover.first, MoveNumber.seven);
            CalculateMoves(RemainingSquares.two, Mover.second, MoveNumber.eight);
            CalculateMoves(RemainingSquares.one, Mover.first, MoveNumber.nine);

            ArrayList progressions = new ArrayList();
            foreach(ArrayList progression in _progressions)
            {
                Move[] moves = new Move[progression.Count];
                for(int i = 0; i < moves.Length; i++)
                {
                    moves[i] = (Move)progression[i];
                }
                progressions.Add(new Progression(moves));
            }
            _progressions = progressions;
            
        }
        private void CalculateMoves(RemainingSquares remainingSquares, Mover mover, MoveNumber moveNumber)
        {
            if(remainingSquares == RemainingSquares.nine)
            {
                var squares = Enum.GetValues(typeof(Square));
                foreach(Square square in squares)
                {
                    Move firstMove = new Move(Mover.first, MoveNumber.one, square);
                    _progressions.Add(new ArrayList() { firstMove });
                }
                return;
            }
            

            // Duplicates each progression in the progressions remainingSquares times. E.g. If one move has been made, duplicate each 8 times as each one will have 8 different next possible moves.
            int completedMoves = (int)MoveNumber.nine - (int)remainingSquares;
            ArrayList progressionsWithDuplicates = new ArrayList();
            for(int progression = 0; progression < _progressions.Count; progression++)
            {
                for(int duplicateNumber = 0; duplicateNumber < (int)remainingSquares; duplicateNumber++)
                {
                    ArrayList duplicate = new ArrayList();
                    for(int move = 0; move < completedMoves; move++)
                    {
                        duplicate.Add(((ArrayList)_progressions[progression])[move]);
                    }
                    progressionsWithDuplicates.Add(duplicate);
                }
            }

            for (int start = 0, end = (int)remainingSquares; end <= progressionsWithDuplicates.Count; start += (int)remainingSquares, end += (int)remainingSquares)
            {
                SetMoves(start, end, progressionsWithDuplicates, mover, moveNumber);
            }
        }
        private void SetMoves(int start, int end, ArrayList progressionsWithDuplicates, Mover mover, MoveNumber moveNumber)
        {
            for (int i = start; i < end;)
            {
                var squares = Enum.GetValues(typeof(Square));
                foreach(Square square in squares)
                {
                    ArrayList occupiedSquares = new ArrayList();
                    for (int j = 0; j < ((ArrayList)progressionsWithDuplicates[i]).Count; j++)
                    {
                        if (!occupiedSquares.Contains(((Move)((ArrayList)progressionsWithDuplicates[i])[j]).Square))
                        {
                            occupiedSquares.Add(((Move)((ArrayList)progressionsWithDuplicates[i])[j]).Square);
                        }
                    }
                    bool squareOccupied = false;

                    if (occupiedSquares.Contains(square))
                    {
                        squareOccupied = true;
                    }
                    if (!squareOccupied)
                    {
                        Move move = new Move(mover, moveNumber, square);
                        ((ArrayList)progressionsWithDuplicates[i]).Add(move);
                        i++;
                        if (i == end) // Not really understanding this??
                        {
                            _progressions = progressionsWithDuplicates;
                            return;
                        }
                        if (i == progressionsWithDuplicates.Count)
                        {
                            _progressions = progressionsWithDuplicates;
                            return; // Hacky. The function that calls this function inputs 'end' as one over the incompleteMovesCollection at some point. Rethink how the parameters are supplied to the call.
                        }
                    }
                }
            }
            _progressions = progressionsWithDuplicates;
        }
    }
}
