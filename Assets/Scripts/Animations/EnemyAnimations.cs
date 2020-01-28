using EscapeFromMars.Controls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EscapeFromMars
{
    namespace Animations
    {
        public class EnemyAnimations : MonoBehaviour, IListener
        {
            public float MaxAttackSpeed = 0.2f;

            private Animator _animator;
            private PersonMover _enemyMover;
            private EventManager _eventManager;
            private bool _canDoAnimation = true;

            [Inject]
            void Constructor(EventManager eventManager, Animator enemyAnimator, PersonMover enemyMover)
            {
                _eventManager = eventManager;
                _animator = enemyAnimator;
                _enemyMover = enemyMover;
            }

            void Start()
            {
                _eventManager.AddListener(EVENT_TYPE.START_ENEMY_ANIMATION, this);
                _eventManager.AddListener(EVENT_TYPE.END_ENEMY_ANIMATION, this);
                _eventManager.AddListener(EVENT_TYPE.GET_DAMAGE, this);
                _eventManager.AddListener(EVENT_TYPE.CHARACTER_DIED, this);
            }

            private void Update()
            {
                _animator.SetFloat("MoveSpeed", _enemyMover.NormalizedSpeed);
            }

            public void Attack()
            {
                if (CanAttack())
                    _animator.SetTrigger("Attack");
            }

            public void GetDamage(ArrayList array)
            {
                if (((GameObject)array[0]).Equals(gameObject))
                    _animator.SetTrigger("GetDamage");
            }

            bool CanAttack()
            {
                if (_enemyMover.IsGrounded && _canDoAnimation)
                    return true;
                else
                    return false;
            }

            void Die(GameObject enemy)
            {
                if (enemy.Equals(gameObject))
                    _animator.SetTrigger("Die");
            }

            // void OnEndPlayerAnimation(string value)
            //  {
            //      if (value.Contains("Attack"))
            //          EnemyMover.CanMove = true;
            //      canDoAnimation = true;
            //  }

            //  void OnStartPlayerAnimation(string value)
            //  {
            //      if (value.Contains("Attack"))
            //          EnemyMover.CanMove = false;
            //      canDoAnimation = false;
            //   }

            public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
            {
                switch (Event_Type)
                {
                    //  case EVENT_TYPE.END_ENEMY_ANIMATION:
                    //       OnEndPlayerAnimation((string)Param);
                    //       break;
                    //   case EVENT_TYPE.START_ENEMY_ANIMATION:
                    //       OnStartPlayerAnimation((string)Param);
                    //       break;
                    case EVENT_TYPE.GET_DAMAGE:
                        GetDamage((ArrayList)Param);
                        break;
                    case EVENT_TYPE.CHARACTER_DIED:
                        Die((GameObject)Param);
                        break;
                }
            }
        }
    }
}