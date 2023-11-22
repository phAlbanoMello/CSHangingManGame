using SuperHangingManGame;

class Program
{ 
    static void Main()
    {
        ServiceLocator.InitializeServices();
        Console.WriteLine("Welcome Message"); //TODO: Insert DialogueService here
        Console.WriteLine("You are trapped inside a maze. Each gate has locks that you need to unlock by guessing letters.");
        Lock[] locks = { new Lock(0, LockedState.Broken), new Lock(1), new Lock(2, LockedState.Open) };
        Gate gate = new Gate("Nature", locks, 1, 0);
        Console.WriteLine($"Current gate theme is {gate.Theme}");
        GateDrawer.DrawGate(gate);
        //string[] secretWords = ProgressionService.GetSecretWords(gate);

        //for (int i = 0; i < gate.GetLocks().Length; i++)
        //{
        //    for (int i = 0; i < gate.GetLocks()[i]; i++)
        //    {

        //    }
        //    Console.WriteLine($"Enter a letter for lock {i + 1}: ");
        //    char playerGuess = Console.ReadKey().KeyChar;
        //    Console.WriteLine();
        //}
    }
}

