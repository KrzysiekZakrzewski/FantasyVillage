using UnityEngine;

namespace Saves
{
    [CreateAssetMenu(fileName = nameof(BinaryEncryptionFactorySO), menuName = nameof(Saves) + "/" + "Encryption Option" + "/" + nameof(BinaryEncryptionFactorySO))]
    public class BinaryEncryptionFactorySO : EncryptionOptionFactorySO
    {
        public override IEncryptionOption CreateEncryption(string savePath)
        {
            return new BinaryEncryptionOption(savePath);
        }
    }
}