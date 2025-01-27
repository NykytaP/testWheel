using System.Threading;
using Infrastructure.Factories;

namespace Infrastructure.Helpers.CancellationTokenHelper
{
    public class CancellationTokenHelper : ICancellationTokenHelper
    {
        private readonly IUIFactory _uiFactory;

        public CancellationTokenHelper(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public CancellationToken GetSceneCancellationToken()
        {
            return _uiFactory.GetSceneCancellationToken();
        }
    }
}