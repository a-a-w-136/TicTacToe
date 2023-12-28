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
            int bestOdds = 0;
            int bestOutcome = 0;
            for (int outcome = 0; outcome < outcomes.Count; outcome++)
            {
                int draw = ((int[])outcomes[outcome])[0];

                int total = ((int[])outcomes[outcome])[0] + ((int[])outcomes[outcome])[1] + ((int[])outcomes[outcome])[2];
                if (nextToMove == Mover.first)
                {
                    if (((((int[])outcomes[outcome])[1] + ((int[])outcomes[outcome])[0]) / total) > bestOdds)
                    {
                        bestOdds = (((int[])outcomes[outcome])[1] + ((int[])outcomes[outcome])[0]) / total;
                        bestOutcome = ((int[])outcomes[outcome])[3];
                    }
                }
                else
                {
                    if (((((int[])outcomes[outcome])[2] + ((int[])outcomes[outcome])[0]) / total) > bestOdds)
                    {
                        bestOdds = (((int[])outcomes[outcome])[2] + ((int[])outcomes[outcome])[0]) / total;
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
