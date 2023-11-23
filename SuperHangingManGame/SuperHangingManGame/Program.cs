using SuperHangingManGame;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Services;
using SuperHangingManGame.Components;
class Program
{
    static async Task Main()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        GateManager gateManager = serviceLocator.GateManager;
        IGateDrawer gateDrawer = serviceLocator.GateDrawer;
        IDataSerializer<Gate[]> progressionService = serviceLocator.ProgressionService;
        
        string[] secretWords = { "Tree", "Cave" };
        Theme theme = new Theme(ConsoleColor.Green, "Nature", secretWords);
        Lock[] locks = { new Lock(0), new Lock(1), new Lock(2) };
        Gate gate = new Gate(theme, locks, 1, 0);

        await DialogueService.SkipLines(5);
        ConsoleService.SetFontColor(ConsoleColor.Yellow);
        await DialogueService.DisplayMessage("Welcome to Ourobouro's Gates", 25);
        await DialogueService.SkipLines(1);
        ConsoleService.SetFontColor(ConsoleColor.White);
        await DialogueService.DisplayMessage("You are trapped inside a maze.*p(500) Each gate has locks *p(100)that you need to unlock by guessing letters.", 10);
        await DialogueService.SkipLines(2);
        await DialogueService.DisplayMessage($"Current gate theme is*p(1000)", 20);
        await DialogueService.SkipLines(1);
        ConsoleService.SetFontColor(gate.Theme.Color);
        await DialogueService.DisplayMessage($"{gate.Theme.Name}", 100);
        await DialogueService.SkipLines(1);
        await DialogueService.DisplayMessage($"And it has*p(200) {gate.GetLocks().Length} Locks", 30);
        await DialogueService.SkipLines(1);
        await gateDrawer.DrawGate(gate);
        await DialogueService.SkipLines(3);
        await DialogueService.DisplayMessage("You must now state your first guess, *p(100) challenger.", 70);

        //TODO: call ComposeHorizontalStringShape instead. (or make it a draw for the drawer?)
        string guessingField = "";
        for (int i = 0; i < secretWords[0].Length; i++)
        {
            guessingField += "_";
        }
        await DialogueService.DisplayMessage(guessingField, 20);
    }
}

