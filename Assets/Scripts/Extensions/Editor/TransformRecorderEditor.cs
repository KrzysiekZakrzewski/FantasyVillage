using System;
using UnityEditor;
using UnityEngine;

namespace BlueRacconGames.Extensions.Editors
{
    [CustomEditor(typeof(TransformRecorder))]
    public class TransformRecorderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            TransformRecorder recorder = target as TransformRecorder;

            if (recorder != null)
            {
                if (GUILayout.Button("Record"))
                {
                    try
                    {
                        recorder.RecordTransform();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"B³¹d podczas wywo³ania Record: {ex.Message}");
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Target is not a TransformRecorder", MessageType.Error);
            }
        }
    }
}