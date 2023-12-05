using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using SuperHangingManGame.Interfaces;
using SuperHangingManGame.Components;
using SuperHangingManGame.Data.Configs;

namespace SuperHangingManGame.Services
{
    public class MessagesSerializationService : IDataSerializer<MessagesConfig>
    {
        private readonly MessagesConfig? messagesConfig;

        public MessagesConfig? MessagesConfig => messagesConfig;

        public MessagesSerializationService(string jsonFilePath)
        {
            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);
                messagesConfig = JsonConvert.DeserializeObject <MessagesConfig>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading message data: {ex.Message}");
                messagesConfig = new MessagesConfig();
            }
        }

        public void Serialize(string filePath, MessagesConfig data)
        {
            throw new NotImplementedException();
        }

        MessagesConfig IDataSerializer<MessagesConfig>.Deserialize(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Init(IJsonFilesLocator jSONFilesLocator)
        {
            throw new NotImplementedException();
        }

        public T[] GetData<T>()
        {
            throw new NotImplementedException();
        }
    }
}