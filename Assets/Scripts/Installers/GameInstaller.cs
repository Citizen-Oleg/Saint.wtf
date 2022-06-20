using System.Resources;
using AnimationSystem;
using Joystick;
using Pools;
using ResourceSystem;
using ResourceSystem.FactoryResources;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private JoystickController _joystickController;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AnimationManager>().AsSingle();
            Container.Bind<ResourcePool>().AsSingle().NonLazy();
            Container.BindFactory<Transform, ResourceType, ResourceItem, ResourceFactory>();
            Container.BindInstance(_joystickController);
        }
    }
}