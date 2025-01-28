using Core.Data.Entities;

namespace Core.Services.WheelPrizeManager
{
    public interface IWheelPrizeManager
    {
        public PrizeEntity[] GenerateAndCachePrizes(int prizesAmount);
        public PrizeEntity CollectPrize();
    }
}