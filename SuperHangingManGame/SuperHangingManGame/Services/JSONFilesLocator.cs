using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
    public class JSONFilesLocator : IJsonFilesLocator
    {
        private readonly string solutionFolderPath;

        public JSONFilesLocator()
        {
            this.solutionFolderPath = GetSolutionFolderPath();
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

        public string GetJsonFilePath(string fileName)
        {
            try
            {
                var jsonFiles = FindJsonFiles();
                var filePath = jsonFiles.FirstOrDefault(file => Path.GetFileName(file) == fileName);

                if (filePath != null)
                    return filePath;
                else
                {
                    Console.WriteLine($"JSON file '{fileName}' not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting JSON file path: {ex.Message}");
                return null;
            }
        }
        private static string GetSolutionFolderPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}

