using System;
using UnityEngine;

namespace Saves
{
    [Serializable]
    public class SaveValue<T> : SaveValueBase
    {
        [SerializeField] 
        private T value;

        public T Value
        {
            get => (T)ValueObject;
            set => ValueObject = value;
        }

        public override object ValueObject
        {
            get => value;
            set
            {
                if (this.value == null)
                {
                    this.value = (T)value;
                }
                else if (!this.value.Equals(value))
                {
                    this.value = (T)value;
                }
            }
        }

        public SaveValue(string key)
        {
            this.key = key;
        }

        public SaveValue(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        public void Initialize(T defaultValue)
        {
            ValueObject = defaultValue;
        }

        public override void AssignFromObject(SaveValueBase data)
        {
            key = data.key;
            value = (T)data.ValueObject;
        }

        public override void ResetValue()
        {
            Value = default;
        }
    }
}
