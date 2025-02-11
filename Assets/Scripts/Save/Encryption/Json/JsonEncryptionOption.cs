using Saves.Serializiation;
using System.IO;
using UnityEngine;

namespace Saves
{
    public class JsonEncryptionOption : IEncryptionOption
    {
        private string savePath;

        public JsonEncryptionOption(string savePath)
        {
            this.savePath = savePath;
        }

        public void Delete()
        {
            if (!FileExist()) return;

            File.Delete(savePath);
        }

        public bool FileExist()
        {
            return File.Exists(savePath);
        }

        public SerializableDictionary<string, SerializableDictionary<string, string>> Load()
        {
            if (!FileExist()) return default;

            string loadPlayerData = File.ReadAllText(savePath);

            var result = JsonUtility.FromJson<SerializableDictionary<string, SerializableDictionary<string, string>>>(loadPlayerData);

            SaveManager.ProcessLoadedData(result);

            return result;
        }

        public void Save(object saveData)
        {
            string savePlayerData = JsonUtility.ToJson(saveData);
            File.WriteAllText(savePath, savePlayerData);
        }
    }
}
