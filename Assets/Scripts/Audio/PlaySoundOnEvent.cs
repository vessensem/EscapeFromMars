using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlaySoundOnEvent : MonoBehaviour, IListener
    {
        [SerializeField]
        protected EVENT_TYPE eventType = EVENT_TYPE.EMPTY_EVENT;
        [SerializeField]
        protected AudioClip audioClip = null;

        protected AudioSource audioSource;
        protected GameObject parentGameObject;

        public virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            parentGameObject = transform.parent.gameObject;
    }

        public virtual void Start()
        {
            EventManager.Instance.AddListener(eventType, this);
        }

        public virtual void PlaySound()
        {
            audioSource.PlayOneShot(audioClip);
        }

        public virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            if (Event_Type.Equals(eventType))
                PlaySound();
        }
    }
}