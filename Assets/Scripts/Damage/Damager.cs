using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Damage
{
    [RequireComponent(typeof(AudioSource))]
    public class Damager : MonoBehaviour
    {
        [SerializeField]
        private float _damage = 1f;
        [SerializeField]
        private float _rateDamage = 1f;

        private AudioSource _audioSource;
        private bool _canDamage = true;
        private WaitForSeconds _pauseDamage;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _pauseDamage = new WaitForSeconds(_rateDamage);
        }

        private IEnumerator PauseDamage()
        {
            _canDamage = false;
            yield return _pauseDamage;
            _canDamage = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_canDamage)
                return;

            var damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.ApplyDamage(_damage);
                _audioSource.Play();
                StartCoroutine(PauseDamage());
            }
        }

    }
}