using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
    internal class JSONFilesLocator : IJsonFilesLocator
    {
        private readonly string solutionFolderPath;

        public JSONFilesLocator(string solutionFolderPath)
        {
            this.solutionFolderPath = solutionFolderPath;
        }

        public IEnumerable<string> FindJsonFiles()
        {
            try
            {
                var jsonFiles = Directory.GetFiles(solutionFolderPath, "*.json", SearchOption.AllDirectories);
                return jsonFiles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding JSON files: {ex.Message}");
                return Enumerable.Empty<string>();
            }
        }
    }
}
