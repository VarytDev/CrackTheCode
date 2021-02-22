using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButtonCollection : MonoBehaviour
{
    public Slider amount;
    public Toggle isTracking;
    public TMP_Dropdown length;
    public TMP_InputField inputField;

    void Start()
    {
        amount.value = (float)GameSettings.Instance.gameplaySettings.attempts;
        amount.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Attempts: " + GameSettings.Instance.gameplaySettings.attempts;
        isTracking.isOn = GameSettings.Instance.gameplaySettings.trackHistory;
        length.value = (int)GameSettings.Instance.gameplaySettings.codeLength - 3;
        inputField.text = GameSettings.Instance.gameplaySettings.seed;
    }

    public void AttemptsSlider(Slider slider)
    {
        int value = (int)slider.value;
        TextMeshProUGUI uGUI;
        uGUI = slider.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        uGUI.text = "Attempts: " + value;

        GameSettings.Instance.gameplaySettings.attempts = value;
    }

    public void ToggleHistory(Toggle toggle)
    {
        bool value = toggle.isOn;

        GameSettings.Instance.gameplaySettings.trackHistory = value;
    }

    public void CodeLength(TMP_Dropdown dropdown)
    {
        int value = dropdown.value;

        value += 3;

        GameSettings.Instance.gameplaySettings.codeLength = (GameplaySettings.CodeLength)value;
    }

    public void InputSeed(string seed)
    {
        if(seed != "")
        {
            GameSettings.Instance.gameplaySettings.seedGeneration = true;
            GameSettings.Instance.gameplaySettings.seed = seed;
        }
        else
        {   
            GameSettings.Instance.gameplaySettings.seedGeneration = false;
            GameSettings.Instance.gameplaySettings.seed = seed;
        } 
    }
}
