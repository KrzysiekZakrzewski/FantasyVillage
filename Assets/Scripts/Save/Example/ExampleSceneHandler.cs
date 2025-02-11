using UnityEngine;
using UnityEngine.UI;

namespace Saves.Example
{
    public class ExampleSceneHandler : MonoBehaviour
    {
        private ExampleSaveObject saveObject;

        [Header("Load Text Display's")]
#pragma warning disable 
        [SerializeField] private Text name;
#pragma warning restore 
        [SerializeField] private Text health;
        [SerializeField] private Text shields;

        [SerializeField]
        private string playerName;
        [SerializeField]
        private int healthValue;
        [SerializeField]
        private int shieldsValue;

        private void OnEnable()
        {
            saveObject = SaveManager.GetSaveObject<ExampleSaveObject>();
            LoadExampleData();
        }

        public void SaveExampleData()
        {
            saveObject.playerShield.Value = shieldsValue;
            saveObject.playerHealth.Value = healthValue;
            saveObject.playerName.Value = playerName;

            saveObject.Save();
            SaveManager.Save();
        }

        public void LoadExampleData()
        {
            saveObject.Load();

            name.text = saveObject.playerName.Value;
            health.text = saveObject.playerHealth.Value.ToString();
            shields.text = saveObject.playerShield.Value.ToString();
        }

        public void ClearExampleData()
        {
            saveObject.ResetObjectSaveValues();
            SaveManager.Save();
        }
    }
}