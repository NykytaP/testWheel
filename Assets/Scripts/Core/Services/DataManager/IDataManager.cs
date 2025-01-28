using Infrastructure.Data.Storages.BalanceStorage;

namespace Core.Services.DataManager
{
    public interface IDataManager
    {
        public BalanceStorage LoadBalanceData();
        public void SaveBalanceData();
    }
}