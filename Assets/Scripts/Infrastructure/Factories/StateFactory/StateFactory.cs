﻿using Infrastructure.StateMachine.States;
using Zenject;
namespace Infrastructure.Factories.StateFactory
{
    public class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container) =>
            _container = container;

        public T CreateState<T>() 
            where T : IExitableState
        {
            return _container.Resolve<T>();
        }
    }
}