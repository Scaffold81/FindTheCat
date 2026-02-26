using FindTheCat.Config;
using UnityEngine;
using Zenject;

namespace FindTheCat.Installer
{
    [CreateAssetMenu(fileName = "SoundConfigInstaller", menuName = "FindTheCat/Installers/SoundConfigInstaller")]
    public class SoundConfigInstaller : ScriptableObjectInstaller<SoundConfigInstaller>
    {
        [SerializeField] private SoundConfig _config;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_config)
                .AsSingle();
        }
    }
}
