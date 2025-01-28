using Core.Services.SaveLoadService;
using Infrastructure.Data.Storages.BalanceStorage;
using Infrastructure.SessionStorage;

namespace Core.Services.DataManager
{
    public class DataManager : IDataManager
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISessionStorage<BalanceStorage> _balanceStorage;

        public DataManager(ISaveLoadService saveLoadService, ISessionStorage<BalanceStorage> balanceStorage)
        {
            _saveLoadService = saveLoadService;
            _balanceStorage = balanceStorage;
        }
        
        public BalanceStorage LoadBalanceData()
        {
            _balanceStorage.UpdateStorage(_saveLoadService.LoadBalanceStorage());

            return _balanceStorage.Data;
        }
        
        public void SaveBalanceData()
        {
            _saveLoadService.SaveBalanceStorage(_balanceStorage.Data);
        }
    }
}