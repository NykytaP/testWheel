using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        public Task InitUIRoot();
        public UniTask<Transform> GetUIRoot();
        public CancellationToken GetSceneCancellationToken();
    }
}