using EscapeFromMars.Controls;
using System.Collections;
using UnityEngine;
using Zenject;

namespace EscapeFromMars.Animations
{
    public class PersonAnimations : MonoBehaviour, IListener
    {
        protected Animator _animator;
        protected PersonMover _personMover;
        protected EventManager _eventManager;
        protected bool _canDoAnimation = true;

        public virtual void Start()
        {
            _eventManager.AddListener(EVENT_TYPE.GET_DAMAGE, this);
            _eventManager.AddListener(EVENT_TYPE.CHARACTER_DIED, this);
        }

        [Inject]
        private void Constructor(EventManager eventManager, Animator playerAnimator, PersonMover playerMover)
        {
            _eventManager = eventManager;
            _animator = playerAnimator;
            _personMover = playerMover;
        }

        private void Update()
        {
            _animator.SetFloat("MoveSpeed", _personMover.NormalizedSpeed);
        }

        public void Attack()
        {
            if (CanAttack())
                _animator.SetTrigger("Attack");
        }

        public void GetDamage(ArrayList array)
        {
            if ((GameObject)array[0] == gameObject)
                _animator.SetTrigger("GetDamage");
        }

        private void Die(GameObject person)
        {
            if (person.Equals(gameObject))
                _animator.SetTrigger("Die");
        }

        private bool CanAttack()
        {
            if (_personMover.IsGrounded && _canDoAnimation)
                return true;
            else
                return false;
        }

        public virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
        {
            switch (Event_Type)
            {
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