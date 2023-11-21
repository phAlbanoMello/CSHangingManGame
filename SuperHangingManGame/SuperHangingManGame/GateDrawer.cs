using System;

public class GateDrawer
{
    public static void DrawGate(int numberOfLocks)
    {
        Console.Clear();

        Console.WriteLine("  +---+");
        Console.WriteLine("  |   |");

        for (int i = 0; i < numberOfLocks; i++)
        {
            Console.Write("  X   |");
        }

        Console.WriteLine("\n      |");
        Console.WriteLine("=========");
    }

    public static void UpdateLockStatus(int lockIndex)
    {
        Console.SetCursorPosition(4 + lockIndex * 6, Console.CursorTop - 1);
        Console.Write("O");
    }
}
