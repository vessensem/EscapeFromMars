using Zenject;

namespace EscapeFromMars.Data
{
    public class TestSceneInstaller : MonoInstaller
    {
        public EventManager MainEventManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(MainEventManager).AsSingle();
            
        }
    }
}