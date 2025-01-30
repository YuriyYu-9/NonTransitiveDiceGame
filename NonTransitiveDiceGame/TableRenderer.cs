using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace NonTransitiveDiceGame
{
    internal class TableRenderer
    {
        public static void RenderProbabilityTable(List<Dice> diceSet)
        {
            Console.WriteLine("\nProbability of the win for the user:");

            var table = new ConsoleTable(new string[] { "User dice v" }.Concat(diceSet.Select(d => d.ToString())).ToArray());

            for (int i = 0; i < diceSet.Count; i++)
            {
                var row = new List<string> { diceSet[i].ToString() };

                for (int j = 0; j < diceSet.Count; j++)
                {
                    if (i == j)
                        row.Add("- (0.3333)");
                    else
                        row.Add(ProbabilityCalculator.CalculateWinProbability(diceSet[i], diceSet[j]).ToString("0.0000"));
                }

                table.AddRow(row.ToArray());
            }

            table.Write(Format.Alternative);
        }
    }
}
