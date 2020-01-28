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
        public class PlayerAnimations : MonoBehaviour, IListener
        {
            public float MaxAttackSpeed = 0.2f;

            private Animator _playerAnimator;
            private PersonMover _playerMover;
            private EventManager _eventManager;
            private bool _canDoAnimation = true;

            [Inject]
            void Constructor(EventManager eventManager, Animator playerAnimator, PersonMover playerMover)
            {
                _eventManager = eventManager;
                _playerAnimator = playerAnimator;
                _playerMover = playerMover;
            }

            void Start()
            {
                _eventManager.AddListener(EVENT_TYPE.START_PLAYER_ANIMATION, this);
                _eventManager.AddListener(EVENT_TYPE.END_PLAYER_ANIMATION, this);
                _eventManager.AddListener(EVENT_TYPE.GET_DAMAGE, this);
            }

            private void Update()
            {
                _playerAnimator.SetFloat("MoveSpeed", _playerMover.NormalizedSpeed);
            }

            public void Attack()
            {
                if (CanAttack())
                    _playerAnimator.SetTrigger("Attack");
            }

            public void GetDamage(ArrayList array)
            {
                if ((GameObject)array[0] == gameObject)
                    _playerAnimator.SetTrigger("GetDamage");
            }

            bool CanAttack()
            {
                if (_playerMover.IsGrounded && _canDoAnimation)
                    return true;
                else
                    return false;
            }

            void OnEndPlayerAnimation(string value)
            {
                if (value.Contains("Attack"))
                    _playerMover.CanMove = true;
                _canDoAnimation = true;
            }

            void OnStartPlayerAnimation(string value)
            {
                if (value.Contains("Attack"))
                    _playerMover.CanMove = false;
                _canDoAnimation = false;
            }

            public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
            {
                switch (Event_Type)
                {
                    case EVENT_TYPE.END_PLAYER_ANIMATION:
                        OnEndPlayerAnimation((string)Param);
                        break;
                    case EVENT_TYPE.START_PLAYER_ANIMATION:
                        OnStartPlayerAnimation((string)Param);
                        break;
                    case EVENT_TYPE.GET_DAMAGE:
                        GetDamage((ArrayList)Param);
                        break;
                }
            }
        }
    }
}