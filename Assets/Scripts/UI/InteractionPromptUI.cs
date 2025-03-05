using TMPro;
using UnityEngine;

namespace Interactable.UI
{
    public class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI promptTxt;

        public bool IsDisplayed => canvasGroup.alpha > 0;

        private void Awake()
        {
            Close();
        }

        public void SetUp(string promptText, Vector2 promptPosition)
        {
            promptTxt.text = promptText;
            canvasGroup.transform.position = Camera.main.WorldToScreenPoint(promptPosition);
            canvasGroup.alpha = 1f;
        }

        public void Close()
        {
            promptTxt.text = "";
            canvasGroup.alpha = 0f;
        }
    }
}