using Saves.Serializiation;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace Saves
{
    [Serializable]
    public abstract class SaveObject : SaveAsset
    {
        [SerializeField, HideInInspector] 
        private bool isExpanded;
        [SerializeField] 
        private string saveKey;

        private Dictionary<string, SaveValueBase> lookupCache;

        public bool IsInitialized => !string.IsNullOrEmpty(saveKey);

        public Dictionary<string, SaveValueBase> Lookup
        {
            get
            {
                if (lookupCache is { } && lookupCache.Count > 0) return lookupCache;
                lookupCache = GetSaveValues();
                return lookupCache;
            }
        }

        public string SaveKey => saveKey;

        private void Reset()
        {
            lookupCache ??= new Dictionary<string, SaveValueBase>();
            lookupCache?.Clear();
        }

        public void Initialize()
        {
            SaveManager.RegisterObject(this);
        }
        public void SetSaveKey(string value)
        {
            saveKey = value;
        }

        public virtual void Save()
        {
            SaveManager.RegisterObject(this);
            SaveManager.UpdateAndSaveObject(this);
        }

        public virtual void Load()
        {
            if (!SaveManager.TryGetSaveValuesLookup(SaveKey, out var data)) return;

            if (data == null) return;

            var saveValues = GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(t => t.GetValue(this)).ToArray();

            foreach (var t in saveValues)
            {
                if (!t.GetType().IsSubclassOf(typeof(SaveValueBase))) continue;
                var sv = (SaveValueBase)t;
                if (!data.ContainsKey(sv.key)) continue;
                JsonUtility.FromJsonOverwrite(data[sv.key], sv);
            }
        }

        public virtual void ResetObjectSaveValues()
        {
            foreach (var saveValue in Lookup.Values)
            {
                saveValue.ResetValue();
            }

            lookupCache?.Clear();
        }

        public virtual SerializableDictionary<string, SaveValueBase> GetSaveValues()
        {
            var dic = new SerializableDictionary<string, SaveValueBase>();


            var saveValues = GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Select(t => t.GetValue(this)).ToArray();


            for (var i = 0; i < saveValues.Length; i++)
            {
                if (!saveValues[i].GetType().IsSubclassOf(typeof(SaveValueBase))) continue;
                var sv = (SaveValueBase)saveValues[i];
                dic.Add(sv.key, sv);
            }


            return dic;
        }

        public bool HasValue(string key)
        {
            return Lookup.ContainsKey(key);
        }

        public SaveValue<T> GetValue<T>(string key)
        {
            if (!Lookup.ContainsKey(key)) return default;
            var saveValue = (SaveValue<T>)Lookup[key];
            return saveValue;
        }

        public void SetValue(string key, object value)
        {
            if (!Lookup.ContainsKey(key)) return;
            Lookup[key].ValueObject = value;
        }

        public void ResetElement(string key)
        {
            if (!SaveManager.TryResetElementFromSave(SaveKey, key)) return;

            if (!Lookup.ContainsKey(key)) return;
            Lookup[key].ResetValue();
        }

        [AttributeUsage(AttributeTargets.Class)]
        protected sealed class SaveCategoryAttribute : Attribute
        {
            public string Category;

            public int OrderInCategory;

            public SaveCategoryAttribute(string category, int order = 0)
            {
                Category = category;
                OrderInCategory = order;
            }
        }
    }
}