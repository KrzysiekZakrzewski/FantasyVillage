using System;
using UnityEngine;

namespace Game.Managers
{
    public class MoneyManager : MonoBehaviour
    {
        private int moneyAmount;

        public int MoneyAmount => moneyAmount;

        public event Action<int> OnMoneyChangedE;

        private void Start()
        {
            Load();
        }

        public void SetMoneyAmount(int value)
        {
            moneyAmount = value;

            OnMoneyChangedE?.Invoke(moneyAmount);
        }

        public void AddMoney(int value)
        {
            moneyAmount += value;

            OnMoneyChangedE?.Invoke(moneyAmount);
        }

        public bool TryRemoveMoney(int value)
        {
            if (!HaveEnoughtMoney(value)) return false;

            moneyAmount -= value;

            OnMoneyChangedE?.Invoke(moneyAmount);

            return true;
        }

        public bool HaveEnoughtMoney(int value)
        {
            return moneyAmount >= value;
        }

        private void Load()
        {
            SetMoneyAmount(100);
        }

        private void Save()
        {

        }
    }
}