using System;
using System.Collections;

namespace TicTacToe
{
    public class AICumulativeDrawAndWin : AI
    {
        protected override Square CalculateNextMove(ArrayList possibleProgressions, Mover nextToMove)
        {
            ArrayList outcomes = new ArrayList();

            // Get all the outcomes of the completed progressions that match the possible progressions.
            for(int arrayOfMoves = 0; arrayOfMoves < possibleProgressions.Count; arrayOfMoves++)
            {
                int[] outcome = GetOutcomes((ArrayList)possibleProgressions[arrayOfMoves]);
                outcome[3] = arrayOfMoves;
                outcomes.Add(outcome);
            }

            // Determine which move will yield the best odd for a win or draw scenario.
            // TODO: This is outrageously UGLY - Refactor
            decimal bestOdds = 0;
            int bestOutcome = 0;
            for (int outcome = 0; outcome < outcomes.Count; outcome++)
            {
                int total = ((int[])outcomes[outcome])[0] + ((int[])outcomes[outcome])[1] + ((int[])outcomes[outcome])[2];
                if (nextToMove == Mover.first)
                {
                    if ((((decimal)((int[])outcomes[outcome])[1] + (decimal)((int[])outcomes[outcome])[0]) / (decimal)total) > bestOdds)
                    {
                        bestOdds = ((decimal)((int[])outcomes[outcome])[1] + (decimal)((int[])outcomes[outcome])[0]) / (decimal)total;
                        bestOutcome = ((int[])outcomes[outcome])[3];
                    }
                }
                else
                {
                 
                    if ((((decimal)((int[])outcomes[outcome])[2] + (decimal)((int[])outcomes[outcome])[0]) / (decimal)total) > bestOdds)
                    {
                        bestOdds = ((decimal)((int[])outcomes[outcome])[2] + (decimal)((int[])outcomes[outcome])[0]) / (decimal)total;
                        bestOutcome = ((int[])outcomes[outcome])[3];
                        
                    }
                }
            }
            return ((Move)((ArrayList)possibleProgressions[bestOutcome])[((ArrayList)possibleProgressions[bestOutcome]).Count - 1]).Square;
        }

        protected override Square GetFirstMove()
        {
            Random nextSquare = new Random();
            return (Square)nextSquare.Next((int)Square.zero, (int)Square.eight);
        }
    }
}
