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
            //GateDataSerializationService = new GateDataSerializationService();
            //guessValidationService = new GuessValidationService();
            jsonFilesLocator = new JSONFilesLocator();
            gateManager = new GateManager();
            gateManager.LoadGates();
            gameService = new GameService(gateManager);

            //GateDataSerializationService.Init(jsonFilesLocator);
        }
    }
}