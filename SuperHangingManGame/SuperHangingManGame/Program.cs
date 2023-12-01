using SuperHangingManGame;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Services;
using SuperHangingManGame.Components;
class Program
{
    static async Task Main()
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        await serviceLocator.InitializeServices();
        await serviceLocator.GameService.StartGame();
    }
}

