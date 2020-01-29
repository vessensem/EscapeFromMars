using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Data
{
    public class CharacterParametersContainer : MonoBehaviour
    {
        public CharacterParametersData СharacterParametersData = null;

        public bool IsDead { get { return СharacterParametersData.CharacterParameters.CurrentHealth <= 0; } }

        void Awake()
        {
            СharacterParametersData = Instantiate(СharacterParametersData);
            СharacterParametersData.CharacterParameters.FirstInit();
        }

        public void ChangeCurrentHealth(float value)
        {
            СharacterParametersData.CharacterParameters.CurrentHealth += value;
        }
    }
}