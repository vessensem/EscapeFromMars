using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Data
{
    [System.Serializable]
    public class CharacterParameters 
    {
        [SerializeField] private float defaultMaxHealth = 100;
        [SerializeField] private float startHealth = 70;

        public float DefaultMaxHealth { get => defaultMaxHealth; }
        public float CurrentHealth { get; set; }
        public float CurrentMaxHealth { get; set; }

        public void FirstInit()
        {
            CurrentHealth = startHealth;
            CurrentMaxHealth = defaultMaxHealth;
        }
    }
}