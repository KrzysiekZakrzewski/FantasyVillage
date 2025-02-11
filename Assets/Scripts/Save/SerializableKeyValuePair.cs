using System;
using System.Collections.Generic;
using UnityEngine;

namespace Saves.Serializiation
{
    [Serializable]
    public class SerializableKeyValuePair<TKey, TValue> : IEquatable<SerializableKeyValuePair<TKey, TValue>>
    {
        [SerializeField] public TKey key;
        [SerializeField] public TValue value;

        public TKey Key => key;

        public TValue Value
        {
            get => value;
            set => this.value = value;
        }

        public SerializableKeyValuePair() { }

        public SerializableKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public static implicit operator SerializableKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            return new SerializableKeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }

        public static implicit operator KeyValuePair<TKey, TValue>(SerializableKeyValuePair<TKey, TValue> pair)
        {
            return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
        }

        public bool Equals(SerializableKeyValuePair<TKey, TValue> other)
        {
            var comparer1 = EqualityComparer<TKey>.Default;
            var comparer2 = EqualityComparer<TValue>.Default;

            return comparer1.Equals(key, other.key) && comparer2.Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            var comparer1 = EqualityComparer<TKey>.Default;
            var comparer2 = EqualityComparer<TValue>.Default;

            int h0;
            h0 = comparer1.GetHashCode(key);
            h0 = (h0 << 5) + h0 ^ comparer2.GetHashCode(value);
            return h0;
        }

        public override string ToString()
        {
            return $"(Key: {key}, Value: {value})";
        }
    }
}