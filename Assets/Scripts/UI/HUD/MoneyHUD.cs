using Game.Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.UI.HUD
{
    public class MoneyHUD : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI moneyAmountTxt;

        private MoneyManager moneyManager;

        [Inject]
        private void Inject(MoneyManager moneyManager)
        {
            this.moneyManager = moneyManager;
        }

        private void Awake()
        {
            moneyManager.OnMoneyChangedE += UpdateMoneyAmount;
        }

        private void OnDestroy()
        {
            moneyManager.OnMoneyChangedE -= UpdateMoneyAmount;
        }

        public void UpdateMoneyAmount(int value)
        {
            moneyAmountTxt.text = value.ToString();
        }
    }
}