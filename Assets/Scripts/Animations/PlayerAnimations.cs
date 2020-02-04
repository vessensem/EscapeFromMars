using EscapeFromMars.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using EscapeFromMars.Data;

namespace EscapeFromMars
{
    namespace Animations
    {
        public class PlayerAnimations : PersonAnimations, IListener
        {
            public override void Start()
            {
                base.Start();
                _eventManager.AddListener(EVENT_TYPE.START_PLAYER_ANIMATION, this);
                _eventManager.AddListener(EVENT_TYPE.END_PLAYER_ANIMATION, this);
            }

            void OnEndPlayerAnimation(string value)
            {
                if (value.Contains("Attack"))
                    _personMover.CanMove = true;
                _canDoAnimation = true;
            }

            void OnStartPlayerAnimation(string value)
            {
                if (value.Contains("Attack"))
                    _personMover.CanMove = false;
                _canDoAnimation = false;
            }

            public override void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
            {
                base.OnEvent(Event_Type, Sender, Param);
                switch (Event_Type)
                {
                    case EVENT_TYPE.END_PLAYER_ANIMATION:
                        OnEndPlayerAnimation((string)Param);
                        break;
                    case EVENT_TYPE.START_PLAYER_ANIMATION:
                        OnStartPlayerAnimation((string)Param);
                        break;
                }
            }
        }
    }
}