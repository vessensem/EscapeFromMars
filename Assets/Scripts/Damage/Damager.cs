using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Damage
{
    [RequireComponent(typeof(AudioSource))]
    public class Damager : MonoBehaviour
    {
        [SerializeField] private float damage = 1;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            print("Damage");
            var damagable = other.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.ApplyDamage(damage);
                audioSource.Play();
            }
        }

    }
}