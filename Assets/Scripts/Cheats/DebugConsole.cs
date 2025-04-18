using System.Collections.Generic;
using UnityEngine;
using System;
using Inputs;
using Cheats.Console.Command;
using BlueRacconGames.Pool;
using BlueRacconGames.Resources;
using UnityEngine.UIElements;
using BlueRacconGames.Resources.Data;
using Unity.VisualScripting;

namespace Cheats.Console
{
    public class DebugConsole : MonoBehaviour
    {
        [SerializeField] private DebugCommandFactorySO[] commandFactoryArray;
        [SerializeField] private DefaultPooledEmitter pooledEmitter;
        [SerializeField] private ResourceSourceSpawnDataSO[] resourceData;
        [SerializeField] private Vector3 spawnOffset;
        private bool showConsole;
        private bool showHelp;
        private string input;
        Vector2 scroll;

        [NonSerialized]
        private PlayerInput playerInput;

        private List<IDebugCammand> commandList;

        private void Awake()
        {
            SetupInputs();
            GetReferences();

            for(int i = 0; i < resourceData.Length; i++)
            {

                pooledEmitter.EmitItem<DestructableResourceSource>(resourceData[i].Prefab, spawnOffset * i);
            }
        }

        private void OnDestroy()
        {
            playerInput.RemoveInputEventDelegate(OnToggleDown);
            playerInput.RemoveInputEventDelegate(OnReturn);
        }

        private void OnToggleDown(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            showConsole = !showConsole;
        }

        private void OnReturn(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            if (!showConsole)
                return;

            HandleInput();
            input = "";
        }

        private void HandleInput()
        {
            string[] properties = input.Split(' ');

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                if (input.Contains(commandBase.CommandId))
                {
                    if (commandList[i] as DebugCommand != null)
                    {
                        (commandList[i] as DebugCommand).Invoke();
                    }
                    else if (commandList[i] as DebugCommand<int> != null)
                    {
                        (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                    }
                }
            }
        }

        private void SetupInputs()
        {
            playerInput = InputManager.GetPlayer(0);

            playerInput.AddInputEventDelegate(OnToggleDown, InputActionEventType.ButtonPressed, InputUtilities.DebugConsole);
            playerInput.AddInputEventDelegate(OnReturn, InputActionEventType.ButtonPressed, InputUtilities.Submit);
        }

        private void GetReferences()
        {

        }

        private void CreateCommands()
        {
            foreach(DebugCommandFactorySO commandFactorySO in commandFactoryArray)
            {
                var command = commandFactorySO.CreateCommand();

                commandList.Add(command);
            }
        }

        private void OnGUI()
        {
            if (!showConsole)
                return;

            float y = 0f;

            if (showHelp)
            {
                GUI.Box(new Rect(0, y, Screen.width, 100), "");
                y += 100;

                Rect vievport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

                scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, vievport);

                for (int i = 0; i < commandList.Count; i++)
                {
                    DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                    string label = $"{commandBase.CommandFormat} - {commandBase.CommandDescrition}";

                    Rect labelRect = new Rect(5, 20 * i, vievport.width - 100, 20);

                    GUI.Label(labelRect, label);
                }
            }

            GUI.Box(new Rect(0, y, Screen.width, 30), "");

            GUI.backgroundColor = new Color(0, 0, 0, 0);

            input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        }
    }
}
