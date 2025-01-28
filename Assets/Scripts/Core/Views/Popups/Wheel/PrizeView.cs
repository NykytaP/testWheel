using Core.Data.Entities;
using Core.Utils;
using TMPro;
using UnityEngine;

namespace Core.Views.Popups.Wheel
{
    public class PrizeView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _prizeAmount;
        
        public void SetData(PrizeEntity prizeEntity)
        {
            _prizeAmount.text = BalanceParser.GetParsedBalance(prizeEntity.Amount);
        }
    }
}