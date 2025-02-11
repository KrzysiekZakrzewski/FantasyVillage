using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Saves.Serializiation;

namespace Saves
{
    public static class SaveManager
    {
        private static SaveData SaveData => AssetsGetter.GetAsset<SaveData>();
        private static SaveManagerSettings Settiings => AssetsGetter.GetAsset<SaveManagerSettings>();

        private static List<ISaveListener> saveListeners = new List<ISaveListener>();
        private static List<ILoadListener> loadListeners = new List<ILoadListener>();

        private static IEncryptionOption encryption;

        private static Dictionary<string, SaveObject> SaveDataLUT { get; set; }
        private static SerializableDictionary<string, SerializableDictionary<string, string>> LoadedDataLUT { get; set; } = new SerializableDictionary<string, SerializableDictionary<string, string>>();

        public static bool IsInitialized { get; private set; }
        public static bool IsSaving { get; private set; }

        public static readonly SavesEvent Saved = new SavesEvent();
        public static readonly SavesEvent Loaded = new SavesEvent();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void RuntimeInitializeOnLoad()
        {
            IsInitialized = false;
            Initialize();
        }

        private static void Initialize()
        {
            if (IsInitialized) return;

            encryption = CreateEncryptionMethod();

            if (!encryption.FileExist())
                Save();

            TryGenerateSaveLookup();

            LoadedDataLUT = LoadFromFile();

            foreach (var saveValue in SaveDataLUT.ToList())
            {
                saveValue.Value.Load();
            }

            IsInitialized = true;
            Loaded.Raise();
        }

        private static IEncryptionOption CreateEncryptionMethod()
        {
            return Settiings.EncryptionOptionFactorySO.CreateEncryption(Settiings.SavePath);
        }

        private static void SaveToFile(SerializableDictionary<string, SerializableDictionary<string, string>> data)
        {
            encryption.Save(data);
        }

        private static SerializableDictionary<string, SerializableDictionary<string, string>> LoadFromFile()
        {
            return encryption.Load();
        }

        private static void TryGenerateSaveLookup(bool ignoreCheck = false)
        {
            if (!ignoreCheck)
            {
                if (SaveDataLUT != null) return;
            }

            SaveDataLUT = new Dictionary<string, SaveObject>();

            foreach (var saveValue in SaveData.Data)
            {
                if (SaveDataLUT.ContainsKey(saveValue.SaveKey))
                    SaveDataLUT[saveValue.SaveKey] = saveValue;
                else
                    SaveDataLUT.Add(saveValue.SaveKey, saveValue);
            }
        }

        private static void CallSaveListeners(bool saveCalled)
        {
            if (!Application.isPlaying) return;

            foreach(var listener in saveListeners)
            {
                if (saveCalled)
                {
                    listener.OnGameSaveCalled();
                }
                else
                {
                    listener.OnGameSaveCompleted();
                }
            }
        }

        private static void CallLoadListeners(bool loadCalled)
        {
            if (!Application.isPlaying) return;

            foreach (var listener in loadListeners)
            {
                if (loadCalled)
                {
                    listener.OnGameLoadCalled();
                }
                else
                {
                    listener.OnGameLoadCompleted();
                }
            }
        }

        private static void SetSaveObjectData(SaveObject saveObject)
        {
            TryGenerateSaveLookup();

            if (SaveDataLUT.ContainsKey(saveObject.SaveKey))
            {
                SaveDataLUT[saveObject.SaveKey] = saveObject;
            }
            else
            {
                SaveDataLUT.Add(saveObject.SaveKey, saveObject);
            }
        }

        private static SerializableDictionary<string, string> GetSaveValuesLookup(string saveKey)
        {
            if(LoadedDataLUT == null || LoadedDataLUT.Count == 0) return null;

            if(LoadedDataLUT.ContainsKey(saveKey))
                return LoadedDataLUT[saveKey];


            if (!SaveDataLUT.ContainsKey(saveKey)) return null;

            var d = SaveDataLUT[saveKey].GetSaveValues();
            var converted = new SerializableDictionary<string, string>();

            foreach (var pair in d)
            {
                converted.Add(pair.Key, JsonUtility.ToJson(pair.Value));
            }

            return converted;
        }

        private static void DeleteSaveFile()
        {
            if (!encryption.FileExist()) return;
            encryption.Delete();
            LoadedDataLUT?.Clear();
            SaveDataLUT?.Clear();
        }

