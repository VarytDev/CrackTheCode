using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonCollection : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartCustomGame()
    {
        GameSettings.SaveGameplaySettings();

        SceneManager.LoadScene(2);
    }

    public void NewCampGame()
    {
        GameSettings.Instance.levelStatus = new LevelStatus();
        GameSettings.SaveLevelStatus();
        ContinueCampGame();
    }

    public void ContinueCampGame()
    {
        StartCampGame(GameSettings.Instance.levelStatus.currentLevel);
    }

    public void StartCampGame(int level)
    {
        GameSettings.SaveGameplaySettings();

        GameplaySettings settings = Resources.Load<GameplaySettingsScriptableObject>("Levels/Level" + level).gameplaySettings;
        GameSettings.Instance.gameplaySettings = settings;
        GameSettings.Instance.gameplaySettings.level = level;

        SceneManager.LoadScene(2);
    }

    public void NextCampGame()
    {
        int level = GameSettings.Instance.gameplaySettings.level + 1;

        if(Resources.Load<GameplaySettingsScriptableObject>("Levels/Level" + level) == null)
        {
            SceneManager.LoadScene(3);
            return;
        }

        GameplaySettings settings = Resources.Load<GameplaySettingsScriptableObject>("Levels/Level" + level).gameplaySettings;

        GameSettings.Instance.gameplaySettings = settings;
        GameSettings.Instance.gameplaySettings.level = level;

        SceneManager.LoadScene(2);
    }

    public void NewGame()
    {
        GameSettings.Instance.gameplaySettings = GameSettings.LoadGameplaySettings();
         
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        if(!GameSettings.Instance.gameplaySettings.campaign) GameSettings.SaveGameplaySettings();

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void Quit()
    {
        //Application.Quit();
    }
}
