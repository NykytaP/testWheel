using Core.Utils;
using TMPro;
using UnityEngine;

namespace Core.Views.Popups.BalancePanel
{
    public class BalancePanelView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _balanceText;

        public void UpdateBalance(int amount)
        {
            _balanceText.text = BalanceParser.GetParsedBalance(amount);
        }
    }
}