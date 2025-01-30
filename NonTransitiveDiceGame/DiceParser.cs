using NonTransitiveDiceGame;
using System;
using System.Collections.Generic;
using System.Linq;

public static class DiceParser
{
    public static List<Dice> Parse(string[] args)
    {
        if (args.Length < 3)
            throw new ArgumentException("Please specify at least three dice.");

        List<Dice> diceSet = new List<Dice>();

        foreach (var arg in args)
        {
            int[] faces = arg.Split(',')
                             .Select(f => int.TryParse(f, out int num) ? num : throw new ArgumentException($"Invalid value '{f}'. Only integers are allowed."))
                             .ToArray();

            if (faces.Length != 6)
                throw new ArgumentException($"Invalid dice configuration: {arg}. Each dice must have exactly 6 faces.");

            diceSet.Add(new Dice(faces));
        }

        return diceSet;
    }
}
