using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    internal class ProbabilityCalculator
    {
        public static double CalculateWinProbability(Dice diceA, Dice diceB)
        {
            int wins = diceA.GetFaces().SelectMany(_ => diceB.GetFaces(), (a, b) => a > b).Count(x => x);
            int total = diceA.GetFaces().Length * diceB.GetFaces().Length;
            return (double)wins / total;
        }
    }
}
