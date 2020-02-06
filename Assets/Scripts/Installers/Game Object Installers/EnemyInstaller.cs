using EscapeFromMars.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeFromMars.Data
{
    public class EnemyInstaller : PersonInstaller
    {
        public EnemyAnimations EnemyAnimations;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.BindInstance(EnemyAnimations).AsSingle();
        }
    }
}