using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    internal class DiceParser
    {
        public static List<Dice> Parse(string[] args)
        {
            if (args.Length < 3)
            {
                throw new ArgumentException("You must specify at least 3 dice configurations.");
            }

            List<Dice> diceList = new List<Dice>();

            foreach (var arg in args)
            {
                try
                {
                    int[] faces = arg.Split(',').Select(int.Parse).ToArray();
                    diceList.Add(new Dice(faces));
                }
                catch
                {
                    throw new ArgumentException($"Invalid dice format: {arg}. Example: 2,3,4,5,6,7");
                }
            }

            return diceList;
        }
    }
}
