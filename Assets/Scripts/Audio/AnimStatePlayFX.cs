using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStatePlayFX : StateMachineBehaviour
{
    [Tooltip("0-1 Время, когда проиграется звук"), Range(0, 1)]
    public float eventTime;
    public AudioClip audioClip;

    private bool _didEvent = false;
    private float _normalizedTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //     
    // }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _normalizedTime = Mathf.Repeat(stateInfo.normalizedTime, 1);
        if (_normalizedTime >= eventTime && !_didEvent)
        {
            _didEvent = true;
            animator.GetComponentInChildren<AudioSource>().PlayOneShot(audioClip);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _didEvent = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
