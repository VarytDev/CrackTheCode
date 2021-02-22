using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace LevelCreatorTool
{
    public class LevelCreator : EditorWindow
    {
        int level, lastLevel;
        GameplaySettings.CodeLength length = GameplaySettings.CodeLength.three;
        int attempts = 1;
        string code;
        int codesLength, lastLength;
        string[] codes = new string[0];
        string entry;
        Vector2 scrollPos = Vector2.zero;

        [MenuItem("Window/LevelCreator")]
        static void Init()
        {
            LevelCreator window = (LevelCreator)EditorWindow.GetWindow(typeof(LevelCreator));
            window.Show();
        }

        void Awake()
        {
            LoadLevel();
        }

        void OnGUI()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);

            GUILayout.Label("Level Creator", EditorStyles.boldLabel);
            level = EditorGUILayout.IntField("Level", level);

            if(level != lastLevel)
            {
                LoadLevel();
            }

            length = (GameplaySettings.CodeLength)EditorGUILayout.EnumPopup("Code Length", length);
            attempts = EditorGUILayout.IntField("Attempts", attempts);
            code = EditorGUILayout.TextField("Code", code);

            codesLength = EditorGUILayout.IntField("Amount of starting codes", codesLength);

            if(codesLength != lastLength)
            {
                string[] previous = codes;

                codes = new string[codesLength];
                for(int i = 0; i < codesLength; i++)
                {
                    if(previous.Length <= i) break;
                    codes[i] = previous[i];
                }
            }

            for(int i = 0; i < codesLength; i++)
            {
                codes[i] = EditorGUILayout.TextField("Starting code " + (i + 1), codes[i]);
            }

            entry = EditorGUILayout.TextField("Entry", entry);

            if(GUILayout.Button("Save"))
            {
                GameplaySettingsScriptableObject so = ScriptableObject.CreateInstance<GameplaySettingsScriptableObject>();
                string name = "Level" + level + ".asset";
                so.gameplaySettings.codeLength = length;
                so.gameplaySettings.attempts = attempts;
                so.gameplaySettings.overrideCode = code;
                so.gameplaySettings.startingCodes = codes;
                so.gameplaySettings.overrideEntry = entry;

                if(!Directory.Exists(Application.dataPath + "/Resources/Levels/"))
                {
                    Directory.CreateDirectory(Application.dataPath + "/Resources/Levels/");
                }

                AssetDatabase.CreateAsset(so, "Assets/Resources/Levels/" + name);
                AssetDatabase.SaveAssets();
            }

            GUILayout.EndScrollView();

            lastLength = codesLength;
            lastLevel = level;
        }

        void LoadLevel()
        {
            if(Resources.Load<GameplaySettingsScriptableObject>("Levels/Level" + level) != null)
            {
                GameplaySettingsScriptableObject so = Resources.Load<GameplaySettingsScriptableObject>("Levels/Level" + level);
                LoadValues(so);
                codesLength = so.gameplaySettings.startingCodes.Length;
            } 
            else
            {
                GameplaySettingsScriptableObject so = ScriptableObject.CreateInstance<GameplaySettingsScriptableObject>();
                LoadValues(so);
                codesLength = 0;
            } 
        }

        void LoadValues(GameplaySettingsScriptableObject so)
        {
            length = so.gameplaySettings.codeLength;
            attempts = so.gameplaySettings.attempts;
            code = so.gameplaySettings.overrideCode;
            codes = so.gameplaySettings.startingCodes;
            entry = so.gameplaySettings.overrideEntry;
        }
    }
}