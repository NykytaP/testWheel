namespace Core.Services.BalanceService
{
    public interface IBalanceService
    {
        public int GetBalance();
        public void AddMoney(int amount);
    }
}