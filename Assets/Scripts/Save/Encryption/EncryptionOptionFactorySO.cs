using UnityEngine;

namespace Saves
{
    public abstract class EncryptionOptionFactorySO : ScriptableObject, IEncryptionOptionFactory
    {        
        public abstract IEncryptionOption CreateEncryption(string savePath);
    }
}