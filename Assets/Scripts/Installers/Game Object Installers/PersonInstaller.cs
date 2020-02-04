using EscapeFromMars.Controls;
using UnityEngine;
using Zenject;

//Test Inject
namespace EscapeFromMars.Data
{
    public class PersonInstaller : MonoInstaller
    {
        public Animator Animator;
        public PersonMover PersonMover;
        public CharacterController CharacterController;
        public CharacterParametersContainer CharacterParametersContainer;
        public Collider Collider;
        public AudioSource AudioSource;

        public override void InstallBindings()
        {
            Container.BindInstance(Animator).AsSingle();
            Container.BindInstance(PersonMover).AsSingle();
            Container.BindInstance(CharacterController).AsSingle();
            Container.BindInstance(CharacterParametersContainer).AsSingle();
            Container.BindInstance(Collider).AsSingle();
            Container.BindInstance(AudioSource).AsSingle();
        }
    }
}
