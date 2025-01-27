using System.Threading.Tasks;
using Infrastructure.SceneManagement;
using UnityEngine;

namespace Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public async Task Enter()
        {
            ConfigureApp();
            
            await _stateMachine.Enter<LoadMenuState>();
        }

        public async Task Exit()
        {
        }
        
        private void ConfigureApp()
        {
            Application.targetFrameRate = 120;
            Application.quitting += OnQuit;
        }
        
        private void OnQuit()
        {
        }
    }
}