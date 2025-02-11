using UnityEngine;

namespace BlueRacconGames.Animation
{
    public static class AnimationHashIDs
    {
        public const string OPEN_ANIMATION_STRING = "Chest_Opening";
        public const string CLOSE_ANIMATION_STRING = "Chest_Closing";

        public static bool IsInitialized { get; private set; }

        public static int OpenAnimationHash {  get; private set; }
        public static int CloseAnimationHash { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        private static void RuntimeInitializeOnLoad()
        {
            IsInitialized = false;
            Initialize();
        }

        public static void Initialize()
        {
            OpenAnimationHash = Animator.StringToHash(OPEN_ANIMATION_STRING);
            CloseAnimationHash = Animator.StringToHash(CLOSE_ANIMATION_STRING);
            IsInitialized = true;
        }
    }
}