        public static T GetSaveObject<T>(string saveObjectKey) where T : SaveObject
        {
            if (SaveDataLUT.ContainsKey(saveObjectKey))
            {
                return (T)SaveDataLUT[saveObjectKey];
            }

            return null;
        }

        public static T GetSaveObject<T>() where T : SaveObject
        {
            foreach (var saveObj in SaveDataLUT.Values)
            {
                if (saveObj.GetType() != typeof(T)) continue;
                return (T)saveObj;
            }

            return null;
        }

        public static bool TryGetSaveObject<T>(out T saveObject) where T : SaveObject
        {
            saveObject = GetSaveObject<T>();
            return saveObject != null;
        }

        public static bool TryGetSaveValuesLookup(string saveKey, out SerializableDictionary<string, string> data)
        {
            data = GetSaveValuesLookup(saveKey);
            return data != null;
        }

        public static bool TryResetElementFromSave(string objectKey, string valueKey)
        {
            if (!IsInitialized)
            {
                Initialize();
            }


            if (!LoadedDataLUT.ContainsKey(objectKey)) return false;
            if (!LoadedDataLUT[objectKey].ContainsKey(valueKey)) return false;


            LoadedDataLUT[objectKey].Remove(valueKey);
            SaveToFile(SaveData.SerializableData);
            return true;
        }

        public static void UpdateAndSaveObject(SaveObject saveObject)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            if (!TryGetSaveValuesLookup(saveObject.SaveKey, out var data)) return;

            var d = saveObject.GetSaveValues();
            var converted = new SerializableDictionary<string, string>();

            foreach (var pair in d)
            {
                converted.Add(pair.Key, JsonUtility.ToJson(pair.Value));
            }

            data = converted;

            if (LoadedDataLUT is { } && LoadedDataLUT.Count > 0)
            {
                LoadedDataLUT[saveObject.SaveKey] = data;
            }
            else
            {
                LoadedDataLUT = new SerializableDictionary<string, SerializableDictionary<string, string>>()
                {
                    { saveObject.SaveKey, data }
                };
            }
        }

        public static void RegisterObject(SaveObject saveObject)
        {
            SetSaveObjectData(saveObject);
        }

        public static void Save(bool callListeners = true)
        {
            IsSaving = true;

            if (callListeners)
            {
                CallSaveListeners(true);
            }

            TryGenerateSaveLookup();

            foreach (var saveValue in SaveDataLUT.Values.ToList())
            {
                RegisterObject(saveValue);
            }

            SaveToFile(SaveData.SerializableData);
            IsSaving = false;

            if (callListeners)
            {
                CallSaveListeners(false);
            }

            Saved.Raise();
        }

        public static void Load(SerializableDictionary<string, SerializableDictionary<string, string>> data = null, bool callListeners = true)
        {
            if (callListeners)
            {
                CallLoadListeners(true);
            }

            var loadedData = data ?? LoadFromFile();

            if (SaveData != null)
            {
                LoadedDataLUT = loadedData;
            }
            else
            {
                LoadedDataLUT = new SerializableDictionary<string, SerializableDictionary<string, string>>();
            }


            TryGenerateSaveLookup();


            foreach (var saveValue in SaveDataLUT.ToList())
            {
                saveValue.Value.Load();
            }

            if (callListeners)
            {
                CallLoadListeners(false);
            }

            Loaded.Raise();
        }

        public static void ProcessLoadedData(SerializableDictionary<string, SerializableDictionary<string, string>> serializableDictionary)
        {
            var keys = serializableDictionary.Keys.ToList();

            if (keys.Count <= 0) return;

            foreach (var saveObject in SaveData.Data)
            {
                if (keys.Contains(saveObject.SaveKey))
                {
                    if (serializableDictionary[saveObject.SaveKey] == null)
                    {
                        continue;
                    }

                    foreach (var pair in serializableDictionary[saveObject.SaveKey])
                    {
                        var t = saveObject.GetSaveValues()[pair.Key].GetType();
                        saveObject.SetValue(pair.Key, ((SaveValueBase)JsonUtility.FromJson(serializableDictionary[saveObject.SaveKey][pair.Key], t)).ValueObject);
                    }
                }
            }
        }

        public static void Clear()
        {
            TryGenerateSaveLookup(true);

            if (SaveDataLUT != null)
            {
                if (SaveDataLUT.Count > 0)
                {
                    foreach (var saveValue in SaveDataLUT.Values.ToList())
                    {
                        saveValue.ResetObjectSaveValues();
                    }
                }
            }

            SaveToFile(SaveData.SerializableData);
        }
    }
}