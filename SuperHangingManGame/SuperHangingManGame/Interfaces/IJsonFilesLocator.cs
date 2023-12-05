namespace SuperHangingManGame.Interfaces
{
    public interface IJsonFilesLocator
    {
        public IEnumerable<string> FindJsonFiles();
        public string GetJsonFilePath(string fileName);
    }
}
