
using static SuperHangingManGame.Services.DialogueService;

namespace SuperHangingManGame.Interfaces
{
    public interface IDialogueService
    {
        static async void DisplayMessage(string message, Pause? pause) { }
    }
}
