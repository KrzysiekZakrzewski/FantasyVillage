using ViewSystem.Implementation;
using UnityEngine;
using TMPro;

namespace Game.View
{
    public class CreditsView : BasicView
    {
        [SerializeField]
        private UIButton backButton;
        [SerializeField]
        private TextMeshProUGUI version;

        public override bool Absolute => false;

        protected override void Awake()
        {
            base.Awake();

            version.text = $"Version: {Application.version}";

            backButton.SetupButtonEvent(OnClickBackPerformed);
        }

        private void OnClickBackPerformed()
        {
            ParentStack.TryPopSafe();
        }
    }
}