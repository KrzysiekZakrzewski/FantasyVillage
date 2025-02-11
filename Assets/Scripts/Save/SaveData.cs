using Saves.Serializiation;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Saves
{
    [Serializable]
    [CreateAssetMenu(fileName = nameof(SaveData), menuName = nameof(Saves) + "/" + "Assets" + "/" + nameof(SaveData))]
    public sealed class SaveData : SaveAsset
    {
        [SerializeField] 
        private List<SaveObject> saveData;
        public List<SaveObject> Data
        {
            get => saveData;
            set => saveData = value;
        }
        public SerializableDictionary<string, SerializableDictionary<string, string>> SerializableData
        {
            get
            {
                var items = new SerializableDictionary<string, SerializableDictionary<string, string>>();

                foreach (var saveValue in Data)
                {
                    var data = saveValue.GetSaveValues();

                    var converted = new SerializableDictionary<string, string>();

                    foreach (var v in data.Values)
                    {
                        converted.Add(v.key, JsonUtility.ToJson(v));
                    }

                    items.Add(saveValue.SaveKey, converted);
                }

                return items;
            }
        }

        public void Initialize()
        {
            foreach (var saveValue in saveData)
            {
                SaveManager.RegisterObject(saveValue);
            }
        }
    }
}