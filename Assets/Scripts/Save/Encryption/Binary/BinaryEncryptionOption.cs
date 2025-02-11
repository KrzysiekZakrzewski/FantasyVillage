using Saves.Serializiation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Saves
{
    public class BinaryEncryptionOption : IEncryptionOption
    {
        private string savePath;

        public BinaryEncryptionOption(string savePath)
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

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(savePath, FileMode.Open);

            string data = (string)formatter.Deserialize(stream);

            var result = JsonUtility.FromJson<SerializableDictionary<string, SerializableDictionary<string, string>>>(data);

            stream.Close();

            SaveManager.ProcessLoadedData(result);

            return result;
        }

        public void Save(object data)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(savePath, FileMode.Create);

            string savePlayerData = JsonUtility.ToJson(data);

            formatter.Serialize(stream, savePlayerData);

            stream.Close();
        }
    }
}