using FindTheCat.Config;
using UnityEngine;
using Zenject;

namespace FindTheCat.Installer
{
    [CreateAssetMenu(fileName = "SearchableConfigInstaller", menuName = "FindTheCat/Installers/SearchableConfigInstaller")]
    public class SearchableConfigInstaller : ScriptableObjectInstaller<SearchableConfigInstaller>
    {
        [SerializeField] private SearchableConfig _config;

        public override void InstallBindings()
        {
            Container
                .BindInstance(_config)
                .AsSingle();
        }
    }
}
