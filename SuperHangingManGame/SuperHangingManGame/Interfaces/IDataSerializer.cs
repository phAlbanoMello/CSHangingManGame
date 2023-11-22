namespace SuperHangingManGame.Interfaces
{
    public interface IDataSerializer<T>
    {
        void Serialize(string filePath, T data);
        T Deserialize(string filePath);
    }
}
