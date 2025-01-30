using NonTransitiveDiceGame;
using System;
using System.Collections.Generic;

public class GameEngine
{
    private readonly List<Dice> diceSet;
    private Dice userDice;
    private Dice computerDice;

    public GameEngine(List<Dice> diceSet)
    {
        this.diceSet = diceSet;
    }

    public void Start()
    {
        Console.WriteLine("Let's determine who makes the first move.");
        var (compChoice, hmac, key) = FairRandomGenerator.GenerateFairNumber(2);

        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac})");
        Console.WriteLine("Try to guess my selection.");
        Console.WriteLine("0 - 0\n1 - 1\nX - exit\n? - help");

        string userChoice;
        while (true)
        {
            userChoice = Console.ReadLine();
            if (userChoice == "?")
            {
                TableRenderer.RenderProbabilityTable(diceSet);
                continue;
            }
            if (userChoice == "0" || userChoice == "1") break;
            if (userChoice.ToUpper() == "X") return;
            Console.WriteLine("Invalid input. Try again.");
        }

        Console.WriteLine($"My selection: {compChoice} (KEY={BitConverter.ToString(key).Replace("-", "")})");

        bool userGoesFirst = userChoice == compChoice.ToString();
        Console.WriteLine(userGoesFirst ? "You make the first move." : "I make the first move.");
        
        SelectDice(userGoesFirst);
        PlayGame();
    }

    private void SelectDice(bool userFirst)
    {
        if (userFirst)
        {
            userDice = GetUserDice();
            computerDice = GetComputerDice(userDice);
        }
        else
        {
            computerDice = GetComputerDice(null);
            userDice = GetUserDice();
        }

        Console.WriteLine($"User selected: {userDice}");
        Console.WriteLine($"Computer selected: {computerDice}");
    }

    private Dice GetUserDice()
    {
        Console.WriteLine("Choose your dice:");
        for (int i = 0; i < diceSet.Count; i++)
            Console.WriteLine($"{i} - {diceSet[i]}");

        int selection;
        while (!int.TryParse(Console.ReadLine(), out selection) || selection < 0 || selection >= diceSet.Count)
        {
            Console.WriteLine("Invalid selection, try again.");
        }

        return diceSet[selection];
    }

    private Dice GetComputerDice(Dice excludeDice)
    {
        var availableDice = diceSet.FindAll(d => d != excludeDice);
        return availableDice[new Random().Next(availableDice.Count)];
    }

    private void PlayGame()
    {
        Console.WriteLine("\nIt's time for my throw.");
        var (compThrow, hmac, key) = FairRandomGenerator.GenerateFairNumber(6);
        Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={hmac}).");

        Console.WriteLine("Add your number modulo 6.");
        Console.WriteLine("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - exit\n? - help");

        int userNumber;
        while (true)
        {
            string input = Console.ReadLine();
            if (input == "?")
            {
                TableRenderer.RenderProbabilityTable(diceSet);
                continue;
            }
            if (input.ToUpper() == "X") return;
            if (int.TryParse(input, out userNumber) && userNumber >= 0 && userNumber <= 5) break;

            Console.WriteLine("Invalid input. Try again.");
        }

        Console.WriteLine($"My number is {compThrow} (KEY={BitConverter.ToString(key).Replace("-", "")}).");
        int resultIndex = (compThrow + userNumber) % 6;
        Console.WriteLine($"The result is {compThrow} + {userNumber} = {resultIndex} (mod 6).");

        int compRoll = computerDice.GetFaces()[resultIndex];
        Console.WriteLine($"My throw is {compRoll}.");

        Console.WriteLine("\nIt's time for your throw.");
        var (userThrow, userHmac, userKey) = FairRandomGenerator.GenerateFairNumber(6);
        Console.WriteLine($"I selected a random value in the range 0..5 (HMAC={userHmac}).");

        Console.WriteLine("Add your number modulo 6.");
        Console.WriteLine("0 - 0\n1 - 1\n2 - 2\n3 - 3\n4 - 4\n5 - 5\nX - exit\n? - help");

        int compNumber;
        while (true)
        {
            string input = Console.ReadLine();
            if (input == "?")
            {
                TableRenderer.RenderProbabilityTable(diceSet);
                continue;
            }
            if (input.ToUpper() == "X") return;
            if (int.TryParse(input, out compNumber) && compNumber >= 0 && compNumber <= 5) break;

            Console.WriteLine("Invalid input. Try again.");
        }

        Console.WriteLine($"My number is {userThrow} (KEY={BitConverter.ToString(userKey).Replace("-", "")}).");
        int userResultIndex = (userThrow + compNumber) % 6;
        Console.WriteLine($"The result is {userThrow} + {compNumber} = {userResultIndex} (mod 6).");

        int userRoll = userDice.GetFaces()[userResultIndex];
        Console.WriteLine($"Your throw is {userRoll}.");

        if (userRoll > compRoll)
            Console.WriteLine("You win!");
        else if (userRoll < compRoll)
            Console.WriteLine("Computer wins!");
        else
            Console.WriteLine("It's a tie!");
    }

}
