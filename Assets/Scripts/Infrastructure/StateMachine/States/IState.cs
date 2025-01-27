using System.Threading.Tasks;

namespace Infrastructure.StateMachine.States
{
    public interface IState : IExitableState
    {
        public Task Enter();
    }

    public interface IExitableState
    {
        public Task Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public Task Enter(TPayload payload);
    }
}