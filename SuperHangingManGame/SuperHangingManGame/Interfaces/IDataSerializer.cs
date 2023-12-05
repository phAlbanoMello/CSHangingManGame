using SuperHangingManGame.Components;
using SuperHangingManGame.Services;

namespace SuperHangingManGame.Interfaces
{
    public interface IDataSerializer<T>
    {
        void Serialize(string filePath, T data);
        T Deserialize(string filePath);
        public void Init(IJsonFilesLocator jSONFilesLocator);
    }
}
