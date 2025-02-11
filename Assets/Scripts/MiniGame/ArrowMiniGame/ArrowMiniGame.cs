using BlueRacconGames.MiniGame.Arrow.Data;
using BlueRacconGames.MiniGame.Factory;
using Inputs;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using PlayerInput = Inputs.PlayerInput;

namespace BlueRacconGames.MiniGame
{
    public class ArrowMiniGame : MiniGameBase
    {
        [NonSerialized]
        protected PlayerInput playerInput;

        private ArrowDifficultyLevel difficultyLevel;

        private ArrowDirectionData[] arrowDirectionDatas;
        private ArrowDirectionData currentArrow;

        private Queue arrowsQueue;

        private float timeToEnd;

        private bool timerStop;

        public ArrowMiniGame(ArrowMiniGameFactorySO initialData)
        {
            difficultyLevel = initialData.DifficultyLevel;
            arrowDirectionDatas = initialData.ArrowDirectionDatas;

            playerInput = InputManager.GetPlayer(0);
        }

        public override void SetupGame(MiniGameController miniGameController)
        {
            base.SetupGame(miniGameController);

            playerInput.AnyButtonDown += CheckInput;

            RandomizeArrowsAmount();

            timeToEnd = difficultyLevel.TimeToEnd;
            isGameReadyToPlay = true;
        }

        public override void StartGame()
        {
            miniGameController.StartCoroutine(Timer());

            GetNextArrow();
        }

        public override void EndMiniGame(bool result)
        {
            this.result = result;
            timerStop = true;
            playerInput.AnyButtonDown -= CheckInput;

            Debug.Log($"End Mini Game with result: {result}");
        }

        private void RandomizeArrowsAmount()
        {
            int arrowAmount = Random.Range(difficultyLevel.MinArrowsAmount, difficultyLevel.MaxArrowsAmount);

            arrowsQueue = new Queue();

            for (int i = 0; i < arrowAmount; i++)
            {
                var newArrowDirection = arrowDirectionDatas[Random.Range(0, arrowDirectionDatas.Length)];

                arrowsQueue.Enqueue(newArrowDirection);
            }
        }

        private void GetNextArrow()
        {
            if(arrowsQueue.Count == 0)
            {
                EndMiniGame(true);
                return;
            }

            currentArrow = arrowsQueue.Dequeue() as ArrowDirectionData;

            if (currentArrow == null) return;
        }

        private void CheckInput(InputControl inputControl)
        {
            foreach(var control in currentArrow.InputAction.controls)
            {
                if (control == inputControl)
                {
                    CorrectInput();
                    return;
                }
            }

            WrongInput();
        }

        private void CorrectInput()
        {
            GetNextArrow();
            Debug.Log("Correct!!");
        }

        private void WrongInput()
        {
            Debug.Log("Wrong!!");
        }

        private IEnumerator Timer()
        {
            while (!timerStop)
            {
                yield return null;

                timeToEnd -= Time.deltaTime;

                if (timeToEnd > 0) continue;

                timerStop = true;
                EndMiniGame(false);
            }
        }
    }
}
