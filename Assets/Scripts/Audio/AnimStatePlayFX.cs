using UnityEngine;
using Zenject;

public class AnimStatePlayFX : StateMachineBehaviour
{
    [SerializeField, Tooltip("0-1 Время, когда проиграется звук"), Range(0, 1)]
    private float _eventTime = default;
    [SerializeField]
    private AudioClip _audioClip = default;

    private bool _didEvent = false;
    private float _normalizedTime;
    private AudioSource _audioSource;

    [Inject]
    public void Construct(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _normalizedTime = Mathf.Repeat(stateInfo.normalizedTime, 1);
        if (_normalizedTime >= _eventTime && !_didEvent)
        {
            _didEvent = true;
            _audioSource.PlayOneShot(_audioClip);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _didEvent = false;
    }


}
