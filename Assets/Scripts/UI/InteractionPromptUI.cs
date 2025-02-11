using TMPro;
using UnityEngine;

namespace Interactable.UI
{
    public class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject uiPanel;
        [SerializeField]
        private TextMeshProUGUI promptTxt;

        private void Awake()
        {
            uiPanel.SetActive(false);
        }

        public bool IsDisplayed => uiPanel.activeInHierarchy;

        public void SetUp(string promptText, Vector2 promptPosition)
        {
            promptTxt.text = promptText;
            uiPanel.transform.position = promptPosition;
            uiPanel.SetActive(true);
        }

        public void Close()
        {
            promptTxt.text = "";
            uiPanel.SetActive(false);
        }
    }
}