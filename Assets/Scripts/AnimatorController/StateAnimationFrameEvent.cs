using BlueRacconGames.Events;
using UnityEngine;

namespace BlueRacconGames.Animation
{
    public class StateAnimationFrameEvent : StateMachineBehaviour
    {
        [SerializeField] private DefaultGameEvent frameEventE;
        [SerializeField] private float frameNumber = 0;

        private bool eventInvoked = false;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            eventInvoked = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (stateInfo.normalizedTime < frameNumber / (stateInfo.length  * 10) || eventInvoked) return;

            InvokeAnimation();
        }

        void InvokeAnimation()
        {
            if (eventInvoked) return;

            eventInvoked = true;

            frameEventE.Reise();
        }
    }
}