using EscapeFromMars.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EscapeFromMars.Damage
{
    public class CharacterDamagable : MonoBehaviour, IDamagable
    {
        private CharacterParametersContainer _characterParametersContainer;
        private Collider _thisCollider;
        private EventManager _eventManager;

        [Inject]
        void Constructor(EventManager eventManager, CharacterParametersContainer characterParametersContainer, Collider collider)
        {
            _eventManager = eventManager;
            _characterParametersContainer = characterParametersContainer;
            _thisCollider = collider;
        }
               
        public void ApplyDamage(float damage)
        {
            if (_characterParametersContainer.IsDead)
                return;

            _eventManager.PostNotification(EVENT_TYPE.GET_DAMAGE, this, new ArrayList { gameObject, damage });
            _characterParametersContainer.ChangeCurrentHealth(-damage);

            if (_characterParametersContainer.IsDead)
                Die();
        }

        public void Die()
        {
            Debug.Log("Character Died!|" + this.name);
            _thisCollider.enabled = false;
            _eventManager.PostNotification(EVENT_TYPE.CHARACTER_DIED, this, gameObject);
        }
    }
}