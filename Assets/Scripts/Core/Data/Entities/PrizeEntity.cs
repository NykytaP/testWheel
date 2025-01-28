namespace Core.Data.Entities
{
    public class PrizeEntity
    {
        public PrizeEntity(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}