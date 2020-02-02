using UnityEngine;
using UnityEngine.Events;

namespace EscapeFromMars.Events
{
    public class OnEventAction : MonoBehaviour, IListener
    {
        public EVENT_TYPE onEvent;
        public UnityEvent action;

        void Start()
        {
            EventManager.Instance.AddListener(onEvent, this);
        }

        public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            if (Event_Type == onEvent)
                action.Invoke();
        }

    }
}