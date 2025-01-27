using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Factories.StateFactory;
using Infrastructure.StateMachine.States;
using Zenject;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : IInitializable
    {
        private readonly StateFactory _stateFactory;
        
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(StateFactory stateFactory)
        {
            _stateFactory = stateFactory;
        }
        
        public async void Initialize()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = _stateFactory.CreateState<BootstrapState>(),
                [typeof(LoadMenuState)] = _stateFactory.CreateState<LoadMenuState>(),
                [typeof(LoadWheelState)] = _stateFactory.CreateState<LoadWheelState>(),
                [typeof(GameLoopState)] = _stateFactory.CreateState<GameLoopState>()
            };
            
            await Enter<BootstrapState>();
        }
        
        public async Task Enter<TState>() 
            where TState : class, IState
        {
            TState state = await ChangeState<TState>();
            await state.Enter();
        }

        public async Task Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedState<TPayload>
        {
            TState state = await ChangeState<TState>();
            await state.Enter(payload);
        }

        private TState GetState<TState>() 
            where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }

        private async Task<TState> ChangeState<TState>() 
            where TState : class, IExitableState
        {
            if(_currentState != null)
                await _currentState.Exit();

            var state = GetState<TState>();
            _currentState = state;
            
            return state;
        }
    }
}