using SuperHangingManGame.Services;

class Program
{
    static async Task Main()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        await serviceLocator.InitializeServices();
        await serviceLocator.GameService.StartGame();
    }
}

