using Saves.Serializiation;

namespace Saves
{
    public interface IEncryptionOption
    {
        void Save(object saveData);
        SerializableDictionary<string, SerializableDictionary<string, string>> Load();
        void Delete();
        bool FileExist();
    }
}