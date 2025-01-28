using Infrastructure.Signals;
using Zenject;

namespace Infrastructure.Installers
{
    public class SignalsInstaller : Installer<SignalsInstaller>
    {
        private SignalBus _signalBus;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            _signalBus = Container.Resolve<SignalBus>();
            
            DeclareSignals();
        }
        
        private void DeclareSignals()
        {
            _signalBus.DeclareSignal<BalanceUpdatedSignal>();
        }
    }
}