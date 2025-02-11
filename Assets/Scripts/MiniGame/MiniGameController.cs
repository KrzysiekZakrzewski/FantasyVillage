using BlueRacconGames.MiniGame.Factory;
using Inputs;
using System;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.MiniGame
{
    public class MiniGameController : MonoBehaviour
    {
        [SerializeField]
        private ArrowMiniGameFactorySO testArrowMiniGame;

        [NonSerialized]
        protected PlayerInput playerInput;

        private IMiniGame currentMiniGame;

        private void Start()
        {
            playerInput = InputManager.GetPlayer(0);
        }

        private IEnumerator PrepeareGameSequence()
        {
            currentMiniGame = testArrowMiniGame.CreateMiniGame();

            yield return currentMiniGame;

            currentMiniGame.SetupGame(this);

            yield return currentMiniGame.IsGameReadyToPlay;

            currentMiniGame.StartGame();
        }
    }
}