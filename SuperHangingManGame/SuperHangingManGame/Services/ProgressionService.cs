using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using SuperHangingManGame.Interfaces;

namespace SuperHangingManGame.Services
{
    public class ProgressionService : IDataSerializer<Gate[]>
    {
        private readonly Gate[]? gates;

        public ProgressionService(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                gates = JsonConvert.DeserializeObject <Gate[]>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading progression data: {ex.Message}");
                gates = new Gate[0];
            }
        }

        public Gate GetGateByIndex(int index)
        {
            if (gates == null || gates.Length < 1 ) {
                Console.WriteLine("Error : Gate array was not loaded");    
            }
            if (index >= 0 && index < gates.Length)
            {
                return gates[index];
            }

            Console.WriteLine("Invalid index. Returning default data.");
            return new Gate();
        }

        public void Serialize(string filePath, Gate[] data)
        {
            throw new NotImplementedException();
        }

        public Gate[] Deserialize(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}