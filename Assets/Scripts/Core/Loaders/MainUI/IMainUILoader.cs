using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.Loaders;
using UnityEngine;

namespace Core.Loaders.MainUI
{
    public interface IMainUILoader : ILoader
    {
        public UniTask<Canvas> LoadUIRoot();
    }
}