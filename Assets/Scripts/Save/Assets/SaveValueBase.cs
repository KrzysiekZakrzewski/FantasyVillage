using System;
using UnityEngine;

namespace Saves
{
    [Serializable]
    public abstract class SaveValueBase
    {
        [HideInInspector] public string key;

        public abstract object ValueObject { get; set; }

        public abstract void ResetValue();

        public abstract void AssignFromObject(SaveValueBase value);
    }
}
