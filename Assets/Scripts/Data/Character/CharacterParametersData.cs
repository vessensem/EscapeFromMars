using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Data
{
    public class CharacterParametersData : ScriptableObject
    {
        [SerializeField] private CharacterParameters characterParameters = null;

        public CharacterParameters CharacterParameters { get => characterParameters; set => characterParameters = value; }
    }
}