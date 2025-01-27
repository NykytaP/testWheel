using Infrastructure.AssetsManagement;
using Infrastructure.Factories.StateFactory;
using Infrastructure.SceneManagement;
using Infrastructure.SessionStorage;
using Zenject;

namespace Infrastructure.Installers
{
    public class InfrastructureInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ISessionStorage<>)).To(typeof(SessionStorage<>)).AsCached();
            
            BindFactories();
            BindLoaders();
            BindServices();
            BindManagers();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
        }

        private void BindLoaders()
        {
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
        }

        private void BindManagers()
        {
        }
    }
}