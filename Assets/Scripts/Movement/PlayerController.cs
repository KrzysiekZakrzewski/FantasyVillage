using BlueRacconGames.MeleeCombat;
using Inputs;
using Interactable;
using System;
using UnityEngine;

namespace BlueRacconGames.Movement
{
    public class PlayerController : MonoBehaviour
    {
        private float horizontalMove = 0f;
        private float verticalMove = 0f;

        [NonSerialized]
        protected PlayerInput playerInput;

        private TopDownCharacterController2D characterController;
        private bool isRunPressed;

        protected virtual void Awake()
        {
            characterController = GetComponent<TopDownCharacterController2D>();
            SetupInputs();
        }

        private void Update()
        {
            if (!characterController.CanMove)
            {
                horizontalMove = verticalMove = 0f;
                return;
            }

            horizontalMove = playerInput.GetAxis<float>(InputUtilities.HorizontalAxis);
            verticalMove = playerInput.GetAxis<float>(InputUtilities.VerticalAxis);
        }

        protected virtual void FixedUpdate()
        {
            characterController.Move(horizontalMove, verticalMove, isRunPressed);
        }

        private void OnDestroy()
        {
            RemoveInputs();
        }

        protected virtual void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(RunPerformed);
            playerInput.RemoveInputEventDelegate(RunPerformedUp);
        }

        protected virtual void SetupInputs()
        {
            playerInput = InputManager.GetPlayer(0);

            playerInput.AddInputEventDelegate(RunPerformed, InputActionEventType.ButtonPressed, InputUtilities.Run);
            playerInput.AddInputEventDelegate(RunPerformedUp, InputActionEventType.ButtonUp, InputUtilities.Run);
        }

        private void RunPerformed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            isRunPressed = true;
        }

        private void RunPerformedUp(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            isRunPressed = false;
        }
    }
}