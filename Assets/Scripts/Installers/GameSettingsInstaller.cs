using AnimationSystem;
using Pools;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "Settings/Game")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField]
        private SettingsResourcePool _settingsVisualPool;
        [SerializeField]
        private AnimationSettings _animationSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsVisualPool);
            Container.BindInstance(_animationSettings);
        }
    }
}