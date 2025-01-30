using NonTransitiveDiceGame;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            List<Dice> diceSet = DiceParser.Parse(args);
            GameEngine game = new GameEngine(diceSet);
            game.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Usage: game.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
        }
    }
}
