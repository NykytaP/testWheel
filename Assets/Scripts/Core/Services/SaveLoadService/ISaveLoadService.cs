using Infrastructure.Data.Storages.BalanceStorage;

namespace Core.Services.SaveLoadService
{
    public interface ISaveLoadService
    {
        public BalanceStorage LoadBalanceStorage();
        public void SaveBalanceStorage(BalanceStorage data);
    }
}