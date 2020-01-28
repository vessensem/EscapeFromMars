using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Data
{
    public class CharacterParametersContainer : MonoBehaviour
    {
        [SerializeField] private CharacterParametersData characterParametersData = null;

        public bool IsDead { get { return characterParametersData.CharacterParameters.CurrentHealth <= 0; } }

        void Awake()
        {
            characterParametersData = Instantiate(characterParametersData);
            characterParametersData.CharacterParameters.FirstInit();
        }

        public void ChangeCurrentHealth(float value)
        {
            characterParametersData.CharacterParameters.CurrentHealth += value;
        }
    }
}