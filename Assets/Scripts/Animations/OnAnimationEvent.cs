using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Animations
{
    public class OnAnimationEvent : StateMachineBehaviour
    {
        public string EventName;
        [Tooltip("0-1 Время эвента")]
        [Range(0, 1)]
        public float EventSignalTime;

        public EVENT_TYPE EventSignal = EVENT_TYPE.EVENT_PLAYER_ANIMATION;
        public EVENT_TYPE StartEvent = EVENT_TYPE.START_PLAYER_ANIMATION;
        public EVENT_TYPE EndEvent = EVENT_TYPE.END_PLAYER_ANIMATION;

        private bool didEvent = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EventManager.Instance.PostNotification(StartEvent, animator, EventName);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.normalizedTime >= EventSignalTime && !didEvent && EventSignalTime > 0)
            {
                didEvent = true;
                EventManager.Instance.PostNotification(EventSignal, animator, EventName);
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            EventManager.Instance.PostNotification(EndEvent, animator, EventName);
        }

    }
}