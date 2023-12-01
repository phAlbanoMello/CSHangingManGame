using SuperHangingManGame.Components;
using SuperHangingManGame.Services;

namespace SuperHangingManGame.Interfaces
{
    public interface ITextManager
    {
        static event EventHandler<TextEventArgs> TextWritten;
        void AddText(Text text) { }
        Text GetNextText() { return null; }
    }
}
