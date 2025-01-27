using System.Threading;

namespace Infrastructure.Helpers.CancellationTokenHelper
{
    public interface ICancellationTokenHelper
    {
        public CancellationToken GetSceneCancellationToken();
    }
}