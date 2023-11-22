using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace SuperHangingManGame
{
    public class ProgressionService
    {
        private List<LevelData> levels;

        public void InitProgressionService(string jsonFilePath)
        {
            LoadProgressionData(jsonFilePath);
        }

        private List<LevelData> LoadProgressionData(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                levels = JsonConvert.DeserializeObject<List<LevelData>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading progression data: {ex.Message}");
                levels = new List<LevelData>();
            }
            return null;
        }

        public LevelData GetLevelData(int level)
        {
            if (level >= 1 && level <= levels.Count)
            {
                return levels[level - 1];
            }

            Console.WriteLine("Invalid level. Returning default data.");
            return new LevelData();
        }
    }

    public class LevelData
    {
        public int NumberOfLocks { get; set; }
        public int WordDifficulty { get; set; }
    }
}