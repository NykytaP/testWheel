using Core.Services.DataManager;
using Infrastructure.Data.Storages.BalanceStorage;
using Infrastructure.SessionStorage;

namespace Core.Services.BalanceService
{
    public class BalanceService : IBalanceService
    {
        private readonly ISessionStorage<BalanceStorage> _balanceStorage;
        private readonly IDataManager _dataManager;

        public BalanceService(ISessionStorage<BalanceStorage> balanceStorage, IDataManager dataManager)
        {
            _balanceStorage = balanceStorage;
            _dataManager = dataManager;
        }
        
        public int GetBalance()
        {
            return _balanceStorage.Data.MoneyAmount;
        }

        public void AddMoney(int amount)
        {
            _balanceStorage.Data.MoneyAmount += amount;
            _dataManager.SaveBalanceData();
        }
    }
}