using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
	public class ServiceLocator
    {
        private IGateDrawer gateDrawer;
        private IGuessValidatorService guessValidationService;
        private IDataSerializer<Gate[]> progressionService;
        private GateManager gateManager;

        public IGateDrawer GateDrawer { get { return gateDrawer; } private set { } }
        public IGuessValidatorService ValidatorService { get { return guessValidationService; } private set { } }
        public IDataSerializer<Gate[]> ProgressionService { get => progressionService; private set { } }
        public GateManager GateManager { get => gateManager; private set { } }

        public ServiceLocator()
        {
            InitializeServices();
        }

        public void InitializeServices()
        {
            gateManager = new GateManager();
            gateDrawer = new GateDrawer();
            guessValidationService = new GuessValidationService();

            //Should I get the json file using an directoryseach service or something like that?
            //progressionService = new ProgressionService();

            //progressionService.InitProgressionService() --> Make it get the path to the jsonFiles
            //gateManager.LoadGates(progressionService); --> Make progressionService be able to return arrays of gates
            //dialogueService.LoadMessages() --> Make it get the path to messages jsonFile
        }
	}
}