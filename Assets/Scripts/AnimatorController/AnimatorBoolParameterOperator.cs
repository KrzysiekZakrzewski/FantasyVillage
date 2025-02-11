using UnityEngine;

namespace BlueRacconGames.Animation
{
    public class AnimatorBoolParameterOperator : StateMachineBehaviour
    {
        [SerializeField]
        private string boolParameterName;
        [SerializeField]
        private bool enterParameterStats;
        [SerializeField]
        private bool exitParameterStats;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            animator.SetBool(boolParameterName, enterParameterStats);
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            animator.SetBool(boolParameterName, exitParameterStats);
        }
    }
}