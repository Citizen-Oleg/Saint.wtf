using BuildingSystem;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BuildingMinerInstaller : MonoInstaller
    {
        [SerializeField]
        private SettingsBuildingMiner _settingsBuildingMiner;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuildingMiner>().AsSingle().WithArguments(_settingsBuildingMiner).NonLazy();
        }
    }
}
