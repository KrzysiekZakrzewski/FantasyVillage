using System;
using UnityEngine;
using Zenject;

namespace Game.Managers.Life
{
    public class LifeManager : MonoBehaviour
    {
        [SerializeField]
        private int baseLifeAmount = 3;

        public int CurrentLifeAmount { get; private set; }
        public event Action OnDieE;

        private void Awake()
        {
            CurrentLifeAmount = baseLifeAmount;
        }

        public void OnDie()
        {
            CurrentLifeAmount--;

            if(CurrentLifeAmount <= 0)
            {
                GameOver();
                return;
            }

            OnDieE?.Invoke();
        }

        private void GameOver()
        {
            Debug.Log("GameOver");

            ResetOnGameOver();
        }

        private void ResetOnGameOver()
        {
            CurrentLifeAmount = baseLifeAmount;
        }
    }
}