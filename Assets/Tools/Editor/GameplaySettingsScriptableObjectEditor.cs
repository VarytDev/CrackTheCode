using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LevelCreatorTool
{
    [CustomEditor(typeof(GameplaySettingsScriptableObject))]
    public class GameplaySettingsScriptableObjectEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(GUILayout.Button("Reset settings"))
            {
                GameSettings.Instance.gameplaySettings = new GameplaySettings();
                GameSettings.SaveGameplaySettings();
            }

            if(GUILayout.Button("Reset status"))
            {
                GameSettings.Instance.levelStatus = new LevelStatus();
                GameSettings.SaveLevelStatus();
            }
        }
    }
}