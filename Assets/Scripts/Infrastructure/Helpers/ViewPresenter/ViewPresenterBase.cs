using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Infrastructure.Helpers.DisposableHelper;

namespace Infrastructure.Helpers.ViewPresenter
{
    public abstract class ViewPresenterBase
    {
        protected CancellationToken _cachedCancellationToken;
        protected CancellationTokenRegistration _cachedCancellationTokenRegistration;
        protected UniTaskCompletionSource<AsyncUnit> _deinitializeWaiter;
        
        protected List<IDisposable> _disposables = new();

        protected void RegisterToken(params CancellationToken[] cancellationTokens)
        {
            _deinitializeWaiter = new UniTaskCompletionSource<AsyncUnit>();
            CancellationTokenSource source = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokens);
            _cachedCancellationToken = source.Token;
            _cachedCancellationTokenRegistration = _cachedCancellationToken.Register(DeInitialize);
        }

        protected void AddToDisposables(Action act)
        {
            _disposables.Add(act.ToDisposable());
        }

        private void DeInitialize()
        {
            ReleaseCancellationToken();
            FlushDisposables();
            _deinitializeWaiter.TrySetResult(AsyncUnit.Default);
        }

        private void ReleaseCancellationToken()
        {
            _cachedCancellationTokenRegistration.Dispose();
        }

        private void FlushDisposables()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }
    }
}