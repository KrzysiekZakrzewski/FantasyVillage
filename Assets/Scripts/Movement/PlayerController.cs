using BlueRacconGames.Events;
using BlueRacconGames.InventorySystem;
using Inputs;
using System;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Movement
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeReference, ReferencePicker] private IEventListenerFactory<BoolEventListener> turnOnMovableListener;
        [field: SerializeReference, ReferencePicker] private IEventListenerFactory<BoolEventListener> turnOffMovableListener;

        private float horizontalMove = 0f;
        private float verticalMove = 0f;

        [NonSerialized]
        protected PlayerInput playerInput;

        private TopDownCharacterController2D characterController;
        private bool isRunPressed;
        private ToolbarManager toolbarManager;
        private BoolEventListener turnOnCanMoveEventListener;
        private BoolEventListener turnOffCanMoveEventListener;


        [Inject]
        private void Inject(ToolbarManager toolbarManager)
        {
            this.toolbarManager = toolbarManager;
        }

        protected virtual void Awake()
        {
            characterController = GetComponent<TopDownCharacterController2D>();
            SetupInputs();

            turnOnCanMoveEventListener = turnOnMovableListener.Create();
            turnOffCanMoveEventListener = turnOffMovableListener.Create();

            turnOnCanMoveEventListener.ResponseE += characterController.SetCanMove;
            turnOffCanMoveEventListener.ResponseE += characterController.SetCanMove;
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
            turnOnCanMoveEventListener.ResponseE -= characterController.SetCanMove;
            turnOffCanMoveEventListener.ResponseE -= characterController.SetCanMove;
        }

        protected virtual void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(RunPerformed);
            playerInput.RemoveInputEventDelegate(RunPerformedUp);
            playerInput.RemoveInputEventDelegate(Use);
        }

        protected virtual void SetupInputs()
        {
            playerInput = InputManager.GetPlayer(0);

            playerInput.AddInputEventDelegate(RunPerformed, InputActionEventType.ButtonPressed, InputUtilities.Run);
            playerInput.AddInputEventDelegate(RunPerformedUp, InputActionEventType.ButtonUp, InputUtilities.Run);
            playerInput.AddInputEventDelegate(Use, InputActionEventType.ButtonPressed, InputUtilities.Use);
        }

        private void Use(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            toolbarManager.UseItem();
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