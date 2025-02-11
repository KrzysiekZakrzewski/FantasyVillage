using ViewSystem.Implementation;
using UnityEngine;
using Loading.Data;

namespace Game.View
{
    public class LoadingViewController : SingleViewTypeStackController
    {
        [SerializeField]
        private LoadingView loadingView;
        [SerializeField]
        private LoadingScreenDatabase loadingScreenDatabase;

        public bool IsShowPresentationComplete() 
        {
            return loadingView.IsShowPresentationComplete;
        }

        public void OpenLoadingView()
        {
            TryOpenSafe<LoadingView>();

            var background = loadingScreenDatabase.GetRandomLoadingBackground();
            var hint = loadingScreenDatabase.GetRandomHint();

            loadingView.SetupLoadingScreen(hint, background);
        }

        public void OnLoadingCompleted(bool showContinueText)
        {
            if(showContinueText)
                loadingView.ShowContinueText();
        }

        public void CloseLoadingView()
        {
            loadingView.ParentStack.TryPopSafe();
        }
    }
}