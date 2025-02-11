using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Inputs
{
    [Serializable]
    public class PlayerInput
    {
        private Controls controls;
        private InputAction mouseAction;
        private Dictionary<Action<InputAction.CallbackContext>, Dictionary<string, List<InputActionEventType>>> inputActionLUT;

        public event Action<InputControl> AnyButtonDown;

        public PlayerInput()
        {
            inputActionLUT = new Dictionary<Action<InputAction.CallbackContext>, Dictionary<string, List<InputActionEventType>>>();

            controls ??= new Controls();

            CreateMouseAction();
            CreateAnyButtonDownAction();

            EnableActions();
        }

        private void CreateMouseAction()
        {
            mouseAction = new InputAction(
                    type: InputActionType.Value);

            mouseAction.AddBinding("<Pointer>/position");
            mouseAction.AddBinding("<Mouse>/position");
        }

        private void CreateAnyButtonDownAction()
        {
            InputSystem.onAnyButtonPress
                .Call(ctrl =>
                {
                    AnyButtonDown?.Invoke(ctrl);
                });
        }

        private bool CanAddDelegate(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, InputAction inputAction)
        {
            bool result = false;

            result = inputActionLUT.TryGetValue(callback, out Dictionary<string, List<InputActionEventType>> callbackValue);

            if (!result)
                return true;

            result = callbackValue.TryGetValue(inputAction.ToString(), out List<InputActionEventType> eventTypeValue);

            if(!result)
                return true;

            if(!eventTypeValue.Contains(eventType))
                return true;

            return false;
        }

        private void AddDelegate(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, string actionName)
        {
            var inputAction = controls.FindAction(actionName);

            if (inputAction == null) return;

            if (!CanAddDelegate(callback, eventType, inputAction))
                return;

            switch (eventType)
            {
                case InputActionEventType.ButtonStarted:
                    inputAction.started += callback;
                    break;
                case InputActionEventType.ButtonPressed:
                    inputAction.performed += callback;
                    break;
                case InputActionEventType.ButtonUp:
                    inputAction.canceled += callback;
                    break;
            }

            DelegateAddedSuccessfully(callback, eventType, actionName);
        }

        private void DelegateAddedSuccessfully(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, string actionName)
        {
            var isExist = inputActionLUT.TryGetValue(callback, out  var actionLUT);

            if (!isExist)
            {
                CreateNewIputActionLUT(callback, eventType, actionName);
                return;
            }

            UpdateInputActionLUT(actionLUT, eventType, actionName);
        }

        private void CreateNewIputActionLUT(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, string actionName)
        {
            var eventList = new List<InputActionEventType>
            {
                eventType
            };

            var actionDictionary = new Dictionary<string, List<InputActionEventType>>
            {
                { actionName, eventList }
            };

            inputActionLUT.Add(callback, actionDictionary);
        }

        private List<InputActionEventType> CreateNewActionNameLUT(InputActionEventType eventType)
        {
            var eventList = new List<InputActionEventType>
            {
                eventType
            };

            return eventList;
        }

        private void UpdateInputActionLUT(Dictionary<string, List<InputActionEventType>> actionLUT, InputActionEventType eventType, string actionName)
        {
            var isExist = actionLUT.TryGetValue(actionName, out var eventTypes);

            if(!isExist)
            {
                actionLUT.Add(actionName, CreateNewActionNameLUT(eventType));
                return;
            }

            UpdateEventTypeList(eventTypes, eventType);
        }

        private void UpdateEventTypeList(List<InputActionEventType> eventTypesLUT, InputActionEventType eventType)
        {
            if (eventTypesLUT.Contains(eventType))
                return;

            eventTypesLUT.Add(eventType);
        }

        private void TryRemoveBindAction(Action<InputAction.CallbackContext> callback)
        {
            var isExist = inputActionLUT.TryGetValue(callback, out var actionLUT);

            if (!isExist)
                return;

            foreach(var action in actionLUT)
            {
                var eventList = action.Value;

                for (int i = 0; i < eventList.Count; i++)
                {
                    RemoveAction(callback, eventList[i], action.Key);
                }
            }

            inputActionLUT.Remove(callback);
        }

        private void TryRemoveBindAction(Action<InputAction.CallbackContext> callback, string actionName)
        {
            var isExist = inputActionLUT.TryGetValue(callback, out var actionLUT);

            if (!isExist)
                return;

            isExist = actionLUT.TryGetValue(actionName, out var events);

            if (!isExist)
                return;

            for (int i = 0; i < events.Count; i++)
            {
                RemoveAction(callback, events[i], actionName);
            }

            actionLUT.Remove(actionName);
        }

        private void RemoveAction(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, string actionName)
        {
            var inputAction = controls.FindAction(actionName);

            if (inputAction == null) return;

            switch (eventType)
            {
                case InputActionEventType.ButtonStarted:
                    inputAction.started -= callback;
                    break;
                case InputActionEventType.ButtonPressed:
                    inputAction.performed -= callback;
                    break;
                case InputActionEventType.ButtonUp:
                    inputAction.canceled -= callback;
                    break;
            }
        }

        public void AddInputEventDelegate(Action<InputAction.CallbackContext> callback, InputActionEventType eventType, string actionName)
        {
            AddDelegate(callback, eventType, actionName);
        }

        public void RemoveInputEventDelegate(Action<InputAction.CallbackContext> callback)
        {
            TryRemoveBindAction(callback);
        }

        public void RemoveInputEventDelegate(Action<InputAction.CallbackContext> callback, string actionName)
        {
            TryRemoveBindAction(callback, actionName);
        }

        public TValue GetAxis<TValue>(string actionName) where TValue : struct
        {
            var action = controls.FindAction(actionName);

            return action.ReadValue<TValue>();
        }

        public bool GetButtonDown(string actionName)
        {
            var action = controls.FindAction(actionName);

            return action.WasPerformedThisFrame();
        }

        public Vector2 GetCoordinates()
        {
            return mouseAction.ReadValue<Vector2>();
        }

        public InputAction GetActionByName(string actionName)
        {
            return controls.FindAction(actionName);
        }

        public void EnableActions()
        {
            controls.Enable();
            mouseAction.Enable();
        }

        public void DisableActions()
        {
            controls.Disable();
            mouseAction.Disable();
        }
    }

    public enum InputActionEventType
    {
        ButtonStarted,
        ButtonPressed,
        ButtonUp
    }
}