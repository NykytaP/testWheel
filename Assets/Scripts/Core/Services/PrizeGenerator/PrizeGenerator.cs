using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Data.Entities;
using UnityEngine;

namespace Core.Services.PrizeGenerator
{
    public class PrizeGenerator : IPrizeGenerator
    {
        private List<PrizeEntity> _generatedPrizes;
        
        public PrizeEntity[] GenerateAndCachePrizes(int prizesAmount)
        {
            const int maxAttempts = 1000;
            System.Random random = new System.Random();
            _generatedPrizes = new List<PrizeEntity>();
            int attempts = 0;

            while (_generatedPrizes.Count < prizesAmount && attempts < maxAttempts)
            {
                int candidateAmount = random.Next(Constants.WheelOfFortuneCore.MinPrizeAmount / Constants.WheelOfFortuneCore.PrizeMultiple, 
                    Constants.WheelOfFortuneCore.MaxPrizeAmount / Constants.WheelOfFortuneCore.PrizeMultiple + 1) * Constants.WheelOfFortuneCore.PrizeMultiple;

                if (_generatedPrizes.All(existing => Mathf.Abs(existing.Amount - candidateAmount) >= Constants.WheelOfFortuneCore.PrizeDelta))
                    _generatedPrizes.Add(new PrizeEntity(candidateAmount));

                attempts++;
            }

            return _generatedPrizes.ToArray();
        }
    }
}