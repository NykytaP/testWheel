using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.Installers
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BootstrapState>().AsSingle().NonLazy();
            Container.Bind<LoadMenuState>().AsSingle().NonLazy();
            Container.Bind<LoadWheelState>().AsSingle().NonLazy();
            Container.Bind<GameLoopState>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle(); //GameStateMachine entry point is Initialize()
        }
    }
}