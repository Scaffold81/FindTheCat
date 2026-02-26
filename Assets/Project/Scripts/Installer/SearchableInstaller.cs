using FindTheCat.Service;
using Zenject;

namespace FindTheCat.Installer
{
    public class SearchableInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SearchableService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<HintService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<SoundService>()
                .AsSingle()
                .NonLazy();
        }
    }
}
