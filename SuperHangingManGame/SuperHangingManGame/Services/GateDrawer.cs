using System;

namespace SuperHangingManGame
{
    public class GateDrawer
    {
        public static void DrawGate(Gate gate)
        {
            int numberOfLocks = gate.GetClosedLocks();
            // Ensure the number of locks is positive
            if (numberOfLocks <= 0)
            {
                return;
            }

            for (int i = 0; i < numberOfLocks; i++)
            {
                Console.Write("-------");
            }
            Console.WriteLine();
            for (int i = 0;i < numberOfLocks; i++)
            {
                Console.Write("|  ()  |");
            }
            Console.WriteLine();
            for (int i = 0; i < numberOfLocks; i++)
            {
                Console.Write("-------");
            }
        }

        public static void DrawGate(int numberOfLocks)
        {
            // Ensure the number of locks is positive
            if (numberOfLocks <= 0)
            {
                return;
            }

            for (int i = 0; i < numberOfLocks; i++)
            {
                Console.Write("--------");
            }
            Console.WriteLine();
            for (int i = 0; i < numberOfLocks; i++)
            {
                Console.Write("|  ()  |");
            }
            Console.WriteLine();
            for (int i = 0; i < numberOfLocks; i++)
            {
                Console.Write("--------");
            }
        }
    }
}


//Console.Write("|  ()  |");
//Console.Write("|  >>  |");
//Console.Write("|  XX  |");


// |>>|