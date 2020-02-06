using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Damage
{
    [RequireComponent(typeof(AudioSource))]
    public class Damager : MonoBehaviour
    {
        [SerializeField]
        private float _damage = 1;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                print("Damage");
                damagable.ApplyDamage(_damage);
                _audioSource.Play();
            }
        }

    }
}