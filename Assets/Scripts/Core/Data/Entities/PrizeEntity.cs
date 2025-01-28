namespace Core.Data.Entities
{
    public class PrizeEntity
    {
        public PrizeEntity(int amount, int index)
        {
            Amount = amount;
            Index = index;
        }

        public int Amount { get; }
        public int Index { get; }
    }
}