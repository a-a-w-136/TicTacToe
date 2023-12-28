using System;
using System.Collections;

namespace TicTacToe
{
    public class Progression
    {
        protected class SquareOwnerCombination
        {
            public Mover? Mover { get; set; }
            public Square Square { get; set; }
            public SquareOwnerCombination(Mover? mover, Square square)
            {
                Mover = mover;
                Square = square;
            }
        }

        internal Outcome Outcome { get; private set;}
        private Move[] _progression;
        private ArrayList _winningCombinations;

        internal Progression(Move[] progression)
        {
            _progression = progression;
            if (!ProgressionIsValid())
            {
                string error = "";
                foreach(Move m in _progression)
                {
                    error += "\n" + m.ToString();
                }
                // TODO: Refactor this is not an easy error to understand
                throw new Exception("Parameter(progression) cannot be null." +
                "Max 9 moves per progression."
                + "Second to move must make every even move."
                + "First to move must make every odd move"
                + "First move must be by the first to move"
                + "Move number must ascend from 1 by 1."
                + "Each move must only occupy a unique Square." + "\n" + error);
            }

            PopulateWinningCombinations();
            CalaculateOutcome();
        }
        public Move GetMove(MoveNumber moveNumber)
        {
            if ((int)moveNumber > this._progression.Length)
            {
                return null;
            }
            else
            {
                return new Move(_progression[(int)moveNumber - 1].Mover, _progression[(int)moveNumber - 1].MoveNumber, _progression[(int)moveNumber - 1].Square);
            }
        }
        public Move[] GetMoves()
        {
            Move[] moves = new Move[this._progression.Length];
            for(int i = 0; i < this._progression.Length; i++)
            {
                moves[i] = new Move(this._progression[i].Mover, this._progression[i].MoveNumber, this._progression[i].Square);
            }
            return moves;
        }
        private void CalaculateOutcome()
        {
            /*
            * Returns -1, 0, 1 or 2
            * 0: Draw
            * -1: Game incomplete
            * 1: Player 1 wins
            * 2: Player 2 wins
            */
            for (int i = 0; i < this._progression.Length; i++)
            {
                if (MarkGameBoard(this._progression[i].Square, this._progression[i].Mover) == Outcome.firstMoverWins)
                {
                    Outcome = Outcome.firstMoverWins; // Player 1 wins
                    return;
                }
                if (MarkGameBoard(this._progression[i].Square, this._progression[i].Mover) == Outcome.secondMoverWins)
                {
                    Outcome = Outcome.secondMoverWins; ; // Player 2 wins
                    return;
                }
                if (MarkGameBoard(this._progression[i].Square, this._progression[i].Mover) == Outcome.draw)
                {
                    Outcome = Outcome.draw; // Draw
                    return;
                }
            }
            Outcome = Outcome.gameIncomplete;
        }
        private Outcome MarkGameBoard(Square square, Mover mover)
        {
            for(int combinationNumber = 0; combinationNumber < _winningCombinations.Count; combinationNumber++)
            {
                ArrayList combination = (ArrayList)_winningCombinations[combinationNumber];
                for(int squareOwnerCombo = 0; squareOwnerCombo < combination.Count; squareOwnerCombo++)
                {
                    if (((SquareOwnerCombination)combination[squareOwnerCombo]).Square == square)
                    {
                        ((SquareOwnerCombination)combination[squareOwnerCombo]).Mover = mover;
                    }
                }
            }
            return CheckProgressionState();
        }
        private Outcome CheckProgressionState()
        {

            int owner_1_count = 0;
            int owner_2_count = 0;
            int ownedSquareCount = 0;
            for(int i = 0; i < this._winningCombinations.Count; i++)
            {
                for(int y = 0; y < ((ArrayList)this._winningCombinations[i]).Count; y++)
                {
                    if(((SquareOwnerCombination)((ArrayList)this._winningCombinations[i])[y]).Mover == Mover.first)
                    {
                        owner_1_count++;
                        ownedSquareCount++;
                    }
                    else if(((SquareOwnerCombination)((ArrayList)this._winningCombinations[i])[y]).Mover == Mover.second)
                    {
                        owner_2_count++;
                        ownedSquareCount++;
                    }
                }
                if(owner_1_count == 3)
                {
                    return Outcome.firstMoverWins;
                }
                if(owner_2_count == 3)
                {
                    return Outcome.secondMoverWins;
                }

                owner_1_count = 0;
                owner_2_count = 0;
            }
            if(ownedSquareCount == 24)
            {
                return Outcome.draw;
            }
            return Outcome.gameIncomplete;
        }
        private void PopulateWinningCombinations()
        {
            this._winningCombinations = new ArrayList();
            ArrayList combination_1 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.zero),
                new SquareOwnerCombination(null, Square.one),
                new SquareOwnerCombination(null, Square.two)
            };
            this._winningCombinations.Add(combination_1);

