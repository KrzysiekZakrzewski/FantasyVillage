using Assets.Scripts.Events;
using System;
using UnityEditor;
using UnityEngine;

namespace BlueRacconGames.Events
{
    [CustomEditor(typeof(BoolGameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = EditorApplication.isPlaying;

            DefaultGameEvent gameEvent = target as DefaultGameEvent;

            if (gameEvent != null)
            {
                if (GUILayout.Button("Reise"))
                {
                    try
                    {
                        gameEvent.Reise();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"B³¹d podczas wywo³ania Reise: {ex.Message}");
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Target is not a GameEvent", MessageType.Error);
            }
        }
    }
}