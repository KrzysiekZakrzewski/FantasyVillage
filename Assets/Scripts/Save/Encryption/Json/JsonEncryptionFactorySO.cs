using UnityEngine;

namespace Saves
{
    [CreateAssetMenu(fileName = nameof(JsonEncryptionFactorySO), menuName = nameof(Saves) + "/" + "Encryption Option" + "/" + nameof(JsonEncryptionFactorySO))]
    public class JsonEncryptionFactorySO : EncryptionOptionFactorySO
    {
        public override IEncryptionOption CreateEncryption(string savePath)
        {
            return new JsonEncryptionOption(savePath);
        }
    }
}
