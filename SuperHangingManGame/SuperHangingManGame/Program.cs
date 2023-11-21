using System;
using System.Collections.Generic;
using SuperHangingManGame;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome Message"); //TODO: Insert DialogueService here
        Console.WriteLine("You are trapped inside a maze. Each gate has locks that you need to unlock by guessing letters.");
        GateDrawer.DrawGate(3);
    }

    //static void PlayGame(List<Gate> gates)
    //{
    //    foreach (var gate in gates)
    //    {
    //        Console.WriteLine($"\nYou are at the {gate.Theme} gate. Guess the letters to unlock the lock.");
    //        GateDrawer.DrawGate(gate.Locks.Count);

    //        for (int i = 0; i < gate.Locks.Count; i++)
    //        {
    //            Console.Write($"Enter a letter for lock {i + 1}: ");
    //            char playerGuess = Console.ReadKey().KeyChar;
    //            Console.WriteLine();

    //            if (gate.GuessLetter(playerGuess))
    //            {
    //                Console.WriteLine("Correct letter!");
    //                GateDrawer.UpdateLockStatus(i);
    //            }
    //            else
    //            {
    //                Console.WriteLine("Incorrect letter. Try again.");
    //                // You can add logic here for handling incorrect guesses, such as reducing attempts.
    //            }
    //        }

    //        Console.WriteLine($"You have successfully unlocked the {gate.Theme} gate!");
    //    }

    //    Console.WriteLine("Congratulations! You have escaped the maze!");
    //}
}

