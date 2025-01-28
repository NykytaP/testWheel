using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Data.Entities;
using Core.Services.BalanceService;
using UnityEngine;

namespace Core.Services.WheelPrizeManager
{
    public class WheelPrizeManager : IWheelPrizeManager
    {
        private readonly IBalanceService _balanceService;
        
        private List<PrizeEntity> _cachedPrizes;

        public WheelPrizeManager(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }
        
        public PrizeEntity[] GenerateAndCachePrizes(int prizesAmount)
        {
            const int maxAttempts = 1000;
            System.Random random = new System.Random();
            _cachedPrizes = new List<PrizeEntity>();
            int attempts = 0;

            while (_cachedPrizes.Count < prizesAmount && attempts < maxAttempts)
            {
                int candidateAmount = random.Next(Constants.WheelOfFortuneCore.MinPrizeAmount / Constants.WheelOfFortuneCore.PrizeMultiple, 
                    Constants.WheelOfFortuneCore.MaxPrizeAmount / Constants.WheelOfFortuneCore.PrizeMultiple + 1) * Constants.WheelOfFortuneCore.PrizeMultiple;

                if (_cachedPrizes.All(existing => Mathf.Abs(existing.Amount - candidateAmount) >= Constants.WheelOfFortuneCore.PrizeDelta))
                    _cachedPrizes.Add(new PrizeEntity(candidateAmount, _cachedPrizes.Count));

                attempts++;
            }

            return _cachedPrizes.ToArray();
        }

        public PrizeEntity CollectPrize()
        {
            int prizeIndex = GetRandomPrize();
            PrizeEntity prizeEntity = _cachedPrizes[prizeIndex];

            _balanceService.AddMoney(prizeEntity.Amount);

            return prizeEntity;
        }
        
        private int GetRandomPrize()
        {
            if (_cachedPrizes.Count == 0)
                return -1;

            return Random.Range(0, _cachedPrizes.Count);
        }
    }
}