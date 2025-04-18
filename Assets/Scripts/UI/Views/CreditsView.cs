using ViewSystem.Implementation;
using UnityEngine;
using TMPro;

namespace Game.View
{
    public class CreditsView : BasicView
    {
        [SerializeField]
        private UIButtonBase backButton;
        [SerializeField]
        private TextMeshProUGUI version;

        public override bool Absolute => false;

        protected override void Awake()
        {
            base.Awake();

            version.text = $"Version: {Application.version}";

            backButton.OnClickE += OnClickBackPerformed;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            backButton.OnClickE -= OnClickBackPerformed;
        }

        private void OnClickBackPerformed()
        {
            ParentStack.TryPopSafe();
        }
    }
}