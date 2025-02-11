using UnityEngine;

namespace Saves
{
    public class AutoSave : MonoBehaviour
    {
        private static AutoSave instance;

        private static bool isInitialized;

        [RuntimeInitializeOnLoadMethod]
        private static void SetupInstance()
        {
            isInitialized = false;

            if (!AssetsGetter.GetAsset<SaveManagerSettings>().AutoSaves) return;
            if (instance != null) return;

            var obj = new GameObject("Auto Saves");
            obj.AddComponent<AutoSave>();
            instance = obj.GetComponent<AutoSave>();
            DontDestroyOnLoad(obj);

            isInitialized = true;
        }
        private void OnApplicationQuit()
        {
#if !UNITY_EDITOR
            if (!isInitialized) return;
            SaveManager.Save(false);
#endif
        }
        private void OnApplicationFocus(bool hasFocus)
        {
#if !UNITY_EDITOR
            if (!isInitialized) return;
            if (hasFocus) return;
            SaveManager.Save(false);
#endif
        }
    }
}