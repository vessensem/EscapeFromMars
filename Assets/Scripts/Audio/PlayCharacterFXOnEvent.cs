using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Audio
{
    public class PlayCharacterFXOnEvent : PlaySoundOnEvent
    {
        public override void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            if (Event_Type.Equals(eventType))
            {
                if (Param is ArrayList)
                {
                    var go = ((ArrayList)Param)[0];

                    if (go.Equals(parentGameObject))
                        PlaySound();
                }

                if (Param is GameObject)
                {
                    var go = (GameObject)Param;
                    if (go.Equals(parentGameObject))
                        PlaySound();
                }
            }
        }
    }
}