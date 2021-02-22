using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameplaySettings
{
    public bool trackHistory = true;

    public enum CodeLength
    {
        three = 3,
        four = 4,
        five = 5,
        six = 6
    }
    public CodeLength codeLength = (CodeLength)3;
    public int attempts = 10;
    public bool seedGeneration;
    public string seed;

    public string overrideCode = "";
    public bool campaign = false;
    [HideInInspector] public int level = 0;
    public string[] startingCodes = new string[0];
    [TextArea(15,20)] public string overrideEntry = "";
}

[System.Serializable]
public class LevelStatus
{
    //Campgign games
    public List<bool> levelProgression;
    public int currentLevel = 1;

    public string currentEntry;
    public string currentFullEntry;
    public int currentAttempts;
}

[System.Serializable]
public class LevelStatistics
{
    //Custom games
    public int totalPlayed => totalWins + totalLosts;
    public int totalWins;
    public int totalLosts;
}
