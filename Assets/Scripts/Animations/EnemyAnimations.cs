using EscapeFromMars.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EscapeFromMars
{
    namespace Animations
    {
        public class EnemyAnimations : PersonAnimations, IListener
        {
            public override void Start()
            {
                base.Start();
            }

            public override void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
            {
                base.OnEvent(Event_Type, Sender, Param);
            }
        }
    }
}