            ArrayList combination_2 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.three),
                new SquareOwnerCombination(null, Square.four),
                new SquareOwnerCombination(null, Square.five)
            };
            this._winningCombinations.Add(combination_2);

            ArrayList combination_3 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.six),
                new SquareOwnerCombination(null, Square.seven),
                new SquareOwnerCombination(null, Square.eight)
            };
            this._winningCombinations.Add(combination_3);

            ArrayList combination_4 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.zero),
                new SquareOwnerCombination(null, Square.three),
                new SquareOwnerCombination(null, Square.six)
            };
            this._winningCombinations.Add(combination_4);

            ArrayList combination_5 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.one),
                new SquareOwnerCombination(null, Square.four),
                new SquareOwnerCombination(null, Square.seven)
            };
            this._winningCombinations.Add(combination_5);

            ArrayList combination_6 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.two),
                new SquareOwnerCombination(null, Square.five),
                new SquareOwnerCombination(null, Square.eight)
            };
            this._winningCombinations.Add(combination_6);

            ArrayList combination_7 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.zero),
                new SquareOwnerCombination(null, Square.four),
                new SquareOwnerCombination(null, Square.eight)
            };
            this._winningCombinations.Add(combination_7);

            ArrayList combination_8 = new ArrayList()
            {
                new SquareOwnerCombination(null, Square.two),
                new SquareOwnerCombination(null, Square.four),
                new SquareOwnerCombination(null, Square.six)
            };
            this._winningCombinations.Add(combination_8);

        }
     
        private void SortProgression()
        {
            for(int i = 0; i < _progression.Length; i++)
            {
                for(int j = i; j < _progression.Length; j++)
                {
                    if(_progression[j].MoveNumber < _progression[i].MoveNumber)
                    {
                        Move temp = _progression[i];
                        _progression[i] = _progression[j];
                        _progression[j] = temp;
                    }
                }
            }
        }
        private bool ProgressionIsValid()
        {
            if (_progression == null)
            {
                return false;
            }

            SortProgression();

            // Verfiy player order
            if (this._progression.Length > 9)
            {
                return false;
            }
            for (int move = 0; move < this._progression.Length; move++)
            {
                if (((move % 2) == 1) && (int)(this._progression[move]).Mover != 2)
                {
                    return false;
                }
                else if (((move % 2) == 0) && (int)(this._progression[move]).Mover != 1)
                {
                    return false;
                }
            }

            // Verfiy moves order
            for (int move = 0; move < this._progression.Length; move++)
            {
                if (move == 0)
                {
                    if ((int)(this._progression[move]).MoveNumber != 1)
                    {
                        return false;
                    }
                }
                else
                {
                    if ((this._progression[move].MoveNumber - this._progression[move - 1].MoveNumber) != 1)
                    {
                        return false;
                    }
                }
            }

            // Verify occupied squares
            for (int move = 0; move < this._progression.Length; move++)
            {
                for (int move_dup = 0; move_dup < this._progression.Length; move_dup++)
                {
                    if (move != move_dup && this._progression[move].Square == this._progression[move_dup].Square)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
