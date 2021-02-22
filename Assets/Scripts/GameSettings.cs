using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameSettings
{
    #region SingletonDefinition
    public static GameSettings Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameSettings();
            }
            return instance;
        }
    }

    static GameSettings instance;
    #endregion

    GameSettings()
    {
        gameplaySettings = LoadGameplaySettings();
        levelStatus = LoadLevelStatus();
    }

    public GameplaySettings gameplaySettings;
    public LevelStatus levelStatus;
    
    #region StaticVoids

    public static void SaveGameplaySettings()
    {
        string data = JsonUtility.ToJson(GameSettings.Instance.gameplaySettings);

        PlayerPrefs.SetString("gameplay-settings", data);
    }

    public static GameplaySettings LoadGameplaySettings()
    {
        return LoadData<GameplaySettings>("gameplay-settings", "");
    }

    public static void SaveLevelStatus()
    {
        string data = JsonUtility.ToJson(GameSettings.Instance.levelStatus);

        PlayerPrefs.SetString("level-status", data);
    }

    public static LevelStatus LoadLevelStatus()
    {
        return LoadData<LevelStatus>("level-status", "");
    }

    public static T LoadData<T>(string key, string defaultValue) where T : new()
    {
        T data = new T();

        //string rawData = System.IO.File.ReadAllText(Application.dataPath + "/Saves/gameplay-settings.json");
        string rawData = PlayerPrefs.GetString(key, defaultValue);

        if(rawData == "") data = new T();
        else data = JsonUtility.FromJson<T>(rawData);

        return data;
    }
    #endregion
}