using SuperHangingManGame;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Services;

class Program
{
    static async Task Main()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        GateManager gateManager = serviceLocator.GateManager;
        IGateDrawer gateDrawer = serviceLocator.GateDrawer;
        IDataSerializer<Gate[]> progressionService = serviceLocator.ProgressionService;

        await DialogueService.DisplayMessage("Welcome Message", null); //TODO: Insert DialogueService here
        await DialogueService.DisplayMessage("You are trapped inside a maze. Each gate has locks that you need to unlock by guessing letters.", new DialogueService.Pause("maze.", 250));
        
        Lock[] locks = { new Lock(0, LockedState.Broken), new Lock(1), new Lock(2, LockedState.Open) };
        Gate gate = new Gate("Nature", locks, 1, 0);
        Console.WriteLine($"Current gate theme is {gate.Theme}");
        gateDrawer.DrawGate(gate);


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

