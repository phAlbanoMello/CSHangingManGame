namespace SuperHangingManGame
{
	public class ServiceLocator
	{
        private GateDrawer gateDrawer;
        private GuessValidationService guessValidationService;
        private DialogueService dialogueService;
        private GateManager gateManager;
        private ProgressionService progressionService;

        public static void InitializeServices()
        {
            //gateManager = new GateManager();
            //gateDrawer = new GateDrawer();
            //guessValidationService = new GuessValidationService();
            //dialogueService = new DialogueService();
            //progressionService = new ProgressionService();

            //progressionService.InitProgressionService() --> Make it get the path to the jsonFiles
            //gateManager.LoadGates(progressionService); --> Make progressionService be able to return arrays of gates
            //dialogueService.LoadMessages() --> Make it get the path to messages jsonFile
        }
	}
}