namespace Saves
{
    public interface IEncryptionOptionFactory
    {
        IEncryptionOption CreateEncryption(string savePath);
    }
}