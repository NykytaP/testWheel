using Core.Data.Entities;

namespace Core.Services.PrizeGenerator
{
    public interface IPrizeGenerator
    {
        public PrizeEntity[] GenerateAndCachePrizes(int prizesAmount);
    }
}