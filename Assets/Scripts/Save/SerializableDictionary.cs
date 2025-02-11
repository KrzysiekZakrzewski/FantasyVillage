using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace Saves.Serializiation
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver, ISerializable
    {
        [SerializeField] 
        private List<SerializableKeyValuePair<TKey, TValue>> list = new List<SerializableKeyValuePair<TKey, TValue>>();

        public SerializableDictionary() { }

        public SerializableDictionary(SerializationInfo info, StreamingContext context) { }

        public void OnBeforeSerialize()
        {
            if (list.Count > Count)
            {
                AddNewValue();
            }
            else
            {
                UpdateSerializedValues();
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            for (var i = 0; list != null && i < list.Count; i++)
            {
                var current = list[i];

#if UNITY_2021_1_OR_NEWER
                if (current.key != null)
                {
                    TryAdd(current.key, current.value);
                }
#elif UNITY_2020
                if (current.key != null)
                {
                    if (ContainsKey(current.key)) continue;
                    Add(current.key, current.value);
                }
#endif
            }
        }

        private void UpdateSerializedValues()
        {
            list.Clear();

            foreach (var pair in this)
            {
                list.Add(pair);
            }
        }

        private void AddNewValue()
        {
#if UNITY_2021_1_OR_NEWER
            var current = list[^1];

            if (current.key != null)
            {
                TryAdd(current.Key, current.value);
            }
#elif UNITY_2020
            var current = list[list.Count - 1];
            
            if (current.key != null)
            {
                if (ContainsKey(current.key)) return;
                Add(current.key, current.value);
            }
#endif
        }
    }
}