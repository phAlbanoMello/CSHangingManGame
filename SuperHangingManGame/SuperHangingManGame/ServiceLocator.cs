using SuperHangingManGame.Components;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Services.Display;
using System.Diagnostics;

namespace SuperHangingManGame.Services
{
    public class ServiceLocator
    {
        private IGateManager? gateManager;
        private IGameService? gameService;
    
        private IGuessValidatorService? guessValidationService;
        private IDataSerializer<Gate[]>? gateDataSerializationService;
        private IJsonFilesLocator? jsonFilesLocator;

        public IGuessValidatorService ValidatorService { get { return guessValidationService; } private set { } }
        public IDataSerializer<Gate[]> GateDataSerializationService { get => gateDataSerializationService; private set { } }
        public IGateManager GateManager { get => gateManager; private set { } }
        public IGameService GameService { get { return gameService; } private set { } }
     
        public async Task InitializeServices()
        {
            jsonFilesLocator = new JSONFilesLocator();
            //GateDataSerializationService = new GateDataSerializationService();
            gateManager = new GateManager();
            gateManager.LoadGates();
            guessValidationService = new GuessValidationService();
            gameService = new GameService(gateManager);

           // GateDataSerializationService.Init(jsonFilesLocator);

            //GateDataSerializationService.InitProgressionService() --> Make it get the path to the jsonFiles
            //gateManager.LoadGates(progressionService); --> Make progressionService be able to return arrays of gates
            //dialogueService.LoadMessages() --> Make it get the path to messages jsonFile

        }
    }
}