using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/GameplaySettings", order = 1)]
public class GameplaySettingsScriptableObject : ScriptableObject
{
    public GameplaySettings gameplaySettings = new GameplaySettings();
    
    void Awake()
    {
        gameplaySettings.trackHistory = false;
        gameplaySettings.attempts = 1;
        gameplaySettings.campaign = true;
    }
}