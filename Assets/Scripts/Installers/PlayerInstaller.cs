using System;
using PlayerComponent;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerSettings _playerSettings;
        [SerializeField]
        private InventorySettings _inventorySettings;
        [SerializeField]
        private Inventarizator _inventarizator;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MovementController>().AsSingle()
                .WithArguments(_playerSettings.Speed, _playerSettings.Rigidbody);
            Container.BindInstance(_inventarizator).AsTransient();
            Container.Bind<Inventory>().AsSingle().WithArguments(_inventorySettings);
        }
    }

    [Serializable]
    public class PlayerSettings
    {
        public float Speed => _speed;
        public Rigidbody Rigidbody => _rigidbody;

        [SerializeField]
        private float _speed;
        [SerializeField]
        private Rigidbody _rigidbody;
    }
}