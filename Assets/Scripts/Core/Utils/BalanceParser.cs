namespace Core.Utils
{
    public static class BalanceParser
    {
        public static string GetParsedBalance(int balance)
        {
            return balance switch
            {
                >= 1000000 => (balance / 1000000.0).ToString("0.##") + "M",
                >= 1000 => (balance / 1000.0).ToString("0.##") + "K",
                _ => balance.ToString()
            };
        }
    }
}